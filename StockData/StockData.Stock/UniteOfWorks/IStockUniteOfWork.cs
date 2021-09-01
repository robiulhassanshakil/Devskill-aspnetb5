using StockData.Data;
using StockData.Stock.Repositories;

namespace StockData.Stock.UniteOfWorks
{
    public interface IStockUniteOfWork : IUnitOfWork
    {
        IStockPriceRepository StockPrices { get; }
        ICompanyRepository Companies { get; }
    }
}