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
        public JsonResult GetBookData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = new BookListModel();
            var data = model.GetBooks(dataTableModel);
            return Json(data);
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
        public IActionResult Edit(int id)
        {
            var model = new EditBookModel();
            model.LoadModelData(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditBookModel model)
        {
            if (ModelState.IsValid)
            {
                model.Update();
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var model = new BookListModel();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
