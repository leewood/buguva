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
            int currentPage = (page.HasValue) ? page.Value: 1;
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            IEnumerable<Models.Project> projects = DBDataContext.Projects;
            if (projects.Count() > 0)
            {
                projects = projects.Where(project => ((project.Tasks.Count > 0) && (project.Tasks.Where(task => task.project_participant_id == this.userSession.workerID).Count() > 0)) || (project.project_manager_id == userSession.workerID));
            }
            return View(projects.ToPagedList(currentPage - 1, 25));
        }

        public ActionResult ProjectIntensivityReport(int? project_id)
        {
            if (project_id.HasValue)
            {
                ViewData["project_id"] = project_id;
                Models.Project myProject = DBDataContext.Projects.Where(project => project.id == project_id.Value).First();
                List<Models.MonthOfYear> months = myProject.workedMonthsInProject();
                List<Models.ProjectIntensivity> result = new List<mvc.Models.ProjectIntensivity>();
                foreach (Models.MonthOfYear month in months)
                {
                    int total = myProject.Tasks.Where(task => (task.year == month.Year) && (task.month == month.Month)).Sum(t => t.worked_hours);
                    int myWorkersWorked = 0;
                    try
                    {
                        myWorkersWorked = myProject.Tasks.Where(task => (task.year == month.Year) && (task.month == month.Month) && (task.Worker.department_id == myProject.Worker.department_id)).Sum(t => t.worked_hours);
                    }
                    catch (Exception)
                    {
                    }
                    result.Add(new mvc.Models.ProjectIntensivity(month, myProject.id, total, myWorkersWorked));
                }
                return View(result);
            }
            else
            {
                return RedirectToAction("ListMyProjects");
            }

        }

        public ActionResult ProjectManagerReport(int? project_id)
        {
            if (project_id.HasValue)
            {

                Models.Project myProject = DBDataContext.Projects.Where(project => project.id == project_id.Value).First();
                System.Collections.Generic.List<Models.Department> departments = DBDataContext.Departments.Where(department =>(department.Workers.Count > 0) && (department.Workers.Where(worker => ((((worker.Tasks.Count > 0) && (worker.Tasks.Where(task=> task.project_id == project_id.Value).Count() > 0))) || (myProject.project_manager_id == worker.id))).Count() > 0)).ToList();
                int totalProjectHours = 0;
                System.Collections.Generic.List<Models.DepartmentInfoForProject> departmentsInfo = new List<mvc.Models.DepartmentInfoForProject>();
                int totalCountOfWorkers = 0;
                int firstDepartment = 0;
                if (myProject.Worker != null)
                {
                    firstDepartment = myProject.Worker.department_id.Value;
                }
                int firstDepartmentIndex = -1;
                for (int i = 0; i < departments.Count; i++)
                {
                    if (departments[i].id == firstDepartment)
                    {
                        firstDepartmentIndex = i;
                    }
                }
                if (firstDepartmentIndex >= 0)
                {

                    for (int i = firstDepartmentIndex; i > 0; i--)
                    {
                        Models.Department temp = departments[i - 1];
                        departments[i - 1] = departments[i];
                        departments[i] = temp;
                    }
                }

                foreach (Models.Department department in departments)
                {
                    System.Collections.Generic.List<Models.Worker> workers = department.Workers.Where(worker => ((worker.hasTasksInProject(project_id.Value)) || (myProject.project_manager_id == worker.id))).ToList();
                    System.Collections.Generic.List<Models.WorkerAndHours> workersResult = new List<mvc.Models.WorkerAndHours>();
                    int totalDepartmentHours = 0;
                    foreach (Models.Worker worker in workers)
                    {
                        int hours = 0;
                        if (worker.Tasks.Where(t => t.project_id == project_id.Value).Count() > 0)
                        {
                            hours = worker.Tasks.Where(t => t.project_id == project_id.Value).Sum(task => task.worked_hours);
                        }
                        else
                        {
                            hours = 0;
                        }
                        totalDepartmentHours += hours;
                        workersResult.Add(new mvc.Models.WorkerAndHours(worker, hours));
                    }
                    departmentsInfo.Add(new mvc.Models.DepartmentInfoForProject(workersResult, department, totalDepartmentHours));
                    totalCountOfWorkers += workers.Count;
                    totalProjectHours += totalDepartmentHours;
                }
                Models.ProjectManagerReportInfo projectManagerReport = new mvc.Models.ProjectManagerReportInfo(myProject, totalProjectHours, departmentsInfo, totalCountOfWorkers);
                return View(projectManagerReport);
            }
            else
            {
                return RedirectToAction("ListMyProjects");
            }
        }

        public ActionResult ListMyTasksInProject(int? page, int? project_id, int? year, int? month)
        {
            if (project_id.HasValue)
            {
                int project = project_id.Value;
                List<Models.MonthOfYear> months = WorkerInformation.workedMonthsInProject(project);
                List<Models.Task> tasks = new List<mvc.Models.Task>();
                int currentPage = (page.HasValue) ? page.Value : 1;
                if (currentPage < 1)
                {
                    currentPage = 1;
                }
                int monthToUse = 0;
                int yearToUse = 0;
                if ((!year.HasValue) || (!month.HasValue))
                {
                    if (months.Count > 0)
                    {
                        yearToUse = months[0].Year;
                        monthToUse = months[0].Month;
                    }
                }
                else
                {
                    monthToUse = month.Value;
                    yearToUse = year.Value;
                }
                if (monthToUse + yearToUse > 0)
                {
                    tasks = DBDataContext.Tasks.Where(task => ((task.project_id == project_id) && (task.year == yearToUse) && (task.month == monthToUse))).ToList();
                }
                if (tasks == null) tasks = new List<mvc.Models.Task>();
                return View(new Models.TasksAndMonths(tasks.ToPagedList(currentPage - 1, 25), months, new Models.MonthOfYear(yearToUse, monthToUse), project));
            }
            else
            {
                return RedirectToAction("ListMyProjects");
            }
        }
    }
}
