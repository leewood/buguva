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

        private int getPeriodsCount(int type, int start, int end)
        {
            int startYear = (start - 1) / 12;
            int startMonth = 1 + (start - 1) % 12;
            int endYear = (end - 1) / 12;
            int endMonth = 1 + (end - 1) % 12;
            switch (type)
            {
                case 1: //Metai
                    return endYear - startYear + 1;
                case 2: //Pusmetis
                    {
                        int tStart = startYear * 2 + (startMonth - 1) / 6;
                        int tEnd = endYear * 2 + (endMonth - 1) / 6;
                        return tEnd - tStart + 1;
                    }                   
                case 3: // Ketvirtis
                    {
                        int tStart = startYear * 4 + (startMonth - 1) / 3;
                        int tEnd = endYear * 4 + (endMonth - 1) / 3;
                        return tEnd - tStart + 1;
                    }
                case 4: //Mėnuo
                    return end - start + 1;
            }
            return 0;
        }


        private int getPeriodStart(int type, int start)
        {
            int startYear = (start - 1) / 12;
            int startMonth = 1 + (start - 1) % 12;
            switch (type)
            {
                case 1: //Metai
                    return startYear;
                case 2: //Pusmetis
                    {
                        int tStart = startYear * 2 + (startMonth - 1) / 6 + 1;
                        return tStart;
                    }
                case 3: // Ketvirtis
                    {
                        int tStart = startYear * 4 + (startMonth - 1) / 3 + 1;
                        return tStart;
                    }
                case 4: //Mėnuo
                    return start;
            }
            return 0;
        }

        private int constructPeriodStart(int type, int index)
        {
            switch (type)
            {
                case 1:
                    return index * 12 + 1;
                case 2:
                    return index * 6 + 1;
                case 3:
                    return index * 3 + 1;
                case 4:
                    return index;
            }
            return index;
        }

        private int constructPeriodEnd(int type, int index)
        {
            switch (type)
            {
                case 1:
                    return index * 12 + 12;
                case 2:
                    return index * 6 + 6;
                case 3:
                    return index * 3 + 3;
                case 4:
                    return index;
            }
            return index;
        }

        private string periodString(int type, int value)
        {
            switch (type)
            {
                case 1:
                    return value.ToString() + " metai";
                case 2:
                    return ((value - 1) / 2).ToString() + " metų " + (1 + (value - 1)% 2).ToString() + " pusmetis";
                case 3:
                    return ((value - 1) / 4).ToString() + " metų " + (1 + (value-1) % 4).ToString() + " ketvirtis";
                case 4:
                    string month = (1 + (value-1) % 12).ToString();
                    if (month.Length < 2)
                    {
                        month = "0" + month;
                    }
                    return ((value - 1) / 12).ToString() + "-" + month;
            }
            return "";
        }

        public ActionResult SwitchingReport(int ? page)
        {
            int currentPage = page ?? 1;
            List<Department> departments = DBDataContext.Departments.Where(d => d.deleted.HasValue == false).ToList();
            SwitchingReport report = new SwitchingReport();
            report.Captions.Add("Laikotarpis");
            report.Redirections.Add(null);
            report.Actions.Add("");
            List<List<Project>> departmentProjects = new List<List<Project>>();
            foreach (Department department in departments)
            {
                report.Captions.Add("Skyrius " + department.title);
                System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                dict.Add("controller", "Departments");
                dict.Add("department_id", department.id);
                if (!department.canBeSeen())
                {
                    dict = null;
                }
                report.Redirections.Add(dict);
                report.Actions.Add("DepartmentManagerReport");
                departmentProjects.Add(DBDataContext.Projects.Where(p => department.Workers.Contains(p.Worker)).ToList());
            }
            report.Captions.Add("Visa įmonė");
            System.Web.Routing.RouteValueDictionary dict2 = new System.Web.Routing.RouteValueDictionary();
            dict2.Add("controller", "Departments");
            if (!(userSession.isAdministrator() || userSession.isAntanas()))
            {
                dict2 = null;
            }
            report.Redirections.Add(dict2);
            report.Actions.Add("GrandMastersReport");
            int total = 0;
            if (!(userSession.isAdministrator() || userSession.isAntanas()))
            {
                dict2 = null;
            }


            int itemsPerPage = userSession.ItemsPerPage;
            ViewData["Title"] = "\"Persijungimo\" ataskaita";
            ViewData["page"] = currentPage;
            ViewData["total"] = total;
            ViewData["size"] = itemsPerPage;
            return View(report);
        }

        public ActionResult IncompleteWorkReport(int? page, int? type)
        {
            int currentPage = page ?? 1;
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            int pType = type ?? 1;
            List<Department> departments = DBDataContext.Departments.Where(d => d.deleted.HasValue == false).ToList();
            IncompleteWorkValueReport report = new IncompleteWorkValueReport();
            report.Captions.Add("Laikotarpis");
            report.Redirections.Add(null);
            report.Actions.Add("");
            List<List<Project>> departmentProjects = new List<List<Project>>();
            foreach (Department department in departments)
            {
                report.Captions.Add("Skyrius " + department.title);
                System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                dict.Add("controller", "Departments");
                dict.Add("department_id", department.id);
                if (!department.canBeSeen())
                {
                    dict = null;
                }
                report.Redirections.Add(dict);
                report.Actions.Add("DepartmentManagerReport");
                departmentProjects.Add(DBDataContext.Projects.Where(p => department.Workers.Contains(p.Worker)).ToList());
            }
            report.Captions.Add("Visa įmonė");
            System.Web.Routing.RouteValueDictionary dict2 = new System.Web.Routing.RouteValueDictionary();
            dict2.Add("controller", "Departments");
            if (!(userSession.isAdministrator() || userSession.isAntanas()))
            {
                dict2 = null;
            }
            report.Redirections.Add(dict2);
            report.Actions.Add("GrandMastersReport");

            List<Task> tasks = DBDataContext.Tasks.OrderBy(t => t.year * 12 + t.month).ToList();
            int start = 0;
            int end = 0;
            if (tasks.Count > 0)
            {
                start = tasks[0].year * 12 + tasks[0].month;
                end = tasks[tasks.Count - 1].year * 12 + tasks[tasks.Count - 1].month;
            }
            int periodsCount = getPeriodsCount(pType, start, end);
            int periodStart = getPeriodStart(pType, start);
            IncompleteWorkValueReportRow totalRow = new IncompleteWorkValueReportRow();
            IncompleteWorkValueReportRow totalRow2 = new IncompleteWorkValueReportRow();
            for (int i = 0; i < departments.Count + 1; i++)
            {
                totalRow.Cells.Add(new IncompleteWorkValueReportCell());
                totalRow2.Cells.Add(new IncompleteWorkValueReportCell());
            }
            int itemsPerPage = userSession.ItemsPerPage;
            int endCycle = (periodsCount <= itemsPerPage) ? periodsCount + periodStart - 1 : periodStart + ((currentPage - 1) * itemsPerPage) + itemsPerPage - 1;
            for (int i = periodStart; i <= periodsCount + periodStart - 1; i++)
            {
                int pStart = constructPeriodStart(pType, i);
                int pEnd = constructPeriodEnd(pType, i);
                IncompleteWorkValueReportRow row = new IncompleteWorkValueReportRow();
                row.Period = periodString(pType, i);
                int j = 0;
                foreach (Department department in departments)
                {
                    
                    List<Task> periodTasks = DBDataContext.Tasks.Where(t => (department.Workers.Contains(t.Project.Worker) && ((t.year * 12 + t.month) >= pStart) && ((t.year * 12 + t.month) <= pEnd))).ToList();
                    IncompleteWorkValueReportCell cell = new IncompleteWorkValueReportCell();
                    cell.Value = periodTasks.Sum(t => t.worked_hours);
                    cell.Income = 0;
                    List<Project> myProjects = departmentProjects[j];
                    foreach (Project project in myProjects)
                    {
                        if (project.Length <= 6)
                        {
                            if ((project.LastMonth >= pStart) && (project.LastMonth <= pEnd))
                            {
                                cell.Income += project.FullIncome;
                            }

                        }
                        else if ((project.Length >= 7) && (project.Length <= 18))
                        {
                            if ((project.LastMonth >= pStart) && (project.LastMonth <= pEnd))
                            {
                                cell.Income += project.FullIncome * 0.6;
                            }
                            if ((project.MiddleMonth >= pStart) && (project.MiddleMonth <= pEnd))
                            {
                                cell.Income += project.FullIncome * 0.4;
                            }
                        }
                        else
                        {
                            if ((project.LastMonth >= pStart) && (project.LastMonth <= pEnd))
                            {
                                cell.Income += project.FullIncome * 0.6;
                            }
                            if ((project.Month8 >= pStart) && (project.Month8 <= pEnd))
                            {
                                cell.Income += project.FullIncome * 0.2;
                            }
                            if ((project.Month15 >= pStart) && (project.Month15 <= pEnd))
                            {
                                cell.Income += project.FullIncome * 0.2;
                            }

                        }
                    }
                    row.Cells.Add(cell);
                    totalRow.Cells[j].Value += cell.Value;
                    totalRow.Cells[j].Income += cell.Income;
                    totalRow2.Cells[j].Income += (cell.Difference > 0) ? cell.Difference : -cell.Difference;
                    j++;
                    
                }
                IncompleteWorkValueReportCell totalCell = new IncompleteWorkValueReportCell();
                totalCell.Value = row.Cells.Sum(c => c.Value);
                totalCell.Income = row.Cells.Sum(c => c.Income);
                row.Cells.Add(totalCell);
                totalRow.Cells[departments.Count].Value += totalCell.Value;
                totalRow.Cells[departments.Count].Income += totalCell.Income;
                totalRow2.Cells[departments.Count].Income += (totalCell.Difference > 0) ? totalCell.Difference : -totalCell.Difference;
                report.Rows.Add(row);


            }
            totalRow.Period = "";
            totalRow2.Period = "Viso ";
            for (int i = 0; i < totalRow2.Cells.Count; i++)
            {
                totalRow2.Cells[i].Value = totalRow.Cells[i].Value / report.Rows.Count;
                totalRow2.Cells[i].Income = totalRow2.Cells[i].Value - (totalRow2.Cells[i].Income / report.Rows.Count);
            }
            report.Rows = report.Rows.ToPagedList(currentPage - 1, itemsPerPage).ToList();
            report.Rows.Add(totalRow);
            report.Rows.Add(totalRow2);
            ViewData["Title"] = "Nebaigto darbo vertės ataskaita";
            ViewData["page"] = currentPage;
            ViewData["type"] = pType;
            ViewData["total"] = periodsCount;
            ViewData["totalPages"] = periodsCount / itemsPerPage + ((periodsCount % itemsPerPage > 0) ? 1 : 0);
            ViewData["size"] = itemsPerPage;
            return View(report);
        }

        public ActionResult OvertimeReport(int? page, int? type)
        {
            int currentPage = page ?? 1;
            int pType = type ?? 1;
            List<Department> departments = DBDataContext.Departments.Where(d => d.deleted.HasValue == false).ToList();
            OvertimeReport report = new OvertimeReport();
            report.Captions.Add("Laikotarpis");
            report.Redirections.Add(null);
            report.Actions.Add("");
            List<List<Project>> departmentProjects = new List<List<Project>>();
            
            foreach (Department department in departments)
            {
                report.Captions.Add("Skyrius " + department.title);
                System.Web.Routing.RouteValueDictionary dict = new System.Web.Routing.RouteValueDictionary();
                dict.Add("controller", "Departments");
                dict.Add("department_id", department.id);
                if (!department.canBeSeen())
                {
                    dict = null;
                }
                report.Redirections.Add(dict);
                report.Actions.Add("DepartmentManagerReport");
                departmentProjects.Add(DBDataContext.Projects.Where(p => department.Workers.Contains(p.Worker)).ToList());
            }
            report.Captions.Add("Visa įmonė");
            System.Web.Routing.RouteValueDictionary dict2 = new System.Web.Routing.RouteValueDictionary();
            dict2.Add("controller", "Departments");
            if (!(userSession.isAdministrator() || userSession.isAntanas()))
            {
                dict2 = null;
            }
            report.Redirections.Add(dict2);
            report.Actions.Add("GrandMastersReport");
            List<Task> tasks = DBDataContext.Tasks.OrderBy(t => t.year * 12 + t.month).ToList();
            int start = 0;
            int end = 0;
            if (tasks.Count > 0)
            {
                start = tasks[0].year * 12 + tasks[0].month;
                end = tasks[tasks.Count - 1].year * 12 + tasks[tasks.Count - 1].month;
            }
            int periodsCount = getPeriodsCount(pType, start, end);
            int periodStart = getPeriodStart(pType, start);

            OvertimeReportRow totalRow = new OvertimeReportRow();
            OvertimeReportRow totalRow2 = new OvertimeReportRow();
            for (int i = 0; i < departments.Count + 1; i++)
            {
                totalRow.Cells.Add(new OvertimeReportCell());
                totalRow2.Cells.Add(new OvertimeReportCell());
            }
            int itemsPerPage = userSession.ItemsPerPage;
            int endCycle = (periodsCount <= itemsPerPage) ? periodsCount + periodStart - 1 : periodStart + ((currentPage - 1) * itemsPerPage) + itemsPerPage - 1;
            for (int i = periodStart; i <= periodsCount + periodStart - 1; i++)
            {
                int pStart = constructPeriodStart(pType, i);
                int pEnd = constructPeriodEnd(pType, i);
                OvertimeReportRow row = new OvertimeReportRow();
                row.Period = periodString(pType, i);
                int j = 0;
                foreach (Department department in departments)
                {

                    List<Task> periodTasks = DBDataContext.Tasks.Where(t => (department.Workers.Contains(t.Project.Worker) && ((t.year * 12 + t.month) >= pStart) && ((t.year * 12 + t.month) <= pEnd))).ToList();
                    OvertimeReportCell cell = new OvertimeReportCell();
       
                    cell.TimeSum = periodTasks.Sum(t => t.worked_hours);
                    cell.TimeNormal = department.Workers.ToList().Sum(w => w.workedMonthsInPeriod(new Period((periodStart - 1) / 12, (periodStart - 1) % 12, (periodsCount + periodStart - 1) / 12, (periodsCount + periodStart - 1) % 12))); ;

                    row.Cells.Add(cell);
                    totalRow.Cells[j].TimeSum += cell.TimeSum;
                    totalRow.Cells[j].TimeNormal += cell.TimeNormal;
                    totalRow2.Cells[j].TimeNormal += (cell.TimeNormal > 0) ? cell.TimeNormal : -cell.TimeNormal;
                    j++;

                }

                OvertimeReportCell totalCell = new OvertimeReportCell();
                totalCell.TimeSum = row.Cells.Sum(c => c.TimeSum);
                totalCell.TimeNormal = row.Cells.Sum(c => c.TimeNormal);
                row.Cells.Add(totalCell);
                totalRow.Cells[departments.Count].TimeSum += totalCell.TimeSum;
                totalRow.Cells[departments.Count].TimeNormal += totalCell.TimeNormal;
                totalRow2.Cells[departments.Count].TimeNormal += (totalCell.TimeNormal > 0) ? totalCell.TimeNormal : -totalCell.TimeNormal;
                report.Rows.Add(row);


            }
            totalRow.Period = "";
            totalRow2.Period = "Viso ";
            for (int i = 0; i < totalRow2.Cells.Count; i++)
            {
                totalRow2.Cells[i].TimeSum = totalRow.Cells[i].TimeSum / report.Rows.Count;
                totalRow2.Cells[i].TimeNormal = totalRow2.Cells[i].TimeNormal - (totalRow2.Cells[i].TimeNormal / report.Rows.Count);
            }
            report.Rows.Add(totalRow);
            report.Rows.Add(totalRow2);
            report.Rows = report.Rows.ToPagedList(currentPage - 1, itemsPerPage).ToList();
            ViewData["Title"] = "Darbo laiko išnaudojimo projektams / viršvalandžių ataskaita";
            ViewData["page"] = currentPage;
            ViewData["type"] = pType;
            ViewData["total"] = periodsCount;
            ViewData["totalPages"] = periodsCount / itemsPerPage + ((periodsCount % itemsPerPage > 0) ? 1 : 0);
            ViewData["size"] = itemsPerPage;
            return View(report);
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
            List<Models.DepartmentProjectReport> result = new List<mvc.Models.DepartmentProjectReport>();
            Period period = new Period(stYear, stMonth, enYear, enMonth);
            string[] errors = period.getErrors().ToArray();
            if (errors.Length > 0)
            {
                TempData["errors"] = errors;
                return View(result);
            }
            bool useChart = chart ?? false;
            int currentPage = page ?? 1;
            int currentPageSize = pageSize ?? userSession.ItemsPerPage;
            if (currentPageSize < 1)
            {
                currentPageSize = 1;
            }
            List<Models.Project> projects = DBDataContext.Projects.Where(p => p.Tasks.Any(t => (t.year * 12 + t.month >= stYear * 12 + stMonth) && (t.year * 12 + t.month <= endYear * 12 + endMonth))).ToList();
            ViewData["pageSize"] = currentPageSize;
            IPagedList<Models.Project> pagedProjects = projects.ToPagedList(currentPage - 1, currentPageSize);
            int size = currentPageSize;
            if (useChart)
            {
                size = projects.Count();
                if (size < 1)
                {
                    size = 1;
                }
                pagedProjects = projects.ToPagedList(0, size);
            }

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
            Period period = new Period(stYear, stMonth, enYear, enMonth);
            string[] errors = period.getErrors().ToArray();            
            if (errors.Length > 0)
            {
                TempData["errors"] = errors;
                return View(report);
            }
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
                        report.WorkedHoursOfOthers.Add(new mvc.Models.AssociatedWorkedHours(department.title, tasks.Sum(t => t.worked_hours), department.couldWorkedInPeriod(period), department.id));
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
            Worker worker = null;
            try
            {
                worker = DBDataContext.Workers.First(w => w.id == workerID);
            }
            catch (Exception)
            {
            }
            if ((worker != null) && (!worker.canBeSeen()))
            {
                return RedirectToAction("NoPermissions", "Home");
            }
            ViewData["worker"] = worker;
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
                
                int curPage = page ?? 1;
                ViewData["curPage"] = curPage;
                ViewData["itemsPerPage"] = userSession.ItemsPerPage;
                Models.Project myProject = DBDataContext.Projects.Where(project => project.id == project_id.Value).First();                
                if (!myProject.canBeSeen())
                {
                    return RedirectToAction("NoPermissions", "Home");
                }
                ViewData["Title"] = "Projekto " + myProject.title + " intensyvumas";
                ViewData["projectCode"] = myProject.title + " ";
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
                
                
                Models.Project myProject = DBDataContext.Projects.Where(project => project.id == project_id.Value).First();
                if (!myProject.canBeSeen())
                {
                    return RedirectToAction("NoPermissions", "Home");
                }
                ViewData["Title"] = "Projekto " + myProject.title + " vadovo ataskaita";
                ViewData["projectCode"] = myProject.title;
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
                Worker worker = DBDataContext.Workers.First(w => w.id == workerID);
                if (!worker.canBeSeen())
                {
                    return RedirectToAction("NoPermissions", "Home");
                }

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
                    /*
                    if (months.Count > 0)
                    {
                        yearToUse = months[0].Year;
                        monthToUse = months[0].Month;
                    }
                     */
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
                else
                {
                    tasks = DBDataContext.Tasks.Where(task => ((task.project_id == project_id) && (task.project_participant_id == workerID))).ToList();
                }
                if (tasks == null) tasks = new List<mvc.Models.Task>();
                return View(new Models.TasksAndMonths(tasks.ToPagedList(currentPage - 1, 25), months, ((yearToUse + monthToUse > 0)?new Models.MonthOfYear(yearToUse, monthToUse):null), project));
            }
            else
            {
                return RedirectToAction("ListMyProjects");
            }
        }

        public ActionResult New()
        {
            if (Project.administrationNew())
            {
                Project project = ((Project)TempData.getAndRemove("project") ?? new Project());
                ViewData["TitleWindow"] = "Kuriamas naujas projektas";
                return View(project);
            }
            else
            {
                string[] errors = { "Neturite teisių kurti naują projektą" };
                TempData["errors"] = errors;
                TempData.Remove("project");
                return RedirectToAction("List");
            }
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
                    project = (Project)TempData.getAndRemove("project") ?? DBDataContext.Projects.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (project != null)
                {
                    if (project.administrationEdit())
                    {
                        ViewData["TitleWindow"] = "Koreguojamas projektas #" + project.id.ToString() + "(" + project.title + ")";
                        return View(project);
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių koreguoti šį projektą" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
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
            List<Project> projects = DBDataContext.Projects.Where(w => (w.deleted.HasValue == false)).ToList();
            projects = projects.Where(p => p.administationView()).ToList();
            return View(projects.ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
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
                    if (project.administrationDelete())
                    {
                        if (project.Tasks.Any())
                        {
                            string[] errors = { "Negalima ištrinti, nes šis projektas dar turi užduočių." };
                            TempData["errors"] = errors;
                            return RedirectToAction("List");
                        }
                        project.makeBackup(userSession.userId);
                        DBDataContext.Projects.DeleteOnSubmit(project);
                        DBDataContext.SubmitChanges();
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių ištrinti šį projektą" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
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
