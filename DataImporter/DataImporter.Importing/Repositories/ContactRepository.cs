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
    public class ContactRepository : Repository<Contact, int>,IContactRepository
    {
        public ContactRepository(IImportingDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
