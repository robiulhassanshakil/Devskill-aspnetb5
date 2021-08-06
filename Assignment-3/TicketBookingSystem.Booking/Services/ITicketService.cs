using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Booking.Services
{
    public interface ITicketService
    {
        IList<Ticket> GetAllTicket();
        void CreateTicket(Ticket ticket);
    }
}
