using System.Collections.Generic;
using StockData.Stock.BusinessObjects;

namespace StockData.Stock.Services
{
    public interface IStockService
    {
        void LoadDataToStore(List<StockPrice> stockPrices);
        void LoadDataToCompany(List<Company> companies);
        bool IsCompanyDataEmpty();
    }
}