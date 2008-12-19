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
    public class TasksController : Common.BaseController
    {
        public ActionResult Index()
        {            
            return RedirectToAction("List");
        }

        public ActionResult New(int? project_id, int? year, int? month, bool? back)
        {
            if (Task.administrationNew())
            {
                Task task = ((Task)TempData.getAndRemove("task") ?? new Task());
                if (project_id.HasValue)
                {
                    task.project_id = project_id.Value;
                }
                if (year.HasValue)
                {
                    task.year = year.Value;
                }
                if (month.HasValue)
                {
                    task.month = month.Value;
                }
                ViewData["back"] = back;
                ViewData["TitleWindow"] = "Kuriama nauja užduotis";
                return View(task);
            }
            else
            {
                string[] errors = { "Jūs neturite teisių kurti naujų užduočių" };
                TempData["errors"] = errors;
                TempData.Remove("task");
                return RedirectToAction("List");
            }
        }

        public ActionResult Insert(bool? back)
        {
            Task task = DBDataContext.CreateEntityFromForm<Task>(Request.Form);
            var errors = task.Validate();
            if (errors != null)
            {
                TempData["errors"] = errors.ErrorMessages;
                TempData["task"] = task;

                return RedirectToAction("New");
            }
            else
            {
                DBDataContext.Tasks.InsertOnSubmit(task);
                DBDataContext.SubmitChanges();            
            }
            if ((back.HasValue) && (back.Value))
            {
                return RedirectToAction("ListMyTasksInProject", "Projects", new { project_id = task.project_id, year = task.year, month = task.month, id = task.project_participant_id });
            }
            else
            {
                return RedirectToAction("List");
            }

        }

        public ActionResult Edit(int? id, bool? back)
        {
            if (id.HasValue)
            {
                Task task = null;
                try
                {
                    task = (Task)TempData.getAndRemove("task") ?? DBDataContext.Tasks.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (task != null)
                {
                    if (task.administrationEdit())
                    {
                        ViewData["TitleWindow"] = "Koreguojama užduotis #" + task.id.ToString() /*+ "(" + task.title + ")"*/;
                        ViewData["back"] = back;
                        return View(task);
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių koreguoti šio užduoties" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
                }
                else
                {
                    string[] errors = { "Bandoma koreguoti neegzistuojančią užduotį" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodyta jokia užduotis" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult List(int? page, string filter, string sorting)
        {
            ViewData["Title"] = "Užduočių sąrašas";
            List<Task> tasks = DBDataContext.Tasks.Where(w => (w.deleted.HasValue == false)).ToList();
            tasks = tasks.Where(t => t.administationView()).ToList();
            tasks = filteredAndSorted<Task>(tasks, filter, sorting);
            return View(tasks.ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
        }

        public ActionResult Update(int? id, bool? back)
        {
            Task task = null;
            if (id.HasValue)
            {
                task = DBDataContext.CreateEntityFromForm<Task>(Request.Form);
                task.id = id.Value;
                var errors = task.Validate();
                if (errors != null)
                {
                    TempData["errors"] = errors.ErrorMessages;
                    TempData["task"] = task;
                    return RedirectToAction("Edit", new { id = task.id });
                }
                else
                {
                    DBDataContext.Update<Task>(Request.Form, id.Value);
                }
            }
            else
            {
                string[] errors = { "Nenurodyta jokia užduotis" };
                TempData["errors"] = errors;
            }
            if ((back.HasValue) && (back.Value))
            {
                return RedirectToAction("ListMyTasksInProject", "Projects", new { project_id = task.project_id, year = task.year, month = task.month, id = task.project_participant_id });
            }
            else
            {
                return RedirectToAction("List");
            }

        }

        public ActionResult Delete(int? id, bool? back)
        {
            int aproject_id = 0;
            int ayear = 0;
            int amonth = 0;
            int aworker_id = 0;
            if (id.HasValue)
            {
                Task task = null;
                try
                {
                    task = DBDataContext.Tasks.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (task != null)
                {
                    aproject_id = task.project_id;
                    aworker_id = task.project_participant_id;
                    ayear = task.year;
                    amonth = task.month;
                    if (task.administrationDelete())
                    {
                        task.makeBackup(userSession.userId);
                        DBDataContext.Tasks.DeleteOnSubmit(task);
                        DBDataContext.SubmitChanges();
                        if ((back.HasValue) && (back.Value))
                        {
                            return RedirectToAction("ListMyTasksInProject", "Projects", new { project_id = aproject_id, id = aworker_id, year = ayear, month = amonth});
                        }
                        else
                        {

                            return RedirectToAction("List");
                        }
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių ištrinti šią užduotį" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
                }
                else
                {
                    string[] errors = { "Bandoma trinti neegzistuojančią užduotį" };
                    TempData["errors"] = errors;
                }
                return RedirectToAction("List");
            }
            else
            {
                string[] errors = { "Nenurodyta jokia užduotis" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }
    }
}
