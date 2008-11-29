using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Common;
using mvc.Models;
using LinqToSqlExtensions;

namespace mvc.Controllers
{
    public class ProjectsController : Common.BaseController
    {
        public ProjectsController()
        {
            ViewData["Image"] = road.img("AllProjects");
            ViewData["Base"] = road.link("Projektų sąrašas", "Projects","");
        }
        
        
        public ActionResult AllProjects(int? startYear, int? startMonth, int? endMonth, int? endYear, bool? chart, int? page, int? pageSize)
        {


            ViewData["Title"] = "Visi projektai";
            int stYear = startYear ?? DateTime.Today.Year;
            int stMonth = startMonth ?? DateTime.Today.Month;
            int enYear = endYear ?? DateTime.Today.Year;
            int enMonth = endMonth ?? DateTime.Today.Month;
            ViewData["startYear"] = stYear;
            ViewData["endYear"] = enYear;
            ViewData["startMonth"] = stMonth;
            ViewData["endMonth"] = enMonth;
            ViewData["chart"] = chart ?? false;
            bool useChart = chart ?? false;
            int currentPage = page ?? 1;
            int currentPageSize = pageSize ?? userSession.ItemsPerPage;
            List<Models.Project> projects = DBDataContext.Projects.Where(p => p.Tasks.Any(t => (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth))).ToList();
            ViewData["pageSize"] = currentPageSize;
            IPagedList<Models.Project> pagedProjects = projects.ToPagedList(currentPage - 1, currentPageSize);
            int size = currentPageSize;
            if (useChart)
            {
                size = projects.Count();
                pagedProjects = projects.ToPagedList(0, size);
            }
            List<Models.DepartmentProjectReport> result = new List<mvc.Models.DepartmentProjectReport>();
            ViewData["page"] = currentPage;


            foreach (Models.Project project in pagedProjects)
            {
                Models.DepartmentProjectReport line = new mvc.Models.DepartmentProjectReport();
                line.Title = project.title;
                line.ManagerID = (project.Worker != null) ? project.project_manager_id.Value : 0;
                if (line.ManagerID != 0)
                {
                    line.DepartmentID = (project.Worker.Department != null) ? project.Worker.Department.id : 0;
                }
                else
                {
                    line.DepartmentID = 0;
                }
                line.ProjectID = project.id;
                line.Manager = (project.Worker != null) ? project.Worker.Fullname : "Nepaskirtas";
                line.ManagerDepartment = (project.Worker != null) ? project.Worker.Department.title : "Nėra";
                line.Started = (project.FirstTask != null) ? new Models.MonthOfYear(project.FirstTask.year, project.FirstTask.month) : null;
                line.Ended = (project.LastTask != null) ? new Models.MonthOfYear(project.LastTask.year, project.LastTask.month) : null;
                line.TotalWorked = project.Tasks.Where(t => (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth)).Sum(t2 => t2.worked_hours);
                result.Add(line);
            }
            IPagedList<Models.DepartmentProjectReport> paged = result.ToPagedList(currentPage - 1, currentPageSize);
            if (useChart)
            {
                paged = result.ToPagedList(0, size);
            }
            ViewData["pageCount"] = paged.PageCount;
            return View(result);
        }

        public ActionResult GrandMastersReport(int? startYear, int? startMonth, int? endMonth, int? endYear, bool? chart)
        {
            ViewData["Title"] = "Vadovybės ataskaita";
            Models.DepartmentManagerReport report = new mvc.Models.DepartmentManagerReport();
            report.DepartmentInfo = null;
            report.ShowAsChart = chart ?? false;
            int stYear = startYear ?? DateTime.Today.Year;
            int stMonth = startMonth ?? DateTime.Today.Month;
            int enYear = endYear ?? DateTime.Today.Year;
            int enMonth = endMonth ?? DateTime.Today.Month;
            ViewData["startYear"] = stYear;
            ViewData["endYear"] = enYear;
            ViewData["startMonth"] = stMonth;
            ViewData["endMonth"] = enMonth;
            ViewData["chart"] = chart ?? false;
            report.Period = new mvc.Models.Period(stYear, stMonth, enYear, enMonth);
            report.WorkersCount = DBDataContext.Workers.Count();
            System.Data.Linq.EntitySet<Models.Project> myProjects = new System.Data.Linq.EntitySet<mvc.Models.Project>();
            if (DBDataContext.Departments.Any())
            {
                List<Models.Department> departments = DBDataContext.Departments.ToList();
                List<Models.Task> myTasks = DBDataContext.Tasks.Where(t => (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth)).ToList();
                report.TotalDepartmentWorked = myTasks.Sum(t => t.worked_hours);
                foreach (Models.Department department in departments)
                {
                    if (department.Workers.Any())
                    {
                        IEnumerable<Models.Task> tasks = myTasks.Where(t => (department.Workers.Contains(t.Worker)));
                        report.WorkedHoursOfOthers.Add(new mvc.Models.AssociatedWorkedHours(department.title, tasks.Sum(t => t.worked_hours), department.id));
                    }
                }
            }

            return View(report);
        }


        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult ListMyProjects(int? page, int? id)
        {

            ViewData["Image"] = road.img("MyProjects");
            
            int workerID = id ?? userSession.workerID;
            if (workerID == userSession.workerID)
            {
                ViewData["Title"] = "Mano Projektai";
            }
            else
            {
                ViewData["Title"] = "Darbuotojo " + id.Value.ToString() + " projektai";
            }

            ViewData["currentWorkerID"] = workerID;
            int currentPage = (page.HasValue) ? page.Value: 1;
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            IEnumerable<Models.Project> projects = DBDataContext.Projects;
            if (projects.Count() > 0)
            {
                projects = projects.Where(project => ((project.Tasks.Count > 0) && (project.Tasks.Any(task => task.project_participant_id == workerID))) || (project.project_manager_id == workerID));
            }
            return View(projects.ToPagedList(currentPage - 1, 25));
        }

