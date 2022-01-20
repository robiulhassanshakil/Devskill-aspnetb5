using System;

namespace TicketBookingSystem.Booking.Exceptions
{
    public class InvalidParameterException:Exception
    {
        public InvalidParameterException(string message)
            :base(message)
        {

        }
    }
}
