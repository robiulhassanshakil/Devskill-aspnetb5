using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Importing.Services;
using DataImporter.Web.Models.Commons;

namespace DataImporter.Web.Models.Files
{
    public class ExportHistoryListModel
    {
        private readonly IExcelFileService _excelFileService;
        private readonly IMapper _mapper;
        private readonly IDateTimeUtility _dateTime;


        public ExportHistoryListModel()
        {
            _excelFileService = Startup.AutofacContainer.Resolve<IExcelFileService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _dateTime = Startup.AutofacContainer.Resolve<IDateTimeUtility>();

        }
        public ExportHistoryListModel(IExcelFileService excelFileService, IMapper mapper, IDateTimeUtility dateTime)
        {

            _excelFileService = excelFileService;
            _mapper = mapper;
            _dateTime = dateTime;
        }

        internal object LoadDataExcelHistory(DataTablesAjaxRequestModel dataTableModel, Guid applicationuserId)
        {
            var data = _excelFileService.GetExcelFileHistory(
                dataTableModel.PageIndex,
                dataTableModel.PageSize,
                dataTableModel.SearchText,
                dataTableModel.GetSortText(new string[] { "GroupName", "Email", "ExportDate",  }), applicationuserId);

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.GroupName,
                            record.Email,
                            record.ExportDate.ToString(),
                            record.ExportLastExcelFieldId.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
