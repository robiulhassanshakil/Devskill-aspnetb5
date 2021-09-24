using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Membership.Entities;
using DataImporter.Web.Models.Commons;
using DataImporter.Web.Models.Contact;
using DataImporter.Web.Models.Files;
using DataImporter.Web.Models.GroupModel;
using ExcelDataReader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DataImporter.Web.Controllers
{
    [Authorize(Policy = "RestrictedArea")]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ILogger<DashboardController> logger, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManageGroup()
        {
            return View();
        }
        public JsonResult GetGroupData()
        {
            var DataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = new GroupListModel();
            var applicationuser = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var data = model.GetGroupData(DataTableModel, applicationuser);
            return Json(data);
        }
        public IActionResult CreateGroup()
        {
           
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult CreateGroup(CreateGroupModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                   var applicationuser= Guid.Parse(_userManager.GetUserId(HttpContext.User));
                    model.CreateGroup(applicationuser);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create Group");
                    _logger.LogError(ex, "Create Group Failed");
                }
            }
            return View(model);
        }
        public IActionResult GroupEdit(int id)
        {
            var model = new GroupEditModel();
            model.LoadModelData(id);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult GroupEdit(GroupEditModel model)
        {

            if (ModelState.IsValid)
            {
                model.Update();
            }

            return RedirectToAction("ManageGroup");
        }
        public IActionResult GroupDelete(int id)
        {
            var model = new GroupListModel();
            model.Delete(id);
            return RedirectToAction("ManageGroup");
        }
        public IActionResult ViewContactsStatus()
        {
            return View();
        }
        public JsonResult ViewContactsGetData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = new ContactListModel();
            var applicationuser = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            var data = model.LoadData(dataTableModel, applicationuser);
            return Json(data);
        }
        public IActionResult UploadContacts()
        {
            var model = new AllGroupForContacts();
            var applicationuser = Guid.Parse(_userManager.GetUserId(HttpContext.User));
            model.LoadAllGroup(applicationuser);
            
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UploadContacts(IFormFile fileSelect, AllGroupForContacts allGroupForContacts)
        {
            var model = new FileUploadModel();
            model.FileUpload(fileSelect, allGroupForContacts);

            return View();
        }

        public IActionResult SendMailContacts()
        {
            return View();
        }
        public IActionResult DownloadContacts()
        {
            return View();
        }


    }
}
