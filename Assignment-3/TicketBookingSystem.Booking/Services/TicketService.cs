using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.UniteOfWorks;

namespace TicketBookingSystem.Booking.Services
{
    public class TicketService : ITicketService
    {
        private readonly IBookingUniteOfWork _bookingUniteOfWork;

        
        public TicketService(IBookingUniteOfWork bookingUniteOfWork)
        {
            _bookingUniteOfWork = bookingUniteOfWork;
        }

        public void CreateTicket(Ticket ticket)
        {
            _bookingUniteOfWork.Tickets.Add(new Entities.Ticket
            {
                Destination = ticket.Destination,
                TicketFee = ticket.TicketFee,
                CustomerId = ticket.CustomerId
            });
            _bookingUniteOfWork.Save();
        }

        public IList<Ticket> GetAllTicket()
        {
            var ticketsEntities = _bookingUniteOfWork.Tickets.GetAll();
            var tickets = new List<Ticket>();
            foreach (var entity in ticketsEntities)
            {
                var ticket = new Ticket()
                {
                    Destination = entity.Destination,
                    TicketFee = entity.TicketFee,
                    CustomerId = entity.CustomerId
                };

                tickets.Add(ticket);
            }
            return tickets;
        }
       
    }
}
