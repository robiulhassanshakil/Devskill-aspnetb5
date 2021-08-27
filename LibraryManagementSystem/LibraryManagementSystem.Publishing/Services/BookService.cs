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

        public void DeleteBook(int id)
        {
            _booKUniteOfWork.Books.Remove(id);
            _booKUniteOfWork.Save();
        }

       

        public Book GetBook(int id)
        {
            var book = _booKUniteOfWork.Books.GetById(id);

            if (book == null) return null;

            return _mapper.Map<Book>(book);
        }

        public (IList<Book> records, int total, int totalDisplay) GetBooks(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var bookData = _booKUniteOfWork.Books.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Title.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from book in bookData.data
                select _mapper.Map<Book>(book)).ToList();

            return (resultData, bookData.total, bookData.totalDisplay);
        }

        public void UpdateBook(Book book)
        {
            var bookEntity = _booKUniteOfWork.Books.GetById(book.Id);

            if (bookEntity != null)
            {
                _mapper.Map(book, bookEntity);
                _booKUniteOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find Book");
        }
    }
}
