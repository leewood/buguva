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

        public ActionResult New()
        {
            if (Task.administrationNew())
            {
                Task task = ((Task)TempData.getAndRemove("task") ?? new Task());
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

        public ActionResult Insert()
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
            return RedirectToAction("List");
        }

        public ActionResult Edit(int? id)
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

        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Task task = DBDataContext.CreateEntityFromForm<Task>(Request.Form);
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
            return RedirectToAction("List");
        }

        public ActionResult Delete(int? id)
        {
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
                    if (task.administrationDelete())
                    {
                        task.makeBackup(userSession.userId);
                        DBDataContext.Tasks.DeleteOnSubmit(task);
                        DBDataContext.SubmitChanges();
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
