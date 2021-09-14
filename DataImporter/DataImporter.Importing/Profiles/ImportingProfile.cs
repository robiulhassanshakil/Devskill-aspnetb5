using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EO = DataImporter.Importing.Entities;
using BO = DataImporter.Importing.BusinessObjects;

namespace DataImporter.Importing.Profiles
{
    public class ImportingProfile : Profile
    {
        public ImportingProfile()
        {
            CreateMap<EO.Contact, BO.Contact>().ReverseMap();
        }
    }
}
