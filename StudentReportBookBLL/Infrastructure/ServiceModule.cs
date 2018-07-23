using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using StudentReportBookDAL.Interfaces;
using StudentReportBookDAL.Repositories;
//using Microsoft.IdentityModel.Protocols;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using StudentReportBookDAL.Context;
using Autofac.Core;
using Microsoft.AspNetCore.Identity;
using StudentReportBookBLL.Identity.Model;
using StudentReportBookDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace StudentReportBookBLL.Infrastructure
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //base.Load(builder);
            //var builderconfig = new ConfigurationBuilder();
            //// установка пути к текущему каталогу
            //builderconfig.SetBasePath(Directory.GetCurrentDirectory());
            //// получаем конфигурацию из файла appsettings.json
            //builderconfig.AddJsonFile("appsettings.json");
            //// создаем конфигурацию
            //var config = builderconfig.Build();
            //string connectionString = config.GetConnectionString("DefaultConnection");

            //builder.Register(c =>
            //{
            //    var conf = c.Resolve<IConfiguration>();
            //    var opt = new DbContextOptionsBuilder<AppDbContext>();
            //    opt.UseSqlServer(connectionString, b => b.MigrationsAssembly("StudentReportBook"));

            //    return new AppDbContext(opt.Options);
            //}).AsImplementedInterfaces();
            builder.RegisterType<UserStore<AppUser>>().As<IUserStore<AppUser>>();
            //builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>();
            //builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();

            //builder.Register(c => new UserStore<AppUser>(c.Resolve<MyApplicationContext>())).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<PersonManager>().As<IPersonManager>();
            //builder.RegisterType<ApplicationRoleManager>();

            builder.RegisterType<IdentityUnitOfWork>().As<IIdentityUnitOfWork>();//.WithParameter(AppDbContext dbContext);
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();//.WithParameter("connectionString", connectionString);
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

   
}

