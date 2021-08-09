using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SocialNetwork.Web.Areas.Admin.Models;
using SocialNetwork.Web.Models;

namespace SocialNetwork.Web.Areas.Admin.Controllers
{
      [Area("admin")]
    public class MemberController : Controller
    {

        private readonly ILogger<MemberController> _logger;

        public MemberController(ILogger<MemberController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = new MemberListModel();
            return View(model);
        }
        public JsonResult GetMemberData()
        {
            var DataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = new MemberListModel();
            var data = model.GetMembers(DataTableModel);
            return Json(data);
        }
        public IActionResult Create()
        {
            var model = new CreateMemberModel();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateMemberModel model)
        {
            try
            {
                model.CreateMember();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Failed To Create Ticket");
                _logger.LogError(ex, "Create Ticket Failed");
            }

            return View(model);
        }
    }
}
