using System;
using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Services;

namespace DataImporter.Web.Models.GroupModel
{
    public class CreateGroupModel
    {
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 Characters")]
        public string Name { get; set; }

        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public CreateGroupModel()
        {
            _groupService= Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper= Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateGroupModel(IGroupService groupService,IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        internal void CreateGroup()
        {
            var group = _mapper.Map<Group>(this);

            _groupService.CreateGroup(group);
        }
    }
}