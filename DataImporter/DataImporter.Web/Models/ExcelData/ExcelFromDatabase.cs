using System;
using System.Data;
using Autofac;
using AutoMapper;
using DataImporter.Common.Utilities;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Importing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace DataImporter.Web.Models.ExcelData
{
    public class ExcelFromDatabase
    {
        private readonly IExcelFileService _excelFileService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeUtility _dateTimeUtility;

        public DataTable DataTable {get; set;}
        public  string ExcelFileName {get; set;}
        public int ExcelLastId { get; set; }
        public ExcelFromDatabase()
        {
            _excelFileService = Startup.AutofacContainer.Resolve<IExcelFileService>(); 
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
            _dateTimeUtility = Startup.AutofacContainer.Resolve<IDateTimeUtility>();
        }
        public ExcelFromDatabase(IExcelFileService excelFileService, IMapper mapper, IHttpContextAccessor httpContextAccessor,IDateTimeUtility dateTimeUtility)
        {
            _excelFileService = excelFileService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _dateTimeUtility = dateTimeUtility;
        }

        internal byte[] GetExcelDatabase(int groupId)
        {
           var dataTableAndExcelData = _excelFileService.GetExcelDatabase(groupId);

           DataTable = dataTableAndExcelData.dataTable;
           ExcelLastId = dataTableAndExcelData.ExceldataId;

            ExcelFileName = _excelFileService.GetExcelFileName(groupId);
           ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

           byte[] fileContents;
           using (var package = new ExcelPackage())
           {
               var workSheet = package.Workbook.Worksheets.Add(ExcelFileName);
               workSheet.Cells["A1"].LoadFromDataTable(DataTable, true);
               fileContents = package.GetAsByteArray();
           }
            return fileContents;
        }

        internal string GetExcelDatabaseToJson(int groupId)
        {
            var dataTableAndExcelData = _excelFileService.GetExcelDatabase(groupId);
            DataTable = dataTableAndExcelData.dataTable;
            ExcelLastId = dataTableAndExcelData.ExceldataId;
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(DataTable, Formatting.Indented);
            return jsonString;
        }

        internal void CreateExportHistory(int groupId, int lastExcelFieldId)
        {
            var exportFileHistory = new ExportFileHistory()
            {
                GroupId = groupId,
                ExportDate = _dateTimeUtility.Now,
                Email = "NoT Send",
                ExportLastExcelFieldId = lastExcelFieldId
            };
            _excelFileService.ExportFileHistoryCreate(exportFileHistory);
        }

        public int GetGroupId(int excelLastId)
        {
           var groupid= _excelFileService.GetGroupId(excelLastId);

           return groupid;
        }

        internal byte[] GetExcelDatabaseForHistory(int groupId, int excelLastDataId)
        {
            var dataTableAndExcelData = _excelFileService.GetExcelDataForHistoryDownload(groupId, excelLastDataId);
            ExcelFileName = _excelFileService.GetExcelFileName(groupId);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add(ExcelFileName);
                workSheet.Cells["A1"].LoadFromDataTable(dataTableAndExcelData, true);
                fileContents = package.GetAsByteArray();
            }
            return fileContents;
        }
    }
}