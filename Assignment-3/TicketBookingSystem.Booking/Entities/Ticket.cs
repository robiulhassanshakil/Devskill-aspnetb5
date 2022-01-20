using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Entities
{
    public class Ticket:IEntity<int>
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public double TicketFee { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
