using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using StockData.Stock.BusinessObjects;
using StockData.Stock.Services;

namespace StockData.Worker.Models
{
    public class CreateCompanyDataScrape : ICreateCompanyDataScrape
    {
        private readonly IStockService _stockService;

        public CreateCompanyDataScrape(IStockService stockService)
        {
            _stockService = stockService;
        }

        public void LoadDataToCompany()
        {
            var companies = new List<Company>();
            var html = @"https://www.dse.com.bd/latest_share_price_scroll_l.php";
            HtmlWeb web = new HtmlWeb();
            var doc = web.Load(html);

            var nodes = doc.DocumentNode.SelectNodes("//table[@class='table table-bordered background-white shares-table fixedHeader']");
            foreach (var node in nodes)
            {

                var noThead = node.ChildNodes[1];
                noThead.Remove();

                var cm = new Company()
                {
                    TradeCode = node.SelectSingleNode("tbody/tr/td[2]").InnerText
                    
                };
                companies.Add(cm);

                var noTbody = node.ChildNodes[2];
                noTbody.Remove();


                var newnode = node.SelectNodes(".//tr");
                foreach (var nNode in newnode)
                {

                    var cmp = new Company()
                    {

                        TradeCode = nNode.SelectSingleNode("td[2]").InnerText
                        
                    };
                    companies.Add(cmp);

                }


            }

            _stockService.LoadDataToCompany(companies);
        }
    }
}

