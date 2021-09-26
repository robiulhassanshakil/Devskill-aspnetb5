using DataImporter.Importing.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Importing.Services
{
    public interface IContactService
    {

        (IList<ExcelFile> records, int total, int totalDisplay) GetExcelfile(int pageIndex, int pageSize, string searchText, string sortText, Guid applicationUser);
    }
}
