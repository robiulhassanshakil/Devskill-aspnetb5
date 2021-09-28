using DataImporter.Importing.BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Importing.Services
{
    public interface IExcelFileService
    {
        
        void FileUploadToDb(ExcelFile file);
        void GetExcelDatabase(int groupId);
    }
}