using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Autofac;
using DataImporter.Web.Models.Files;
using DataImporter.Web.Models.GroupModel;


namespace DataImporter.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
