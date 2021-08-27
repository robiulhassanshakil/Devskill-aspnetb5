using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EO= LibraryManagementSystem.Publishing.Entities;
using BO = LibraryManagementSystem.Publishing.BusinessObjects;

namespace LibraryManagementSystem.Publishing.Profiles
{
    public class PublishingProfile : Profile
    {
        public PublishingProfile()
        {
            CreateMap<EO.Book, BO.Book>().ReverseMap();
        }
    }
}
