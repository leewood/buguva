using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class HomeController : Common.BaseController
    {
        public ActionResult Index()
        {
            bool installResult = DBDataContext.Install();
            string[] errors = { "Duomenų bazė nerasta ir jos sukurti nepavyko. Pasitikrinkite nustatymus." };
            TempData["errors"] = errors;            
            //ViewData["Message"] = "Welcome to ASP.NET MVC!";
            return RedirectToAction("ListMyProjects", "Projects");
        }

        public ActionResult NoPermissions()
        {
            ViewData["Title"] = "Nėra teisių atlikti šią operaciją";
            ViewData["Message"] = "Nėra teisių atlikti šią operaciją";
            return View();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }

        public ActionResult Example()
        {
            ViewData["Title"] = "About Page";

            return View();
        }
    }
}
