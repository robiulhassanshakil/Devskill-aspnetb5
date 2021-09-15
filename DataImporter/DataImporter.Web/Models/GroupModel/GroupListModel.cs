using System.Linq;
using Autofac;
using AutoMapper;
using DataImporter.Importing.Services;
using DataImporter.Web.Models.Commons;

namespace DataImporter.Web.Models.GroupModel
{
    public class GroupListModel
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupListModel()
        {
            _groupService = Startup.AutofacContainer.Resolve<IGroupService>();
            _mapper= Startup.AutofacContainer.Resolve<IMapper>();
        }
        public GroupListModel(IGroupService groupService,IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }
        internal object GetGroupData(DataTablesAjaxRequestModel dataTableModel)
        {
            var data = _groupService.GetGroups(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] { "Name" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _groupService.DeleteGroup(id);
        }
    }
}