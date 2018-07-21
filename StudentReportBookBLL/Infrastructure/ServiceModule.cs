using Autofac;
using AutoMapper;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.IO;

namespace StudentReportBookBLL.Infrastructure
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var builderconfig = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builderconfig.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builderconfig.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builderconfig.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

           // builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("connectionString", connectionString);
            builder.RegisterAssemblyTypes(typeof(ServiceModule).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                 .AsImplementedInterfaces();

            //builder.RegisterAssemblyTypes().AssignableTo(typeof(Profile)).As<Profile>();
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
