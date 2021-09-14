using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataImporter.Web.Models.Files;
using Microsoft.AspNetCore.Http;

namespace DataImporter.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateGroup()
        {
            return View();
        }
        public IActionResult ManageGroup()
        {
            return View();
        }
        public IActionResult ViewContacts()
        {
            return View();
        }
        public IActionResult UploadContacts()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadContacts(IFormFile file)
        {
            var model = new FileUploadModel();
            model.FileUpload(file);

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
