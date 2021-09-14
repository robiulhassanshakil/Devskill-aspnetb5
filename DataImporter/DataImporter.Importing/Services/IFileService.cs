using DataImporter.Importing.BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Importing.Services
{
    public interface IFileService
    {
        
        void FileUploadToDb(ExcelFile file);
    }
}