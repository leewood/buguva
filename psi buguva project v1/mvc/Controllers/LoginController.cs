using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Prisijungimas";
            ViewData["Message"] = "žinutė";

            return View();
        }
    }
}
