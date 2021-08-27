using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
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
        private readonly IMapper _mapper;


        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper=Startup.AutofacContainer.Resolve<IMapper>();
        }

        public CreateTicketModel(ITicketService ticketService,IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        public void CreateTicket()
        {
            var ticket = _mapper.Map<Ticket>(this);

            _ticketService.CreateTicket(ticket);
        }
    }
}