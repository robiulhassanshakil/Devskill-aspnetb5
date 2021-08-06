using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class CreateTicketModel
    {
        public string Destination { get; set; }
        public double TicketFee { get; set; }
        public int CustomerId { get; set; }

        private readonly ITicketService _ticketService;
        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public CreateTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public void CreateTicket()
        {
            var ticket = new Ticket()
            {
                Destination = Destination,
                TicketFee = TicketFee,
                CustomerId = CustomerId
            };

            _ticketService.CreateTicket(ticket);
        }
    }
}
