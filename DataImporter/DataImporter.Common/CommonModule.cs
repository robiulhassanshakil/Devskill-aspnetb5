﻿using Autofac;
using DataImporter.Common.Utilities;

namespace DataImporter.Common
{
    public class CommonModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EmailService>().As<IEmailService>()
                .WithParameter("host", "smtp.gmail.com")
                .WithParameter("port", 465)
                .WithParameter("username", "victorshakil9102@gmail.com")
                .WithParameter("password", "robiulhassanshakilvictor")
                .WithParameter("useSsl", true)
                .WithParameter("from", "victorshakil9102@gmail.com")
                .InstancePerLifetimeScope();
                
            base.Load(builder);
        }
    }
}
