using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public class TicketRepository : Repository<Ticket, int, BookingDbContext>, ITicketRepository
    {
        public TicketRepository(IBookingDbContext context)
            : base((BookingDbContext)context)
        {

        }
    }
}
