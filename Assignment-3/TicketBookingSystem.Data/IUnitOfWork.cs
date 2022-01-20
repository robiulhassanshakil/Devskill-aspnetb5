using System;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        Task SaveAsync();
    }
}
