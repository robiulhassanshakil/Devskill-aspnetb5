using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace StockData.Worker
{
    
    public class WorkerModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public WorkerModule(string connectionStringName, string migrationAssemblyName)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
            
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreateDataScrape>().As<ICreateDateScrape>()
                .InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
