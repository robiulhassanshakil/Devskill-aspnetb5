using System;
using Autofac;
using AutoMapper;
using DataImporter.Importing.Services;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Models.ExcelData
{
    public class ExcelFromDatabase
    {
        private readonly IExcelFileService _excelFileService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExcelFromDatabase()
        {
            _excelFileService = Startup.AutofacContainer.Resolve<IExcelFileService>(); 
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }
        public ExcelFromDatabase(IExcelFileService excelFileService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _excelFileService = excelFileService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        internal void GetExcelDatabase(int groupId)
        {
            _excelFileService.GetExcelDatabase(groupId);
        }
    }
}