        public ActionResult ProjectIntensivityReport(int? project_id, int? page)
        {
            if (project_id.HasValue)
            {
                ViewData["Image"] = road.img("ManagerReport");
                ViewData["Base"] = road.link("Mano Projektai", "Projects", "ListMyProjects");

                ViewData["project_id"] = project_id;
                ViewData["Title"] = "Projekto #" + project_id.Value.ToString() + " intensyvumas";
                int curPage = page ?? 1;
                ViewData["curPage"] = curPage;
                ViewData["itemsPerPage"] = userSession.ItemsPerPage;
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
                ViewData["Image"] = road.img("ManagerReport");
                ViewData["Base"] = road.link("Mano Projektai", "Projects", "ListMyProjects");
                ViewData["Title"] = "Vadovo ataskaita";
                
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

        public ActionResult ListMyTasksInProject(int? page, int? project_id, int? year, int? month, int? id)
        {
            if (project_id.HasValue)
            {
                ViewData["Image"] = road.img("Tasks");
                ViewData["Base"] = road.link("Mano Projektai", "Projects", "ListMyProjects");
                
                int workerID = id ?? userSession.workerID;
                if (workerID == userSession.workerID)
                {
                    ViewData["Title"] = "Mano užduotys projekte";
                }
                else
                {
                    ViewData["Title"] = "Darbuotojo " + id.Value.ToString() + " užduotys projekte";
                }
                
                int project = project_id.Value;
                
                List<Models.MonthOfYear> months = WorkerInformation.workedMonthsInProject(project, workerID);
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
                    tasks = DBDataContext.Tasks.Where(task => ((task.project_id == project_id) && (task.year == yearToUse) && (task.month == monthToUse) && (task.project_participant_id == workerID))).ToList();
                }
                if (tasks == null) tasks = new List<mvc.Models.Task>();
                return View(new Models.TasksAndMonths(tasks.ToPagedList(currentPage - 1, 25), months, new Models.MonthOfYear(yearToUse, monthToUse), project));
            }
            else
            {
                return RedirectToAction("ListMyProjects");
            }
        }

        public ActionResult New()
        {
            Project project = ((Project)TempData["project"] ?? new Project());
            ViewData["Title"] = "Kuriamas naujas projektas";
            return View(project);
        }

        public ActionResult Insert()
        {
            Project project = DBDataContext.CreateEntityFromForm<Project>(Request.Form);
            var errors = project.Validate();
            if (errors != null)
            {
                TempData["errors"] = errors.ErrorMessages;
                TempData["project"] = project;

                return RedirectToAction("New");
            }
            else
            {
                DBDataContext.Projects.InsertOnSubmit(project);
                DBDataContext.SubmitChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Project project = null;
                try
                {
                    project = (Project)TempData["project"] ?? DBDataContext.Projects.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (project != null)
                {
                    ViewData["Title"] = "Koreguojamas projektas #" + project.id.ToString() + "(" + project.title + ")";
                    return View(project);
                }
                else
                {
                    string[] errors = { "Bandoma koreguoti neegzistuojantį projektą" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks projektas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult List(int? page)
        {
            ViewData["Title"] = "Projektų sąrašas";
            return View(DBDataContext.Projects.Where(w => (w.deleted.HasValue == false)).ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
        }

        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Project project = DBDataContext.CreateEntityFromForm<Project>(Request.Form);
                project.id = id.Value;
                var errors = project.Validate();
                if (errors != null)
                {
                    TempData["errors"] = errors.ErrorMessages;
                    TempData["project"] = project;
                    return RedirectToAction("Edit", new { id = project.id });
                }
                else
                {
                    DBDataContext.Update<Project>(Request.Form, id.Value);
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks projektas" };
                TempData["errors"] = errors;
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Project project = null;
                try
                {
                    project = DBDataContext.Projects.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (project != null)
                {
                    project.deleted = DateTime.Today;
                    if (userSession.userId != 0)
                    {
                        project.deleted_by_id = userSession.userId;
                    }
                    DBDataContext.SubmitChanges();
                }
                else
                {
                    string[] errors = { "Bandoma trinti neegzistuojantį projektą" };
                    TempData["errors"] = errors;
                }
                return RedirectToAction("List");
            }
            else
            {
                string[] errors = { "Nenurodytas joks projektas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }
    }
}
