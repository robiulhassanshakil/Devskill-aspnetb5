using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Publishing.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Publishing.Repositories
{
    public class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(IAuthorRepository context)
            : base((DbContext)context)
        {

        }
    }
}
