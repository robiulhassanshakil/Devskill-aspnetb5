using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using TicketBookingSystem.Areas.Admin.Models;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;

        public TicketController(ILogger<TicketController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new TicketListModel();
            return View(model);
        }

        public JsonResult GetTicketData()
        {
            var DataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = new TicketListModel();
            var data = model.GetTicketData(DataTableModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            var model = new CreateTicketModel();

            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateTicketModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CreateTicket();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed To Create Ticket");
                    _logger.LogError(ex, "Create Ticket Failed");
                }
            }


            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var model = new EditTicketModel();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(EditTicketModel model)
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
            var model = new TicketListModel();
            
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }

}
