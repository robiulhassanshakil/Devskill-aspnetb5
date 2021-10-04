using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Importing.Services;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models
{
    public class DashBoardModel
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public int GroupNumber { get; set; }
        public int ImportNumber { get; set; }
        public int ContactsNumber { get; set; }
        public DashBoardModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public DashBoardModel(IGroupService groupService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _groupService = groupService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        internal void loadData(Guid applicationUserId)
        {
            GroupNumber = _groupService.GetAllGroup(applicationUserId).Count;
            ImportNumber = _groupService.GetAllImportData(applicationUserId);
        }
    }
}
