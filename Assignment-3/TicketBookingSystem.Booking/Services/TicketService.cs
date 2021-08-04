using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Contexts;

namespace TicketBookingSystem.Booking.Services
{
    public class TicketService : ITicketService
    {

        private readonly BookingDbContext _bookingDbContext;
        public TicketService(BookingDbContext bookingDbContext)
        {
            _bookingDbContext = bookingDbContext;
        }
        public IList<Ticket> GetAllTicket()
        {
            var ticketsEntities = _bookingDbContext.Tickets.ToList();
            var tickets = new List<Ticket>();
            foreach (var entity in ticketsEntities)
            {
                var ticket = new Ticket()
                {
                    Destination = entity.Destination,
                    TicketFee = entity.TicketFee
                };

                tickets.Add(ticket);
            }
            return tickets;
        }
    }
}
