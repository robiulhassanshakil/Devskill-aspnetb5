using System.Collections.Generic;
using StockData.Stock.BsinessObjects;

namespace StockData.Stock.Services
{
    public interface IStockService
    {
        void LoadDataToStore(List<StockPrice> stockPrices);
    }
}