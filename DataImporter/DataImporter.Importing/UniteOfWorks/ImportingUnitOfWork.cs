using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Data;
using DataImporter.Importing.Contexts;
using DataImporter.Importing.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Importing.UniteOfWorks
{
    public class ImportingUnitOfWork : UnitOfWork, IImportingUnitOfWork
    {
       public IGroupRepository Groups { get; private set; }
       public IContactRepository Contacts { get; private set; }

       public ImportingUnitOfWork(IImportingDbContext context,
           IGroupRepository groups,
           IContactRepository contacts
       ) : base((DbContext)context)
       {
           Groups = groups;
           Contacts = contacts;
       }
    }
}
