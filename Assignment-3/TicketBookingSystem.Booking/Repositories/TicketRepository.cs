using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public class TicketRepository : Repository<Ticket, int>, ITicketRepository
    {
        public TicketRepository(IBookingDbContext context)
            : base((DbContext)context)
        {

        }
    }
}
