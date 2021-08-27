using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EO=TicketBookingSystem.Booking.Entities;
using BO=TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Booking.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<EO.Ticket, BO.Ticket>().ReverseMap();
        }
    }
}
