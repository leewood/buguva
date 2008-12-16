using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using mvc.Common;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {                        
            Authentication auth = new Authentication();
            UserSession userSession = new UserSession();
            mvc.Models.POADataModelsDataContext dbDataContext = new mvc.Models.POADataModelsDataContext();
            bool installResult = dbDataContext.Install();
            string[] errors = { "Duomenų bazė nerasta ir jos sukurti nepavyko. Pasitikrinkite nustatymus." };
            TempData["errors"] = errors;
            if (auth.loginWithCookie())
            {
                ViewData["Title"] = "Perkeliama...";

                return Redirect(userSession.getHomepageUrl());
            }
            else
            {
                ViewData["Title"] = "Prisijungimas";
            }

            return View();
        }

        public ActionResult Logout()
        {
            Authentication auth = new Authentication();

            auth.logout();

            var newCookie = new HttpCookie("poa_login");

            // kai uz pinigus programinat, sitaip niekad nedarykit
            newCookie.Values["name"] = "";
            newCookie.Values["password"] = "";

            newCookie.Expires = DateTime.Now.AddDays(10);
            Response.AppendCookie(newCookie);

            return Redirect("/Login/");
        }
    }
}
