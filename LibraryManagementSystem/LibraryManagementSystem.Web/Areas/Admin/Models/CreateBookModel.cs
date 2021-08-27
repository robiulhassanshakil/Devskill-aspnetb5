using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using LibraryManagementSystem.Publishing.BusinessObjects;
using LibraryManagementSystem.Publishing.Services;

namespace LibraryManagementSystem.Web.Areas.Admin.Models
{
    public class CreateBookModel
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public string Title { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public CreateBookModel()
        {
            _bookService = Startup.AutofacContainer.Resolve<IBookService>();
            _mapper= Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateBookModel(IBookService bookService,IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        internal void CreateBook()
        {
            var book = _mapper.Map<Book>(this);

            _bookService.CreateBook(book);
        }
    }
}
