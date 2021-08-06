using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public interface ITicketRepository : IRepository<Ticket, int>
    {
    }
}
