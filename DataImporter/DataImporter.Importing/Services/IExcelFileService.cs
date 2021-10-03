using System;
using System.Collections.Generic;
using System.Data;
using DataImporter.Importing.BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Importing.Services
{
    public interface IExcelFileService
    {
        
        void FileUploadToDb(ExcelFile file);
        (DataTable dataTable,int ExceldataId, int total, int totalDisplay) GetExcelDatabase(int pageIndex, int pageSize, string searchText, string sortText, int groupId);
        string GetExcelFileName(int groupId);
        void ExportFileHistoryCreate(ExportFileHistory exportFileHistory);
        (IList<ExportFileHistory> records, int total, int totalDisplay) GetExcelFileHistory(int pageIndex, int pageSize, string searchText, string sortText, Guid applicationUser);
        int GetGroupId(int excelLastId);
        DataTable GetExcelDataForHistoryDownload(int groupId, int excelLastDataId);
        (DataTable dataTable,int excelDataLastId) GetExcelDatabase(int groupId);
    }
}