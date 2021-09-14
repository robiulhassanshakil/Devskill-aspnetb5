using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Data;
using DataImporter.Importing.Contexts;
using DataImporter.Importing.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Importing.Repositories
{
    public class FileRepository : Repository<ExcelFile, int>, IFileRepository
    {
        public FileRepository(IImportingDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
