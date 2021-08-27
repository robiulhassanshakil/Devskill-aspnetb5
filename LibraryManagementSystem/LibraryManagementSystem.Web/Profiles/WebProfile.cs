using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Publishing.BusinessObjects;
using LibraryManagementSystem.Web.Areas.Admin.Models;

namespace LibraryManagementSystem.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateBookModel, Book>().ReverseMap();
            CreateMap<EditBookModel, Book>().ReverseMap();
        }
    }
}
