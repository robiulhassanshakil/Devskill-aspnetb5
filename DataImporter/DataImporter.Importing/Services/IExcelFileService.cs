using System.Data;
using DataImporter.Importing.BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Importing.Services
{
    public interface IExcelFileService
    {
        
        void FileUploadToDb(ExcelFile file);
        (DataTable dataTable,int ExceldataId) GetExcelDatabase(int groupId);
        string GetExcelFileName(int groupId);
        void ExportFileHistoryCreate(ExportFileHistory exportFileHistory);
    }
}