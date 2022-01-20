using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int, BookingDbContext>
    {
    }
}
