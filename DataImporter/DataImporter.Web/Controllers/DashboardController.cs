﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
