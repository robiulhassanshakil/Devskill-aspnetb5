using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Publishing.BusinessObjects;

namespace LibraryManagementSystem.Publishing.Services
{
    public interface IBookService
    {
        void CreateBook(Book book);
        
    }
}
