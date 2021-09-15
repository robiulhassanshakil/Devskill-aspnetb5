using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Services;

namespace DataImporter.Web.Models.GroupModel
{
    public class AllGroupForContacts
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public AllGroupForContacts()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public AllGroupForContacts(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        public List<Group> LoadAllGroup()
        {
          var Group=_groupService.GetAllGroup().ToList();

            return Group;
        }
    }
}
