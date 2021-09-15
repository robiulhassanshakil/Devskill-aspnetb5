using Autofac;
using DataImporter.Common.Utilities;

namespace DataImporter.Common
{
    public class CommonModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public CommonModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            
            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
