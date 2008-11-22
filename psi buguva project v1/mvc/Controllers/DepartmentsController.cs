using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace mvc.Controllers
{
    public class DepartmentsController : Controller
    {
        public ActionResult Index()
        {
            // Add action logic here
            throw new NotImplementedException();
        }

        public ActionResult DepartmentManagerReport(int? startYear, int? startMonth, int? endMonth, int? endYear, int? department_id)
        {
            if (department_id.HasValue)
            {
                return RedirectToAction("List");
            }
            else
            {
                string[] errors = { "Nenurodytas joks skyrius" };
                TempData["errors"] = errors;
                return RedirectToAction("List");                
            }
        }

    }
}
