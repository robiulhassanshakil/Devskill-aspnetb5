using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.Areas.Admin.Models;

namespace TicketBookingSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            var model = new TicketListModel();
            model.LoadModelData();
            return View(model);
        }
    }
}
