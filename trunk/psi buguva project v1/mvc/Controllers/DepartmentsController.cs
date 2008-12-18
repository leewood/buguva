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
    public class DepartmentsController : Common.BaseController
    {
        public DepartmentsController()
        {
            ViewData["Image"] = road.img("Skyriai");
            ViewData["Base"] = road.link("Skyrių sąrašas", "Departments","");
        }
        
        public ActionResult Index()
        {            
            return RedirectToAction("List");
        }

        public ActionResult DepartmentProjects(int? startYear, int? startMonth, int? endMonth, int? endYear, int? department_id, bool? chart, int? page, int? pageSize, bool? showOnlyMyProjects, string filter, string sorting)
        {
            if (department_id.HasValue)
            {
                ViewData["Image"] = road.img("Department");
                
                Models.Department currentDepartment = null;
                if (DBDataContext.Departments.Any())
                {
                    try
                    {
                        currentDepartment = DBDataContext.Departments.First(d => d.id == department_id.Value);
                    }
                    catch (Exception)
                    {
                    }
                    if (currentDepartment != null)
                    {
                        if (!currentDepartment.canBeSeen())
                        {
                            return RedirectToAction("NoPermissions", "Home");
                        }
                        ViewData["Title"] = "Skyriaus " + currentDepartment.title + " projektai";
                        ViewData["department_id"] = department_id.Value;
                        ViewData["viewOnlyMy"] = showOnlyMyProjects ?? false;
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
                        bool dontShowAll = showOnlyMyProjects ?? false;
                        int currentPage = page ?? 1;                        
                        int currentPageSize = pageSize ?? userSession.ItemsPerPage;
                        ViewData["pageSizeExt"] = currentPageSize;
                        ViewData["pageExt"] = currentPage;
                        Period period = new Period(stYear, stMonth, enYear, enMonth);
                        string[] errors = period.getErrors().ToArray();
                        List<Models.DepartmentProjectReport> result = new List<mvc.Models.DepartmentProjectReport>();
                        if (errors.Length > 0)
                        {
                            TempData["errors"] = errors;
                            return View(result);
                        }
                        List<Models.Project> projects = DBDataContext.Projects.Where(p => p.Tasks.Any(t => (t.Worker.department_id == department_id.Value) && (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth))).ToList();
                        IOrderedEnumerable<Models.Project> orderedProjects = projects.OrderBy(p => p, new Models.MyDepartmentFirstComparer(department_id.Value));
                        
                        if (dontShowAll)
                        {
                            orderedProjects = orderedProjects.Where(o => currentDepartment.Workers.Contains(o.Worker)).OrderBy(o => o.id);
                        }
                        IPagedList<Models.Project> pagedProjects = null;
                        int size = currentPageSize;
                        if (orderedProjects.Count() > 0)
                        {                            
                            if (useChart)
                            {
                                size = orderedProjects.Count();
                                pagedProjects = orderedProjects.ToPagedList(0, size);
                            }
                            else
                            {
                                pagedProjects = orderedProjects.ToPagedList(currentPage - 1, currentPageSize);
                            }
                        }
                        else
                        {
                            pagedProjects = (new List<Models.Project>()).ToPagedList(0, size);
                        }
                        ViewData["pageCountExt"] = pagedProjects.PageCount;                                                                                               
                        foreach (Models.Project project in pagedProjects)
                        {
                            Models.DepartmentProjectReport line = new mvc.Models.DepartmentProjectReport();
                            line.Title = project.title;
                            line.Manager = (project.Worker != null) ? project.Worker.Fullname : "Nepaskirtas";
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
                            line.ManagerDepartment = (project.Worker != null) ? project.Worker.Department.title : "Nėra";
                            line.Started = (project.FirstTask != null)?new Models.MonthOfYear(project.FirstTask.year, project.FirstTask.month):null;
                            line.Ended = (project.LastTask != null) ? new Models.MonthOfYear(project.LastTask.year, project.LastTask.month) : null;
                            line.TotalWorked = project.Tasks.Where(t => (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth)).Sum(t2 => t2.worked_hours);
                            line.DepartmentWorkersWorked = project.Tasks.Where(t => (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth) && (currentDepartment.Workers.Contains(t.Worker))).Sum(t2 => t2.worked_hours);
                            result.Add(line);
                        }
                        result = filteredAndSorted<Models.DepartmentProjectReport>(result, filter, sorting);
                        IPagedList<Models.DepartmentProjectReport> paged = null;
                        if (result.Count > 0)
                        {
                            
                            if (useChart)
                            {
                                paged = result.ToPagedList(0, size);
                            }
                            else
                            {
                                paged = result.ToPagedList(currentPage - 1, currentPageSize); 
                            }
                        }
                        else
                        {
                            paged = (new List<Models.DepartmentProjectReport>()).ToPagedList(0, 1);
                        }
                        /*
                        if (dontShowAll)
                        {
                            result = result.Where(r => r.Manager == currentDepartment.Worker.Fullname).ToList();
                        }
                         */
                        return View(result);
                    }
                    else
                    {
                        string[] errors = { "Nurodytas skyrius nerastas" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }

                }
                else
                {
                    string[] errors = { "Nėra jokių skyrių" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks skyrius" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }

        }

        public ActionResult DepartmentManagerReport(int? startYear, int? startMonth, int? endMonth, int? endYear, int? department_id, bool? chart)
        {
            if (department_id.HasValue)
            {
                ViewData["Image"] = road.img("Department");
                
                Models.Department currentDepartment = null;
                if (DBDataContext.Departments.Any())
                {
                    try
                    {
                        currentDepartment = DBDataContext.Departments.First(d => d.id == department_id.Value);
                    }
                    catch (Exception)
                    {
                    }
                    if (currentDepartment != null)
                    {
                        if (!currentDepartment.canBeSeen())
                        {
                            return RedirectToAction("NoPermissions", "Home");
                        }
                        ViewData["Title"] = "Skyriaus " + currentDepartment.title + " vadovo ataskaita";
                        ViewData["department_id"] = department_id.Value;                        
                        Models.DepartmentManagerReport report = new mvc.Models.DepartmentManagerReport();
                        report.DepartmentInfo = currentDepartment;
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
                        string[] errors = report.Period.getErrors().ToArray();
                        if (errors.Length > 0)
                        {
                            TempData["errors"] = errors;
                            return View(report);
                        }
                        report.WorkersCount = currentDepartment.Workers.Count;

                        List<Models.Project> myProjects = new List<Project>();
                        if (currentDepartment.Worker != null)
                        {
                            myProjects = DBDataContext.Projects.Where(p => currentDepartment.Workers.Contains(p.Worker)).ToList();
                            if (myProjects.Any())
                            {
                                IEnumerable<Models.Task> myProjectTasks = DBDataContext.Tasks.Where(t => (myProjects.Contains(t.Project)) && (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth));
                                if (DBDataContext.Departments.Any())
                                {
                                    List<Models.Department> departments = DBDataContext.Departments.Where(d => d.id != department_id.Value).ToList();
                                    foreach (Models.Department department in departments)
                                    {
                                        if (department.Workers.Any())
                                        {
                                            IEnumerable<Models.Task> myTasks = myProjectTasks.Where(t => (department.Workers.Contains(t.Worker)));
                                            if (myTasks.Count() > 0)
                                            {
                                                report.WorkedHoursOfOthers.Add(new mvc.Models.AssociatedWorkedHours(department.title, myTasks.Sum(t => t.worked_hours), department.id));
                                            }
                                        }
                                    }
                                }
                                if (currentDepartment.Workers.Any())
                                {
                                    IEnumerable<Models.Task> myWorkersTasks = DBDataContext.Tasks.Where(t => (currentDepartment.Workers.Contains(t.Worker)) && (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth));
                                    IEnumerable<Models.Task> myWorkersTasksOfMyProjects = myWorkersTasks.Where(t => (myProjects.Any()) && (myProjects.Contains(t.Project)));
                                    if (myWorkersTasks.Any())
                                    {
                                        report.TotalDepartmentWorked = myWorkersTasks.Sum(t => t.worked_hours);
                                    }
                                    if (myWorkersTasksOfMyProjects.Any())
                                    {
                                        report.ThisDepartmentWorkersWorkedInDepartmentProjects = myWorkersTasksOfMyProjects.Sum(t => t.worked_hours);
                                    }
                                }

                            }
                        }
                        return View(report);
                    }
                    else
                    {
                        string[] errors = { "Nurodytas skyrius nerastas" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
                    
                }
                else
                {
                    string[] errors = { "Nėra jokių skyrių" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks skyrius" };
                TempData["errors"] = errors;
                return RedirectToAction("List");                
            }
        }

        public ActionResult New()
        {
            if (Department.administrationNew())
            {
                Department department = ((Department)TempData.getAndRemove("department") ?? new Department());
                ViewData["TitleWindow"] = "Kuriamas naujas skyrius";
                return View(department);
            }
            else
            {
                string[] errors = { "Neturite teisių kurti naują skyrių" };
                TempData["errors"] = errors;
                TempData.Remove("department");
                return RedirectToAction("List");
            }
        }

        public ActionResult Insert()
        {
            Department department = DBDataContext.CreateEntityFromForm<Department>(Request.Form);
            var errors = department.Validate();
            if (errors != null)
            {
                TempData["errors"] = errors.ErrorMessages;
                TempData["department"] = department;

                return RedirectToAction("New");
            }
            else
            {
                DBDataContext.Departments.InsertOnSubmit(department);
                DBDataContext.SubmitChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult List(int? page, string filter, string sorting)
        {
            ViewData["Title"] = "Skyrių sąrašas";
            List<Department> departments = DBDataContext.Departments.Where(w => (w.deleted.HasValue == false)).ToList();
            departments = departments.Where(d => d.administationView()).ToList();
            departments = filteredAndSorted<Department>(departments, filter, sorting);
            return View(departments.ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Department department = null;
                try
                {
                    department = (Department)TempData.getAndRemove("department") ?? DBDataContext.Departments.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (department != null)
                {
                    if (department.administrationEdit())
                    {
                        ViewData["TitleWindow"] = "Koreguojamas skyrius #" + department.id.ToString() + "(" + department.title + ")";
                        return View(department);
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių koreguoti šį skyrių" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
                }
                else
                {
                    string[] errors = { "Bandoma koreguoti neegzistuojantį skyrių" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks skyrius" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Department department = DBDataContext.CreateEntityFromForm<Department>(Request.Form);
                department.id = id.Value;
                var errors = department.Validate();
                if (errors != null)
                {
                    TempData["errors"] = errors.ErrorMessages;
                    TempData["project"] = department;
                    return RedirectToAction("Edit", new { id = department.id });
                }
                else
                {
                    DBDataContext.Update<Department>(Request.Form, id.Value);
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks skyrius" };
                TempData["errors"] = errors;
            }
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Department department = null;
                try
                {
                    department = DBDataContext.Departments.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (department != null)
                {
                    if (department.administrationDelete())
                    {
                        if (department.Workers.Count > 0)
                        {
                            string[] errors = { "Negalima ištrinti, nes šis skyrius turi jam priklausančių darbuotojų." };
                            TempData["errors"] = errors;
                            return RedirectToAction("List");
                        }
                        department.makeBackup(userSession.userId);                        
                        DBDataContext.Departments.DeleteOnSubmit(department);
                        DBDataContext.SubmitChanges();
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių ištrinti šio skyriaus" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
                }
                else
                {
                    string[] errors = { "Bandoma trinti neegzistuojantį skyrių" };
                    TempData["errors"] = errors;
                }
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
