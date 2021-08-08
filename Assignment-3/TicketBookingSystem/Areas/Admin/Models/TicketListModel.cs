using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using FirstDemo.Models;
using TicketBookingSystem.Booking.BusinessObjects;
using TicketBookingSystem.Booking.Services;

namespace TicketBookingSystem.Areas.Admin.Models
{
    public class TicketListModel
    {
        private readonly ITicketService _ticketService;

        

        public TicketListModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
        }

        public TicketListModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        internal object GetTicketData(DataTablesAjaxRequestModel dataTableModel)
        {
            var data = _ticketService.GetTickets(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] { "Destination", "TicketFee", "CustomerId" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Destination,
                            record.TicketFee.ToString(),
                            record.CustomerId.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _ticketService.DeleteTicket(id);
        }
    }
}