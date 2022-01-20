using System.Collections.Generic;
using TicketBookingSystem.Data;

namespace TicketBookingSystem.Booking.Entities
{
    public class Customer:IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
