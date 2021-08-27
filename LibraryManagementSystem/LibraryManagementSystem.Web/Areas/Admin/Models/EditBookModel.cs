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
    public class EditBookModel
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        public int Id { get; set; }
        public string Title { get; set; }
        public string Barcode { get; set; }
        public double Price { get; set; }
        public EditBookModel()
        {
            _bookService = Startup.AutofacContainer.Resolve<IBookService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public EditBookModel(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }
        public void LoadModelData(int id)
        {
            var course = _bookService.GetBook(id);
            _mapper.Map(course, this);
        }

        internal void Update()
        {
            var book = _mapper.Map<Book>(this);
            _bookService.UpdateBook(book);
        }
    }
}
