using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockData.Stock.BusinessObjects;
using StockData.Stock.UniteOfWorks;

namespace StockData.Stock.Services
{
   
    public class StockService : IStockService
    {
        private readonly IStockUniteOfWork _stockUniteOfWork;

        public StockService(IStockUniteOfWork stockUniteOfWork)
        {
            _stockUniteOfWork = stockUniteOfWork;
        }

        public bool IsCompanyDataEmpty()
        {
            var testCount = _stockUniteOfWork.Companies.GetCount();

            if (testCount>0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void LoadDataToCompany(List<Company> companies)
        {
            foreach (var company in companies)
            {
                _stockUniteOfWork.Companies.Add(new Entities.Company()
                {
                    TradeCode = company.TradeCode

                });
                _stockUniteOfWork.Save();
            }
        }

        public void LoadDataToStore(List<StockPrice> stockprices)
        {
            foreach (var stockprice in stockprices)
            {
                _stockUniteOfWork.StockPrices.Add(new Entities.StockPrice()
                {
                    CompanyId = stockprice.CompanyId,
                    LastTradingPrice = stockprice.LastTradingPrice,
                    High = stockprice.High,
                    ClosePrice = stockprice.ClosePrice,
                    YesterdayClosePrice = stockprice.YesterdayClosePrice,
                    Change = stockprice.Change,
                    Trade = stockprice.Trade,
                    Value = stockprice.Value,
                    Volume = stockprice.Volume


                });
                _stockUniteOfWork.Save();
            }
           

        }

    }
}
