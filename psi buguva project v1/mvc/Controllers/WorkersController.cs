using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using LinqToSqlExtensions;
using mvc.Common;

namespace mvc.Controllers
{
    public class WorkersController : Common.BaseController
    {   
        
        public ActionResult Index()
        {
            return RedirectToAction("List");
            /*
            ViewData["Title"] = "Darbuotojai";
            mvc.Models.POADataModelsDataContext data = new mvc.Models.POADataModelsDataContext();//System.Configuration.ConfigurationManager.ConnectionStrings["ProjectDatabaseConnection"].ConnectionString);            
            ViewResult result = View(data.GetWorkers());
            
           
            return result;
             */
        }


        public ActionResult Form()
        {
            ViewData["Title"] = "Darbuotojas";
            
            return View(new Worker());
        }


        public ActionResult List(int? page)
        {
            ViewData["Title"] = "Darbuotojų sąrašas";
            return View(DBDataContext.Workers.Where(w => (w.deleted.HasValue == false)).ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));                        
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                Worker worker = null;
                try
                {
                    worker = DBDataContext.Workers.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (worker != null)
                {
                    worker.deleted = DateTime.Today;
                    if (userSession.userId != 0)
                    {
                        worker.deleted_by_id = userSession.userId;
                    }
                    DBDataContext.SubmitChanges();
                }
                else
                {
                    string[] errors = { "Bandoma trinti neegzistuojantį darbuotoją" };
                    TempData["errors"] = errors;
                }
                return RedirectToAction("List");
            }
            else
            {
                string[] errors = { "Nenurodytas joks darbuotojas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                Worker worker = null;
                try
                {
                   worker = (Worker)TempData["worker"] ?? DBDataContext.Workers.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (worker != null)
                {
                    ViewData["Title"] = "Koreguojamas darbuotojas #" + worker.id.ToString() + "(" + worker.Fullname + ")";
                    return View(worker);
                }
                else
                {
                    string[] errors = { "Bandoma koreguoti neegzistuojantį darbuotoją" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks darbuotojas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult New()
        {
            Worker worker = ((Worker)TempData["worker"] ?? new Worker());
            ViewData["Title"] = "Kuriamas naujas darbuotojas";
            return View(worker);
        }

        public ActionResult Insert()
        {
            Worker worker = DBDataContext.CreateEntityFromForm<Worker>(Request.Form);            
            var errors = worker.Validate();
            if (errors != null)
            {
                TempData["errors"] = errors.ErrorMessages;
                TempData["worker"] = worker;

                return RedirectToAction("New");
            }
            else
            {
                DBDataContext.Workers.InsertOnSubmit(worker);
                DBDataContext.SubmitChanges();
            }
            return RedirectToAction("List");
        }

        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                Worker worker = DBDataContext.CreateEntityFromForm<Worker>(Request.Form);
                worker.id = id.Value;
                var errors = worker.Validate();
                if (errors != null)
                {
                    TempData["errors"] = errors.ErrorMessages;
                    TempData["worker"] = worker;
                    return RedirectToAction("Edit", new { id = worker.id });
                }
                else
                {
                    DBDataContext.Update<Worker>(Request.Form, id.Value);
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks darbuotojas" };
                TempData["errors"] = errors;
            }
            return RedirectToAction("List");
        }

        
        public ActionResult IndexRedirect()
        {
            return RedirectToAction("Index");
        }

    }
}
