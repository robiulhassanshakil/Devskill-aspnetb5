using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Profiling.Services;
using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Areas.Admin.Models
{
    public class MemberListModel
    {
        private readonly IMemberService _memberService;



        public MemberListModel()
        {
            _memberService = Startup.AutofacContainer.Resolve<IMemberService>();
        }

        public MemberListModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        internal object GetMembers(DataTablesAjaxRequestModel dataTableModel)
        {
            var data = _memberService.GetMembers(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] { "Name", "DateOfBirth", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.DateOfBirth.ToLongDateString(),
                            record.Address.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _memberService.DeleteMember(id);
        }
    }
}
