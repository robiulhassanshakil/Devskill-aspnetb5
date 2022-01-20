using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public interface ITicketRepository : IRepository<Ticket, int, BookingDbContext>
    {
    }
}
