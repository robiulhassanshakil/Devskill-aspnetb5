﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StockData.Stock.BsinessObjects;
using StockData.Stock.Services;

namespace StockData.Worker
{
   
    public class CreateDataScrape : ICreateDateScrape
    {
        private readonly IStockService _stockService;

        public CreateDataScrape(IStockService stockService)
        {
            _stockService = stockService;
        }

        public void LoadDataToStore()
        {
            var stockPrices = new List<StockPrice>();
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(html);

            var nodes = doc.DocumentNode.SelectNodes("//table[@class='table table-bordered background-white shares-table fixedHeader']");
            foreach (var node in nodes)
            {

                var noThead = node.ChildNodes[1];
                noThead.Remove();

                var sp = new StockPrice
                {
                   CompanyId = Int32.Parse(node.SelectSingleNode("tbody/tr/td[1]").InnerText),
                    LastTradingPrice = double.Parse(node.SelectSingleNode("tbody/tr/td[3]").InnerText),
                    High = double.Parse(node.SelectSingleNode("tbody/tr/td[4]").InnerText),
                    Low = double.Parse(node.SelectSingleNode("tbody/tr/td[5]").InnerText),
                    ClosePrice = double.Parse(node.SelectSingleNode("tbody/tr/td[6]").InnerText),
                    YesterdayClosePrice = double.Parse(node.SelectSingleNode("tbody/tr/td[7]").InnerText),
                    Change = node.SelectSingleNode("tbody/tr/td[8]").InnerText,
                    Trade = double.Parse(node.SelectSingleNode("tbody/tr/td[9]").InnerText),
                    Value = double.Parse(node.SelectSingleNode("tbody/tr/td[10]").InnerText),
                    Volume = double.Parse(node.SelectSingleNode("tbody/tr/td[11]").InnerText)
                    
                };
                stockPrices.Add(sp);

                var noTbody = node.ChildNodes[2];
                noTbody.Remove();


                var newnode = node.SelectNodes(".//tr");
                foreach (var nNode in newnode)
                {
                    
                    var spz = new StockPrice()
                    {

                        CompanyId = Int32.Parse(nNode.SelectSingleNode("td[1]").InnerText),
                        LastTradingPrice = double.Parse(nNode.SelectSingleNode("td[3]").InnerText),
                        High = double.Parse(nNode.SelectSingleNode("td[4]").InnerText),
                        Low = double.Parse(nNode.SelectSingleNode("td[5]").InnerText),
                        ClosePrice = double.Parse(nNode.SelectSingleNode("td[6]").InnerText),
                        YesterdayClosePrice = double.Parse(nNode.SelectSingleNode("td[7]").InnerText),
                        Change = nNode.SelectSingleNode("td[8]").InnerText,
                        Trade = double.Parse(nNode.SelectSingleNode("td[9]").InnerText),
                        Value = double.Parse(nNode.SelectSingleNode("td[10]").InnerText),
                        Volume = double.Parse(nNode.SelectSingleNode("td[11]").InnerText)
                    };
                    stockPrices.Add(spz);

                }


            }


            _stockService.LoadDataToStore(stockPrices);
        }
    }
}
