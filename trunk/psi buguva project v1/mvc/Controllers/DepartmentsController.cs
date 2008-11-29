using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Common;
using mvc.Models;
using LinqToSqlExtensions;

namespace mvc.Models
{   
    
    public class AssociatedWorkedHours
    {
        private int _hours = 0;
        private string _title = "";
        public int Hours
        {
            get
            {
                return _hours;
            }
            set
            {
                _hours = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        private int _associationID = 0;

        public int AssociationID
        {
            get
            {
                return _associationID;
            }
            set
            {
                _associationID = value;
            }

        }

        public AssociatedWorkedHours(string title, int hours, int associationID)
        {
            Title = title;
            Hours = hours;
            AssociationID = associationID;
        }
        
    }

    public class Period
    {
        private MonthOfYear _start;
        private MonthOfYear _end;
        List<string> errors = new List<string>();

        private MonthOfYear PeriodStart
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
            }
        }

        private MonthOfYear PeriodEnd
        {
            get
            {
                return _end;
            }
            set
            {
                _end = value;
            }
        }

        private void validatePeriod()
        {
            if (PeriodStart.Year < 1970)
            {
                errors.Add("Metai negali būti mažesni nei 1970. Norėta pasirinkti " + PeriodStart.Year.ToString());
                PeriodStart.Year = DateTime.Today.Year;
                
            }
            if (PeriodEnd.Year < 1970)
            {
                errors.Add("Metai negali būti mažesni nei 1970. Norėta pasirinkti " + PeriodEnd.Year.ToString());
                PeriodEnd.Year = DateTime.Today.Year;                
            }

            if ((PeriodStart.Month < 1) || (PeriodStart.Month > 12))
            {
                errors.Add("Mėnuo gali būti tik intervale 1..12. Pasirinkta " + PeriodStart.Month.ToString());
                PeriodStart.Month = DateTime.Today.Month;                
            }
            if ((PeriodEnd.Month < 1) || (PeriodEnd.Month > 12))
            {
                errors.Add("Mėnuo gali būti tik intervale 1..12. Pasirinkta " + PeriodStart.Month.ToString());
                PeriodEnd.Month = DateTime.Today.Month;                
            }
            if (PeriodStart.Year * 12 + PeriodStart.Month > PeriodEnd.Year * 12 + PeriodEnd.Month)
            {
                errors.Add("Pradžios data negali būti didesnė nei pabaigos data");
                PeriodEnd.Year = PeriodStart.Year;
                PeriodEnd.Month = PeriodStart.Month;                
            }
            if (PeriodStart.Year * 12 + PeriodStart.Month > DateTime.Today.Year * 12 + DateTime.Today.Month)
            {
                errors.Add("Data negali viršyti šio mėnesio datos");
                PeriodStart.Year = DateTime.Today.Year;
                PeriodStart.Month = DateTime.Today.Month;                
            }
            if (PeriodEnd.Year * 12 + PeriodEnd.Month > DateTime.Today.Year * 12 + DateTime.Today.Month)
            {
                errors.Add("Data negali viršyti šio mėnesio datos");
                PeriodEnd.Year = DateTime.Today.Year;
                PeriodEnd.Month = DateTime.Today.Month;                
            }

        }

        public Period(MonthOfYear start, MonthOfYear end)
        {
            PeriodStart = start;
            PeriodEnd = end;
            validatePeriod();
        }

        public Period(int startYear, int startMonth, int endYear, int endMonth)
        {
            PeriodStart = new MonthOfYear(startYear, startMonth);
            PeriodEnd = new MonthOfYear(endYear, endMonth);
            validatePeriod();
        }


        public List<string> getErrors()
        {
            return errors;
        }

        private double DateDiff(string howtocompare, System.DateTime startDate, System.DateTime endDate) 
        {
            double diff=0;
            System.TimeSpan TS = new System.TimeSpan(endDate.Ticks-startDate.Ticks);

            switch (howtocompare.ToLower())
            {
                case "year":
                  diff = Convert.ToDouble(TS.TotalDays/365);
                  break;
               case "month":
                  diff = Convert.ToDouble((TS.TotalDays/365)*12);
                  break;
               case "day":
                  diff = Convert.ToDouble(TS.TotalDays);
                  break;
               case "hour":
                  diff = Convert.ToDouble(TS.TotalHours);
                  break;
               case "minute":
                  diff = Convert.ToDouble(TS.TotalMinutes);
                  break;
               case "second":
                  diff = Convert.ToDouble(TS.TotalSeconds);
                  break;
            }

            return diff;
        }

        public int TotalWorkHoursInPeriod
        {
            get
            {
                DateTime start = new DateTime(PeriodStart.Year, PeriodStart.Month, 1);
                int days = DateTime.DaysInMonth(PeriodEnd.Year, PeriodEnd.Month);
                DateTime end = new DateTime(PeriodEnd.Year, PeriodEnd.Month, days);
                int result = (int)DateDiff("month", start, end) + 1;
                return result * 160;
            }
        }

    }

    public class DepartmentManagerReport
    {
        private Period _period = null;
        public Period Period
        {
            get
            {
                return _period;
            }
            set
            {
                _period = value;
            }
        }

        private int _workersCount = 0;
        public int WorkersCount
        {
            get
            {
                return _workersCount;
            }
            set
            {
                _workersCount = value;
            }
        }


        private Department _department = null;
        public Department DepartmentInfo
        {
            get
            {
                return _department;
            }
            set
            {
                _department = value;
            }
        }

        private bool _useChart = false;

        public bool ShowAsChart
        {
            get
            {
                return _useChart;
            }
            set
            {
                _useChart = value;
            }
        }

