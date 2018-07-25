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
using StudentReportBookBLL.Auth;
using StudentReportBookBLL.Models;
using StudentReportBookBLL.Services;

namespace StudentReportBookBLL.Infrastructure
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
     
            builder.RegisterType<UserStore<AppUser>>().As<IUserStore<AppUser>>();
            builder.RegisterType<PersonManager>().As<IPersonManager>();
            builder.RegisterType<JwtFactory>().As<IJwtFactory>().SingleInstance();
            builder.RegisterType<JWTIssuerOptions>().AsSelf();

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

