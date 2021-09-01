using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockData.Data;
using StockData.Stock.Contexts;
using StockData.Stock.Entities;

namespace StockData.Stock.Repositories
{
    public class StockPriceRepository : Repository<StockPrice, int>,IStockPriceRepository
    {
        public StockPriceRepository(IStockDbContext context)
            : base((DbContext)context)
        {

        }
    }
}
