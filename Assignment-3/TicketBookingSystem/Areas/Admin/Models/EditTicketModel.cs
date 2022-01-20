using System;
using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class EditTicketModel
    {
        [Required, Range(1, 50000)]
        public int? Id { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Destination should be less than 200 charecters")]
        public string Destination { get; set; }

        [Required, Range(100, 50000)]
        public double? TicketFee { get; set; }

        [Required, Range(1, 50000)]
        public int? CustomerId { get; set; }

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public EditTicketModel(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;
        }

        public void LoadModelData(int id)
        {
            var ticket = _ticketService.GetTicket(id);

            _mapper.Map(ticket,this);

        }

        internal void Update()
        {
            var ticket = _mapper.Map<Ticket>(this);

            _ticketService.UpdateTicket(ticket);
        }
    }
}
