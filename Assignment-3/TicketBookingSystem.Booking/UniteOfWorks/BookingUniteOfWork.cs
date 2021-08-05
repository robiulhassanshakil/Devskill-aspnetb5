using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.Repositories;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.UniteOfWorks
{
    public class BookingUniteOfWork : UnitOfWork ,IBookingUniteOfWork
    {
         public ICustomerRepository Customers { get; private set; }
         public ITicketRepository Tickets { get; private set; }

         public BookingUniteOfWork(IBookingDbContext context,
             ICustomerRepository customers,
             ITicketRepository tickets) : base((DbContext) context)
         {
             Customers = customers;
             Tickets = tickets;
         }
    }
}
