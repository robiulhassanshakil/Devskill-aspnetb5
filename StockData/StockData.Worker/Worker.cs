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
using StockData.Worker.Models;

namespace StockData.Worker
{

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ICreateStockPriceDataScrape _createStockPriceDataScrape;
        private readonly ICreateCompanyDataScrape _createCompanyDataScrape;

        public Worker(ILogger<Worker> logger, ICreateStockPriceDataScrape createDateScrape, ICreateCompanyDataScrape createCompanyDataScrape)
        {
            _logger = logger;
            _createStockPriceDataScrape = createDateScrape;
            _createCompanyDataScrape = createCompanyDataScrape;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            var data = _createCompanyDataScrape.IsCompanyDataEmpty();
            if (data == true)
            {
                _createCompanyDataScrape.LoadDataToCompany();
            }
            var statusReport = MarketStatusChecker();
            if (statusReport != "Closed")
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _createStockPriceDataScrape.LoadDataToStore();
                    await Task.Delay(60000, stoppingToken);
                }
            }

        }

        private string MarketStatusChecker()
        {
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(html);

            var nodes = doc.DocumentNode.SelectNodes("//table[@class='table table-bordered background-white shares-table fixedHeader']");
            var singelnode =
                doc.DocumentNode.SelectSingleNode(
                    "//div[@class='HeaderTop']/span[3]/span");
            var status = singelnode.InnerText;
            return status;
        }

    }
}
