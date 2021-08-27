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
    public class BookRepository : Repository<Book,int>, IBookRepository
    {
        public BookRepository(IBookRepository context)
            : base((DbContext)context)
        {

        }
    }
}
