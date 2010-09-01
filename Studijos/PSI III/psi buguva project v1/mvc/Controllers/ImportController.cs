using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace mvc.Controllers
{
    public class ImportController : Common.BaseController
    {
        public ImportController()
        {
            ViewData["Image"] = road.img("Inport");
            ViewData["Base"] = road.link("Duomenų importavimas", "Import", "");
            ViewData["Title"] = "Duomenų importavimas";
        }
        
        public ActionResult Index()
        {

            return View();
        }


    }
}
