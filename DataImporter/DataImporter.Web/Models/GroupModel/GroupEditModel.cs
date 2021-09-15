using System;
using System.ComponentModel.DataAnnotations;
using Autofac;
using AutoMapper;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Services;

namespace DataImporter.Web.Models.GroupModel
{
    public class GroupEditModel
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 Characters")]
        public string Name { get; set; }
        public int Id { get; set; }

        public GroupEditModel()
        {
            _groupService= Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper= Startup.AutofacContainer.Resolve<IMapper>();
        }
        public GroupEditModel(IGroupService groupService,IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        internal void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);

            _mapper.Map(group, this);

        }

        internal void Update()
        {
            var group = _mapper.Map<Group>(this);
            _groupService.UpdateGroup(group);
        }
    }
}