        public string DepartmentManagerTitle
        {
            get
            {
                if (DepartmentInfo != null)
                {
                    if (DepartmentInfo.Worker != null)
                    {
                        return DepartmentInfo.Worker.Fullname;
                    }                   
                }
                return "Nepaskirtas";
            }
        }

        private int _totalDepartmentWorked = 0;
        private int _workersOfDepartmentWorked = 0;        
        private List<AssociatedWorkedHours> _hoursOfOthers = new List<AssociatedWorkedHours>();

        public List<AssociatedWorkedHours> WorkedHoursOfOthers
        {
            get
            {
                return _hoursOfOthers;
            }
            set
            {
                _hoursOfOthers = value;
            }
        }

        public int TotalDepartmentWorked
        {
            get
            {
                return _totalDepartmentWorked;
            }
            set
            {
                _totalDepartmentWorked = value;
            }
        }

        public int ThisDepartmentWorkersWorkedInDepartmentProjects
        {
            get
            {
                return _workersOfDepartmentWorked;
            }
            set
            {
                _workersOfDepartmentWorked = value;
            }
        }

        public int ThisDepartmentWorkersWorkedInOtherProjects
        {
            get
            {
                return TotalDepartmentWorked - ThisDepartmentWorkersWorkedInDepartmentProjects;
            }
        }

        public int OthersTotalWorked
        {
            get
            {
                return _hoursOfOthers.Sum(h => h.Hours);
            }
        }

        public int WorkedNoWhere
        {
            get
            {
                return (Period.TotalWorkHoursInPeriod * WorkersCount) - TotalDepartmentWorked;
            }
        }

        public string PercentNotWorked
        {
            get
            {
                if (Period.TotalWorkHoursInPeriod != 0)
                {
                    return (((double)WorkedNoWhere / ((double)Period.TotalWorkHoursInPeriod * WorkersCount)) * (double)100).ToString("0.00") + "%";
                }
                else
                {
                    return "0.00%";
                }
            }
        }
    }


    public class DepartmentProjectReport
    {
        private string _title = "";
        private string _managerTitle = "";
        private string _managerDepartment = "";
        private MonthOfYear _started = null;
        private MonthOfYear _ended = null;
        private int _totalWorked = 0;
        private int _worked = 0;
        private int _project_id = 0;
        private int _manager_id = 0;
        private int _department_id = 0;


        public int DepartmentID
        {
            get
            {
                return _department_id;
            }
            set
            {
                _department_id = value;
            }
        }

        public int ProjectID
        {
            get
            {
                return _project_id;
            }
            set
            {
                _project_id = value;
            }
        }

        public int ManagerID
        {
            get
            {
                return _manager_id;
            }
            set
            {
                _manager_id = value;
            }
        }

        public String Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }


        public String Manager
        {
            get
            {
                return _managerTitle;
            }
            set
            {
                _managerTitle = value;
            }
        }

        public String ManagerDepartment
        {
            get
            {
                return _managerDepartment;
            }
            set
            {
                _managerDepartment = value;
            }
        }

        public MonthOfYear Started
        {
            get
            {
                return _started;
            }
            set
            {
                _started = value;
            }
        }

        public MonthOfYear Ended
        {
            get
            {
                return _ended;
            }
            set
            {
                _ended = value;
            }
        }

        public int TotalWorked
        {
            get
            {
                return _totalWorked;
            }
            set
            {
                _totalWorked = value;
            }
        }

        public int DepartmentWorkersWorked
        {
            get
            {
                return _worked;
            }
            set
            {
                _worked = value;
            }
        }

        public int OthersWorked
        {
            get
            {
                return TotalWorked - DepartmentWorkersWorked;
            }
        }
    }


    public class MyDepartmentFirstComparer : IComparer<Project>
    {

        private int department_id = 0;
        public MyDepartmentFirstComparer(int department)
        {
            department_id = department;
        }

        public int Compare(Project x, Project y)
        { 
            if ((x.Worker.department_id == department_id) && (y.Worker.department_id != department_id))
            {
                return -1;
            }
            else if ((x.Worker.department_id != department_id) && (y.Worker.department_id == department_id))
            {
                return 1;
            }
            else if ((x.Worker.department_id != department_id) && (y.Worker.department_id != department_id))
            {
                if (x.Worker.department_id > y.Worker.department_id)
                {
                    return -1;
                }
                else if (x.Worker.department_id < y.Worker.department_id)
                {
                    return 1;
                }
                else
                {
                    if (x.id > y.id)
                    {
                        return -1;
                    }
                    else if (x.id < y.id)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                if (x.id > y.id)
                {
                    return -1;
                }
                else if (x.id < y.id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }

        }
    }

}

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
            // Add action logic here
            return RedirectToAction("List");
        }

        public ActionResult DepartmentProjects(int? startYear, int? startMonth, int? endMonth, int? endYear, int? department_id, bool? chart, int? page, int? pageSize, bool? showOnlyMyProjects)
        {
            if (department_id.HasValue)
            {
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
            Department department = ((Department)TempData["department"] ?? new Department());
            ViewData["TitleWindow"] = "Kuriamas naujas skyrius";
            return View(department);
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

        public ActionResult List(int? page)
        {
            ViewData["Title"] = "Skyrių sąrašas";
            return View(DBDataContext.Departments.Where(w => (w.deleted.HasValue == false)).ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Department department = null;
                try
                {
                    department = (Department)TempData["department"] ?? DBDataContext.Departments.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (department != null)
                {
                    ViewData["TitleWindow"] = "Koreguojamas skyrius #" + department.id.ToString() + "(" + department.title + ")";
                    return View(department);
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
                    department.deleted = DateTime.Today;
                    if (userSession.userId != 0)
                    {
                        department.deleted_by_id = userSession.userId;
                    }
                    DBDataContext.SubmitChanges();
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
