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
        public WorkersController()
        {
            ViewData["Image"] = road.img("Workers");
            ViewData["Base"] = road.link("Darbuotojų sąrašas", "Workers","");
        }
        
        
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }


        public ActionResult Form()
        {
            ViewData["Title"] = "Darbuotojas";            
            //return View(new Worker());
            return RedirectToAction("List");
        }


        public ActionResult List(int? page, string filter, string sorting)
        {
            ViewData["Title"] = "Darbuotojų sąrašas";
            List<Worker> workers = DBDataContext.Workers.Where(w => (w.deleted.HasValue == false)).ToList();
            workers = workers.Where(w => w.administationView()).ToList();
            Common.Sortings sorter = new Sortings(sorting);
            string sortCommand = sorter.getSortString();
            ViewData["filter"] = filter;
            if (filter != null)
            {
                workers = workers.Filter(filter);
            }
            ViewData["sorting"] = sorting;
            if (sortCommand != "")
            {
                workers = workers.Sort(sortCommand);
            }
            return View(workers.ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));                        
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
                    if (worker.administrationDelete())
                    {                        
                        if (worker.Projects.Count > 0)
                        {
                            string[] errors = { "Šis darbuotojas vadovauja keliems projektams. Pirma ištrinkite tuos projektus." };
                            TempData["errors"] = errors;
                            return RedirectToAction("List");
                        }
                        if (worker.Departments.Count > 0)
                        {
                            string[] errors = { "Šis darbuotojas vadovauja skyriams. Pirma ištrinkite tuos skyrius." };
                            TempData["errors"] = errors;
                            return RedirectToAction("List");
                        }
                        if (worker.Tasks.Count > 0)
                        {
                            string[] errors = { "Šis darbuotojas turi jam priskirtų užduočių. Pirma ištrinkite tas užduotis." };
                            TempData["errors"] = errors;
                            return RedirectToAction("List");                            
                        }
                        if (worker.Users.Count > 0)
                        {
                            string[] errors = { "Šis darbuotojas susietas su vienu ar daugiau sistemos vartotojų. Pirma pakeiskite šį susiejimą" };
                            TempData["errors"] = errors;
                            return RedirectToAction("List");                            

                        }
                        if (worker.WorkerStatus.Count > 0)
                        {
                            foreach (WorkerStatus status in worker.WorkerStatus)
                            {
                                worker.WorkerStatus.Remove(status);
                            }
                        }
                        worker.makeBackup(userSession.userId);
                        DBDataContext.Workers.DeleteOnSubmit(worker);
                        DBDataContext.SubmitChanges();
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių ištrinti šio darbuotojo" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
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
                   worker = (Worker)TempData.getAndRemove("worker") ?? DBDataContext.Workers.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (worker != null)
                {
                    if (worker.administrationEdit())
                    {
                        ViewData["Title"] = "Koreguojamas darbuotojas #" + worker.id.ToString() + "(" + worker.Fullname + ")";
                        return View(worker);
                    }
                    else
                    {
                        string[] errors = { "Neturite teisių keisti šio darbuotojo" };
                        TempData["errors"] = errors;
                        return RedirectToAction("List");
                    }
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
            if (Worker.administrationNew())
            {
                Worker worker = ((Worker)TempData.getAndRemove("worker") ?? new Worker());
                ViewData["Title"] = "Kuriamas naujas darbuotojas";
                return View(worker);
            }
            else
            {
                string[] errors = { "Neturite teisių kurti naujo darbuotojo" };
                TempData["errors"] = errors;
                TempData.Remove("worker");
                return RedirectToAction("List");
            }
            
        }

        public ActionResult Insert()
        {
            Worker worker = DBDataContext.CreateEntityFromForm<Worker>(Request.Form);
            if ((worker.surname == "") || (worker.surname == null))
            {
                worker.surname = " ";
            }
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
                if ((worker.surname == "") || (worker.surname == null))
                {
                    worker.surname = " ";
                }
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
