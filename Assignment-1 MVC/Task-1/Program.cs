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
using Serilog.Sinks.MSSqlServer;
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
          

            Log.Logger = new LoggerConfiguration().WriteTo.MSSqlServer(
                    connectionString: @"Server=DESKTOP-FPJ2F9H; Database=Task-1; Integrated Security=True;",
                    sinkOptions: new MSSqlServerSinkOptions{TableName ="Logs"})
                .WriteTo.Email(new EmailConnectionInfo
                {
                    FromEmail = "shakilvictor9102@gmail.com",
                    ToEmail = "robiul35-1663@diu.edu.bd",
                    MailServer = "smtp.gmail.com",
                    NetworkCredentials = new NetworkCredential
                    {
                        UserName = "shakilvictor9102@gmail.com",
                        Password = "itsshakil123"
                    },
                    EnableSsl = true,
                    Port = 465,
                    EmailSubject = "ERROR!"
                },
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}",
                    batchPostingLimit: 10
                    , restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                )
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

