using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class ExampleController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Example page";
            ViewData["Message"] = "Hi";

            return View();
        }

        public ActionResult About()
        {
            ViewData["Title"] = "About Page";

            return View();
        }
    }
}
