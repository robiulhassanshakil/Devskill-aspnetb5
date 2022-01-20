using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly DbContext _dbContext;

        public UnitOfWork(DbContext dbContext) => this._dbContext = dbContext;

        public void Dispose() => this._dbContext?.Dispose();

        public void Save() => this._dbContext?.SaveChanges();

        public async Task SaveAsync()
        {
            int num = await this._dbContext.SaveChangesAsync();
        }
    }
}
