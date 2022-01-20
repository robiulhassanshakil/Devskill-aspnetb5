using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.Entities;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Repositories
{
    public class CustomerRepository : Repository<Customer, int, BookingDbContext>,
        ICustomerRepository
    {
        public CustomerRepository(IBookingDbContext context)
            : base((BookingDbContext)context)
        {

        }
    }
}
