using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Stock.Contexts;
using StockData.Stock.Repositories;

namespace StockData.Stock.UniteOfWorks
{
    public class StockUniteOfWork : UnitOfWork, IStockUniteOfWork
    {
        public IStockPriceRepository StockPrices  { get; private set; }
        public ICompanyRepository Companies { get; private set; }

        public StockUniteOfWork(IStockDbContext context,
            IStockPriceRepository stockPrice, ICompanyRepository companies) 
            : base((DbContext)context)
        {
            Companies = companies;
            StockPrices = stockPrice;   
        }
    }
}
