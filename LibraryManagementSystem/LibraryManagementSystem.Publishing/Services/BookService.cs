using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryManagementSystem.Publishing.BusinessObjects;
using LibraryManagementSystem.Publishing.UniteOfWorks;

namespace LibraryManagementSystem.Publishing.Services
{
    public class BookService : IBookService
    {
        private readonly IBooKUniteOfWork _booKUniteOfWork;
        private readonly IMapper _mapper;

        
        public BookService(IBooKUniteOfWork booKUniteOfWork,
            IMapper mapper)
        {
            _booKUniteOfWork = booKUniteOfWork;
            _mapper = mapper;
        }

        public void CreateBook(Book book)
        {

            _booKUniteOfWork.Books.Add(_mapper.Map<Entities.Book>(book));

            _booKUniteOfWork.Save();
        }

    }
}
