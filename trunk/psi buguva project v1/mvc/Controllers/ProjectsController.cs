using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Common;

namespace mvc.Controllers
{
    public class ProjectsController : Common.BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListMyProjects(int? page)
        {
            ViewData["Title"] = "Mano Projektai";
            int currentPage = (page.HasValue) ? page.Value : 1;
            Models.POADataModelsDataContext data = new mvc.Models.POADataModelsDataContext();
            IEnumerable<Models.Project> projects = data.Projects;
            if (projects.Count() > 0)
            {
                projects = projects.Where(project => (project.Tasks.Where(task => task.project_participant_id == MyWorkerID).Count() > 0) || (project.project_manager_id == MyWorkerID));
            }
            return View(projects.ToPagedList(currentPage, 25));
        }
    }
}
