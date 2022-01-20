using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Booking.Entities;

namespace TicketBookingSystem.Booking.Contexts
{
    public class BookingDbContext : DbContext, IBookingDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public BookingDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
