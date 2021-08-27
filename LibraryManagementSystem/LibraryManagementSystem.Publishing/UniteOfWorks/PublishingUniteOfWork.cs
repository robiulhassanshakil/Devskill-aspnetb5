using LibraryManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Publishing.Contexts;
using LibraryManagementSystem.Publishing.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Publishing.UniteOfWorks
{
    public class PublishingUniteOfWork : UnitOfWork, IPublishingUniteOfWork 
    {
        public IBookRepository Books { get; private set; }
        public IAuthorRepository Authors { get; private set; }

        public PublishingUniteOfWork(IPublishingDbContext context,
            IBookRepository book, IAuthorRepository author) 
            : base((DbContext)context)
        {
            Books = book;
            Authors = author;
        }
    }
}
