using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace StockData.Worker
{
    
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICreateDateScrape _createDateScrape;

        public Worker(ILogger<Worker> logger, ICreateDateScrape createDateScrape)
        {
            _logger = logger;
            _createDateScrape = createDateScrape;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _createDateScrape.LoadDataToStore();   
                await Task.Delay(1000, stoppingToken);
            }
        }

    }
}
