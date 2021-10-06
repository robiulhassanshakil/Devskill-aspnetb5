using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Importing.Services;
using DataImporter.Web.Models.Commons;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DataImporter.Web.Models.Contact
{
    public class ContactListModel
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTime;
        private readonly ILifetimeScope _scope;

        public ContactListModel()
        {
            _contactService = Startup.AutofacContainer.Resolve<IContactService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTime = Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }
        public ContactListModel(IContactService contactService, IMapper mapper, IDateTimeUtility dateTime, ILifetimeScope scope)
        {
            _contactService = contactService;
            _mapper = mapper;
            _dateTime = dateTime;
            _scope = scope;
        }
        internal object LoadData(DataTablesAjaxRequestModel dataTableModel, Guid applicationUserId)
        {
            var data = _contactService.GetExcelfile(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] { "Name", "ExcelFileName", "DateTime", "Status" }), applicationUserId);
            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.GroupName,
                            record.ExcelFileName,
                            record.ImportDate.ToString(),
                            record.Status
                        }
                    ).ToArray()
            };
        }
    }
}
