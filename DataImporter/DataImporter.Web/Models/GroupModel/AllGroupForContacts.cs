using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Services;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models.GroupModel
{
    public class AllGroupForContacts
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public List<Group> Groups { get; set; }
        public int GroupId { get; set; }
        public AllGroupForContacts()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public AllGroupForContacts(IGroupService groupService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _groupService = groupService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<Group> LoadAllGroup()
        {
            var applicationuserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            Groups = _groupService.GetAllGroup(applicationuserId).ToList();

            Groups.Insert(0,new Group() { Id = 0, Name = "--Select Group Name--" });

            return Groups;
        }
    }
}
