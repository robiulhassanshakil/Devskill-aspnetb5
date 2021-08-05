using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketBookingSystem.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
    }
}
