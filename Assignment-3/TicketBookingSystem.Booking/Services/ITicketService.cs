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
    
        void CreateTicket(Ticket ticket);
        (IList<Ticket>records,int total,int totalDisplay )GetTickets(int pageIndex, int pageSize, string searchText, string sortText);
        Ticket GetTicket(int id);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int id);
    }
}
