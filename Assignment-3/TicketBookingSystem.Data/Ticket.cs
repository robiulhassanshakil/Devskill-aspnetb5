using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.Data
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Destination { get; set; }
        public double TicketFee { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
