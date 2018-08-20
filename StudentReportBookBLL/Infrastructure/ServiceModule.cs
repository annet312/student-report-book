using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Autofac;
using AutoMapper;
using StudentReportBookDAL.Interfaces;
using StudentReportBookDAL.Repositories;
using StudentReportBookDAL.Context;
using StudentReportBookDAL.Entities;
using StudentReportBookBLL.Auth;
using StudentReportBookBLL.Identity.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace StudentReportBookBLL.Infrastructure
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserStore<AppUser>>().As<IUserStore<AppUser>>();

            builder.RegisterType<PersonManager>().As<IPersonManager>();
            builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
            builder.RegisterType<JWTIssuerOptions>().AsSelf();

            var build = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configur= build.Build();
            var connectionString = configur.GetConnectionString("DefaultConnection");

            builder.Register(c =>
            {
                var config = c.Resolve<IConfiguration>();

                var opt = new DbContextOptionsBuilder<AppDbContext>();
                opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("StudentReportBook"));

                return new AppDbContext(opt.Options);
            }).AsSelf().InstancePerLifetimeScope();
            //unique instance will be returned from each request for a service - as default:
            builder.RegisterType<IdentityUnitOfWork>().As<IIdentityUnitOfWork>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterAssemblyTypes(typeof(ServiceModule).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                 .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes().AssignableTo(typeof(Profile)).As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();
        }
    }

    public static class ServiceProviderExtensions
    {
        public static void AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddUserManager<UserManager<IdentityUser>>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        }
    }
}

