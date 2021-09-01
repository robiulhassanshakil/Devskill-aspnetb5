using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using StockData.Stock;
using StockData.Stock.Contexts;
using Microsoft.AspNetCore.Builder;

namespace StockData.Worker
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        private static string _connectionString;
        private static string _migrationAssemblyName;
        public static IConfiguration _configuration { get; set; }
        public static ILifetimeScope AutofacContainer { get; set; }

        public static void Main(string[] args)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .AddEnvironmentVariables()
                .Build();
            var connectionStringName = "DefaultConnection";

            _connectionString = _configuration.GetConnectionString(connectionStringName);

            _migrationAssemblyName = typeof(Worker).Assembly.FullName;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .ReadFrom.Configuration(_configuration)
                .CreateLogger();
            try
            {
                Log.Information("Application Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .UseSerilog()
                .ConfigureContainer<ContainerBuilder>(builder =>
                {
                    builder.RegisterModule(new WorkerModule(_connectionString,
                        _migrationAssemblyName));
                    builder.RegisterModule(new StockModule(_connectionString, _migrationAssemblyName));

                })
                .ConfigureServices((hostContext, services) =>
                {
                    _connectionString = hostContext.Configuration["ConnectionStrings:DefaultConnection"];
                    _migrationAssemblyName = typeof(Worker).Assembly.FullName;
                    services.AddDbContext<StockDbContext>(option =>
                        option.UseSqlServer(_connectionString, m => m.MigrationsAssembly(_migrationAssemblyName)));
                    services.AddHostedService<Worker>();
                });

    }



}
