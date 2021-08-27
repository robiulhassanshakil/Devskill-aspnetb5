﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TicketBookingSystem.Areas.Admin.Models;
using TicketBookingSystem.Booking.BusinessObjects;

namespace TicketBookingSystem.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateTicketModel, Ticket>().ReverseMap();
            CreateMap<EditTicketModel, Ticket>().ReverseMap();
        }
    }
}
