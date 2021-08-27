using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Web.Areas.Admin.Models;
using LibraryManagementSystem.Web.Models;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var model = new CreateBookModel();
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateBookModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateBook();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed To Create Book");
                    _logger.LogError(ex, "Create Book Failed");
                }
            }

            return View(model);
        }
       
    }
}
