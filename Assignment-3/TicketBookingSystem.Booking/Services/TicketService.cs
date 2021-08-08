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

        public void DeleteTicket(int id)
        {
            _bookingUniteOfWork.Tickets.Remove(id);
            _bookingUniteOfWork.Save();
        }

        public Ticket GetTicket(int id)
        {
            var ticket = _bookingUniteOfWork.Tickets.GetById(id);
            
            if (ticket == null) return null;
           
            return new Ticket()
            {
                Id = ticket.Id,
                Destination = ticket.Destination,
                TicketFee = ticket.TicketFee,
                CustomerId = ticket.CustomerId
            };
        }

        public (IList<Ticket> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var ticketData = _bookingUniteOfWork.Tickets.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Destination.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from ticket in ticketData.data
                              select (new Ticket
                              {
                                  Id = ticket.Id,
                                  Destination = ticket.Destination,
                                  TicketFee = ticket.TicketFee,
                                  CustomerId = ticket.CustomerId
                              })).ToList();

            return (resultData, ticketData.total, ticketData.totalDisplay);
        }

        public void UpdateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidOperationException("Ticket is missing");

            var ticketEntity = _bookingUniteOfWork.Tickets.GetById(ticket.Id);

            if (ticketEntity != null)
            {
                ticketEntity.Destination = ticket.Destination;
                ticketEntity.TicketFee = ticket.TicketFee;
                ticketEntity.CustomerId = ticket.CustomerId;

                _bookingUniteOfWork.Save();
            }
            else
            {
                throw new InvalidOperationException("Couldn't find ticket"); 
            }

        }
    }
}
