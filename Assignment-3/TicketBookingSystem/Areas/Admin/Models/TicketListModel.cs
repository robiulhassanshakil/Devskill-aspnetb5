using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class TicketListModel
    {
        private readonly ITicketService _ticketService;

        public IList<Ticket> Tickets { get; set; }

        public TicketListModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public TicketListModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public void LoadModelData()
        {
            Tickets = _ticketService.GetAllTicket();
        }
    }
}