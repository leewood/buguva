using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Models;
using LinqToSqlExtensions;
using mvc.Common;

namespace mvc.Controllers
{
    public class UsersController : Common.BaseController
    {
        public UsersController()
        {
            ViewData["Image"] = road.img("Users");
            ViewData["Base"] = road.link("Vartotojų sąrašas", "Users","");
        }
        
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(int? page)
        {
            ViewData["Title"] = "Vartotojų sąrašas";
            return View(DBDataContext.Users.Where(w => (w.deleted.HasValue == false)).ToPagedList(((page.HasValue) ? page.Value : 1) - 1, userSession.ItemsPerPage));
        }

        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                User user = null;
                try
                {
                    user = DBDataContext.Users.Where(u => u.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (user != null)
                {
                    user.deleted = DateTime.Today;
                    if (userSession.userId != 0)
                    {
                        user.deleted_by_id = userSession.userId;
                    }
                    DBDataContext.SubmitChanges();
                }
                else
                {
                    string[] errors = { "Bandoma trinti neegzistuojantį vartotoją" };
                    TempData["errors"] = errors;
                }
                return RedirectToAction("List");
            }
            else
            {
                string[] errors = { "Nenurodytas joks vartotojas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                User user = null;
                try
                {
                    user = (User)TempData["user"] ?? DBDataContext.Users.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (user != null)
                {
                    ViewData["Title"] = "Koreguojamas vartotojas #" + user.id.ToString() + "(" + user.login_name + ")";
                    return View(user);
                }
                else
                {
                    string[] errors = { "Bandoma koreguoti neegzistuojantį vartotoją" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks vartotojas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }
        }

        public ActionResult ChangePassword(int? id)
        {
            if (id.HasValue)
            {
                User user = null;
                try
                {
                    user = (User)TempData["user"] ?? DBDataContext.Users.Where(w => w.id == id.Value).First();
                }
                catch (Exception)
                {
                }
                if (user != null)
                {
                    ViewData["Title"] = "#" + user.id.ToString() + "(" + user.login_name + ") vartotojo slaptažodžio keitimas";
                    return View(user);
                }
                else
                {
                    string[] errors = { "Bandoma koreguoti neegzistuojantį vartotoją" };
                    TempData["errors"] = errors;
                    return RedirectToAction("List");
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks vartotojas" };
                TempData["errors"] = errors;
                return RedirectToAction("List");
            }

        }

        public ActionResult New()
        {
            User user = ((User)TempData["user"] ?? new User());
            ViewData["Title"] = "Kuriamas naujas vartotojas";
            return View(user);
        }

        public ActionResult Insert()
        {
            User user = DBDataContext.CreateEntityFromForm<User>(Request.Form);            
            var errors = user.Validate();
            if (errors != null)
            {
                TempData["errors"] = errors.ErrorMessages;
                TempData["user"] = user;

                return RedirectToAction("New");
            }
            else
            {
                DBDataContext.Users.InsertOnSubmit(user);
                DBDataContext.SubmitChanges();
            }
            return RedirectToAction("List");
        }


        public ActionResult UpdatePassword(int? id)
        {
            if (id.HasValue)
            {
                User user = DBDataContext.Get<User>(id.Value, "id");
                User tempUser = DBDataContext.CreateEntityFromForm<User>(Request.Form);
                user.repeated_password = tempUser.repeated_password;
                user.new_password = tempUser.new_password;
                user.new_repeated_password = tempUser.new_repeated_password;
                user.id = id.Value;                
                var errors = user.Validate();
                if (errors != null)
                {
                    TempData["errors"] = errors.ErrorMessages;
                    TempData["user"] = user;
                    return RedirectToAction("ChangePassword", new { id = user.id });
                }
                else
                {
                    user.password = user.new_password;
                    user.repeated_password = user.password;
                    user.new_password = "";
                    user.new_repeated_password = "";
                    DBDataContext.SubmitChanges(); 
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks vartotojas" };
                TempData["errors"] = errors;
            }
            return RedirectToAction("List");
        }


        public ActionResult Update(int? id)
        {
            if (id.HasValue)
            {
                User user = DBDataContext.CreateEntityFromForm<User>(Request.Form);
                user.id = id.Value;
                user.repeated_password = user.password;
                var errors = user.Validate();
                if (errors != null)
                {
                    TempData["errors"] = errors.ErrorMessages;
                    TempData["user"] = user;
                    return RedirectToAction("Edit", new { id = user.id });
                }
                else
                {
                    DBDataContext.Update<User>(Request.Form, id.Value);
                }
            }
            else
            {
                string[] errors = { "Nenurodytas joks vartotojas" };
                TempData["errors"] = errors;
            }
            return RedirectToAction("List");
        }
        
    }
}
