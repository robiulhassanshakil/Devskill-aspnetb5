using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Profiling.Contexts;
using SocialNetwork.Profiling.Repositories;
using SocialNetwork.Profiling.Services;
using SocialNetwork.Profiling.UniteOfWorks;

namespace SocialNetwork.Profiling
{
    public class ProfilingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ProfilingModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProfilingDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<ProfilingDbContext>().As<IProfilingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();
            builder.RegisterType<ProfilingUniteOfWork>().As<IProfilingUniteOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MemberRepository>().As<IMemberRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PhotoRepository>().As<IPhotoRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MemberService>().As<IMemberService>()
                .InstancePerLifetimeScope();

         

            base.Load(builder);

        }
    }
}
