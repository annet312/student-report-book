using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentReportBookDAL.Context;
using StudentReportBook.Models.Entities;
using AutoMapper;
using StudentReportBook.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using StudentReportBook.Helpers;
using Microsoft.AspNetCore.Http;
using Autofac;
using StudentReportBookBLL.Infrastructure;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Helpers;
using StudentReportBookBLL.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Security.Claims;
//using StudentReportBookDAL.Repositories;

namespace StudentReportBook
{


    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public IConfiguration Configuration { get;private set; }

       

        public Startup(IHostingEnvironment env)//(IConfiguration configuration)
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


            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //// jwt wire up
            //// Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JWTIssuerOptions));

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
                ClockSkew = TimeSpan.Zero
            };
            services.AddIdentity<IdentityUser, IdentityRole>()
              .AddUserManager<UserManager<IdentityUser>>()
              .AddRoleManager<RoleManager<IdentityRole>>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();
            services.AddHttpContextAccessor();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";

            services.AddAuthentication(/*JwtBearerDefaults.AuthenticationScheme*/
                options => 
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
                })
            .AddJwtBearer(configureOptions =>
            {
                //configureOptions.RequireHttpsMetadata = false;
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JWTIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
                configureOptions.Authority = domain;
                configureOptions.Authority = Configuration["Auth0:ApiIdentifier"];
            });


            //// api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Moderator", policy => { policy.RequireClaim("Moderator", "Moderator"); });
                options.AddPolicy("Teacher", policy => { policy.RequireClaim("Teacher", "Teacher"); });
                options.AddPolicy("Student", policy => { policy.RequireClaim("Student", "Student"); });
            });


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

            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(Startup));

            //    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //    {
            //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //};


            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
             };

            services.AddMvc() 
                //.AddJsonOptions(options => {
                //   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //})
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
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseDefaultFiles();

            //app.UseCookieAuthentication();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
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
