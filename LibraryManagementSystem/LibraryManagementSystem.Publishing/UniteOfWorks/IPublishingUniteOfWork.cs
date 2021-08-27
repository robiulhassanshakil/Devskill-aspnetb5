using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Publishing.Repositories;

namespace LibraryManagementSystem.Publishing.UniteOfWorks
{
    public interface IPublishingUniteOfWork : IUnitOfWork
    {
        IBookRepository Books { get; }
        IAuthorRepository Authors { get; }
    }
}
