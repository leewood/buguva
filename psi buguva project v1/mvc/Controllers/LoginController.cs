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

            if (auth.loginWithCookie())
            {
                ViewData["Title"] = "Perkeliama...";
                Debugger.Instance.addMessage("su cookie Prisijungta!");
            }
            else
            {
                ViewData["Title"] = "Prisijungimas";
                Debugger.Instance.addMessage("su cookie neprisijungta");
            }

            return View();
        }
    }
}
