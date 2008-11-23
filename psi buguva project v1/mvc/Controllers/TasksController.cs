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
            // Add action logic here
            return RedirectToAction("List");
        }

        public ActionResult New()
        {
            Task task = ((Task)TempData["task"] ?? new Task());
            ViewData["TitleWindow"] = "Kuriama nauja užduotis";
            return View(task);
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
                    task = (Task)TempData["task"] ?? DBDataContext.Tasks.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (task != null)
                {
                    ViewData["TitleWindow"] = "Koreguojama užduotis #" + task.id.ToString() /*+ "(" + task.title + ")"*/;
                    return View(task);
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

        public ActionResult List(int? page)
        {
            ViewData["Title"] = "Užduočių sąrašas";
            return View(DBDataContext.Tasks.Where(w => (w.deleted.HasValue == false)).ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
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
                    task.deleted = DateTime.Today;
                    if (userSession.userId != 0)
                    {
                        task.deleted_by_id = userSession.userId;
                    }
                    DBDataContext.SubmitChanges();
                }
                else
                {
                    string[] errors = { "Bandoma trinti neegzistuojančą užduotį" };
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
