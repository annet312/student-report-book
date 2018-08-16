using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Autofac;
using StudentReportBookBLL.Infrastructure;
//using StudentReportBookDAL.Context;
using StudentReportBook.Models.Entities;
using StudentReportBook.Scope;
using StudentReportBookBLL.Identity.Auth;
using System.IO;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using StudentReportBook.Models;
using StudentReportBookDAL.Context;

namespace StudentReportBook
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
               b => b.MigrationsAssembly("StudentReportBook")));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //// jwt wire up
            //// Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JWTIssuerOptions));
            var configurationBuilder = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json");
            Configuration = configurationBuilder.Build();
            var secretKey = Configuration["JwtAuthentication:SecurityKey"];
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            //// Configure JwtIssuerOptions
            services.Configure<JWTIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JWTIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JWTIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });
            services.AddOptions();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JWTIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JWTIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddUserManager<UserManager<IdentityUser>>()
              .AddRoleManager<RoleManager<IdentityRole>>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            services.AddHttpContextAccessor();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JWTIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                configureOptions.Authority = domain;
                configureOptions.Authority = Configuration["Auth0:ApiIdentifier"];
                configureOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " +
                            context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " +
                            context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
            //// api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Student", policy =>
                {
                    policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new HasScopeRequirement("Student", domain));
                });

                options.AddPolicy("Moderator", policy =>  policy.Requirements.Add(new HasScopeRequirement("Moderator", domain)));
                options.AddPolicy("Teacher", policy => policy.Requirements.Add(new HasScopeRequirement("Teacher", domain)));
            });
           
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();

            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);


            services.AddAutoMapper(typeof(Startup));

        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
             };
            
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                //    app.UseStatusCodePagesWithReExecute("/Error");
                //    app.UseExceptionHandler("/Error");
                app.UseExceptionHandler(
                         builder =>
                         {
                             builder.Run(
                             async context =>
                             {
                                 context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                 context.Response.ContentType = "application/json";
                                 var ex = context.Features.Get<IExceptionHandlerFeature>();
                                 if (ex != null)
                                 {
                                     var err = JsonConvert.SerializeObject(new Error()
                                     {
                                         Stacktrace = ex.Error.StackTrace,
                                         Message = ex.Error.Message
                                     });
                                     await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(err), 0, err.Length).ConfigureAwait(false);
                                 }
                             });
                         }
                    );
                app.UseHsts();

            }
            app.UseStatusCodePages();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseDefaultFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }

        
}
