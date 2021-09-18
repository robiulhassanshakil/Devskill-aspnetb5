using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Importing.BusinessObjects;
using DataImporter.Web.Models.Commons;
using DataImporter.Web.Models.Files;
using DataImporter.Web.Models.GroupModel;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            gl.Insert(0, new Group() { Id = 0, Name = "--Select Group Name--" });
            ViewBag.Message = gl;
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadContacts(IFormFile fileSelect, Group group)
        {
            var model = new FileUploadModel();
            model.FileUpload(fileSelect, group);

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

        public async Task<IActionResult> ConvertExcelToJson()
        {
            var inFilePath = @"xx\Wave.xlsx";
            var outFilePath = @"xx\text.json";

            using (var inFile = System.IO.File.Open(inFilePath, FileMode.Open, FileAccess.Read))
            using (var outFile = System.IO.File.CreateText(outFilePath))
            {
                using (var reader = ExcelReaderFactory.CreateReader(inFile, new ExcelReaderConfiguration()
                    { FallbackEncoding = Encoding.GetEncoding(1252) }))
                {
                    var ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });
                    var table = ds.Tables[0];
                    var json = JsonConvert.SerializeObject(table, Formatting.Indented);
                    outFile.Write(json);
                }
            }
            return Ok();
        }

    }
}
