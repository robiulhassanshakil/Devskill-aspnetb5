using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Email;
using Task_1.Models;

namespace Task_1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", false,reloadOnChange:true)
                .AddEnvironmentVariables()
                .Build();
          
            //Database
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configBuilder)
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
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:80");
                });
    }
}

