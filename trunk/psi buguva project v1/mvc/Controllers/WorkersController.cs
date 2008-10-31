using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using LinqToSqlExtensions;

namespace mvc.Controllers
{
    public class WorkersController : Controller
    {   
        
        public ActionResult Index()
        {
            ViewData["Title"] = "Darbuotojai";
            mvc.Models.POADataModelsDataContext data = new mvc.Models.POADataModelsDataContext();//System.Configuration.ConfigurationManager.ConnectionStrings["ProjectDatabaseConnection"].ConnectionString);            
            ViewResult result = View(data.GetWorkers());
            
           
            return result;
        }

        public ActionResult Form()
        {
            ViewData["Title"] = "Darbuotojas";
            
            return View(new Worker());
        }


        public ActionResult Insert(NameValueCollection formParams)
        {
            mvc.Models.POADataModelsDataContext data = new POADataModelsDataContext();
            data.Insert<Worker>(this.Request.Params);
            //data.AddWorker((Worker)ViewData.Model);
            return RedirectToAction("Index");
        }

        public ActionResult IndexRedirect()
        {
            return RedirectToAction("Index");
        }

    }
}
