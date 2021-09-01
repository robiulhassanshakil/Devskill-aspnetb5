using StockData.Data;
using StockData.Stock.Entities;

namespace StockData.Stock.Repositories
{
    public interface IStockPriceRepository : IRepository<StockPrice, int>
    {
    }
}