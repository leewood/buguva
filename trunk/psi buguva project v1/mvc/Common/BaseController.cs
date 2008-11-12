using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace mvc.Common
{
    public class BaseController : Controller
    {
        public int MyWorkerID
        {
            get
            {
                if (HttpContext.Session["myWorkerID"] != null)
                {
                    return (int)HttpContext.Session["myWorkerID"];
                }
                else
                {
                    //return 0;
                    return 1; //<--- Cia tik meginimo tikslais, kai normaliai veiks loginas, sito neturetu but
                }
            }
            set
            {
                HttpContext.Session["myWorkerID"] = value;
                
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            filterContext.Controller.ViewData["MyWorkerID"] = MyWorkerID;            
            base.OnActionExecuting(filterContext);
        }
    }
}
