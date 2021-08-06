using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketBookingSystem.Areas.Admin.Models;

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
            model.LoadModelData();
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateTicketModel();

            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
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
                    ModelState.AddModelError("","Failed To Create Ticket");
                    _logger.LogError(ex, "Create Ticket Failed");
                }    
            }
            

            return View(model);
        }

    }
}
