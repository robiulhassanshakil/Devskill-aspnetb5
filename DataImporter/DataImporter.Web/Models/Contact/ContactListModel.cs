using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Importing.Services;

namespace DataImporter.Web.Models.Contact
{
    public class ContactListModel
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTime;


        public ContactListModel()
        {
            _contactService = Startup.AutofacContainer.Resolve<IContactService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTime = Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }
        public ContactListModel(IContactService contactService, IMapper mapper, IDateTimeUtility dateTime)
        {
            
            _contactService = contactService;
            _mapper = mapper;
            _dateTime = dateTime;
        }
        internal void loadData()
        {
            _contactService.LoadAllData();
        }
    }
}
