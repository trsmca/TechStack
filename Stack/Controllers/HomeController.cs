using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Web.Mvc;
using Stack.DBModels;
using Stack.Models;
using System.IO.Compression;
using System.IO;
using System.Collections.Generic;

namespace Stack.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //foreach (string file in Directory.EnumerateFiles("C:\\Raja Sekhar\\Projects\\barter\\web\\", "*"))
            //{
            //    string contents = System.IO.File.ReadAllText(file);
            //}

            return View();
        }
         
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SuccessMessage()
        {
            ViewBag.Message = "Your contact page.";

            return View("SuccessMessage");
        }
    }
}