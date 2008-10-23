using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Controllers
{
    public class WorkersController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Title"] = "Darbuotojai";

            return View();
        }

        public ActionResult Form()
        {
            ViewData["Title"] = "Darbuotojas";

            return View();
        }
    }
}
