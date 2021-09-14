using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DataImporter.Importing.Contexts;
using DataImporter.Importing.Repositories;
using DataImporter.Importing.Services;
using DataImporter.Importing.UniteOfWorks;

namespace DataImporter.Importing
{
    public class ImportingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ImportingModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ImportingDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ImportingDbContext>().As<IImportingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>().As<IGroupRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactRepository>().As<IContactRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ContactService>().As<IContactService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImportingUnitOfWork>().As<IImportingUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileRepository>().As<IFileRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<FileService>().As<IFileService>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
