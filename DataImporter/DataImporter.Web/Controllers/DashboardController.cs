using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Web.Models.Commons;
using DataImporter.Web.Models.Files;
using DataImporter.Web.Models.GroupModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DataImporter.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
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
            var data = model.GetGroupData(DataTableModel);
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
                    model.CreateGroup();
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

            return RedirectToAction(nameof(ManageGroup));
        }
        public IActionResult GroupDelete(int id)
        {
            var model = new GroupListModel();
            model.Delete(id);
            return RedirectToAction("ManageGroup");
        }
        public IActionResult ViewContacts()
        {
            return View();
        }
        public IActionResult UploadContacts()
        {
            var model = new AllGroupForContacts();
            var gl=model.LoadAllGroup();
            gl.Insert(0, new Group() { Id = 0, Name = "--Select Country Name--" });
            ViewBag.Message = gl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadContacts(IFormFile file, Group group)
        {
            var model = new FileUploadModel();
            model.FileUpload(file, group);

            return RedirectToAction("ViewContacts","Dashboard");
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
