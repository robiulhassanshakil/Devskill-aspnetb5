using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Publishing.Entities;

namespace LibraryManagementSystem.Publishing.Repositories
{
    public interface IBookRepository : IRepository<Book,int>
    {
    }
}
