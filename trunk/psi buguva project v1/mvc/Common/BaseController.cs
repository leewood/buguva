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

        public int MyUserID
        {
            get
            {
                if (Session["myUserID"] != null)
                {
                    return (int)Session["myUserID"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Session["myUserID"] = value;
            }
        }


        private Models.POADataModelsDataContext _dBDataContext = null;
        public Models.POADataModelsDataContext DBDataContext
        {
            get
            {
                if (_dBDataContext == null)
                {
                    _dBDataContext = new mvc.Models.POADataModelsDataContext();
                }
                return _dBDataContext;
            }
        
        }

        public Models.Worker MyselfAsWorker
        {
            get
            {
                return DBDataContext.Workers.Where(worker => worker.id == MyWorkerID).First();
            }
        }

        public Models.User MyselfAsUser
        {
            get
            {
                return DBDataContext.Users.Where(user => user.id == MyUserID).First();
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            filterContext.Controller.ViewData["MyWorkerID"] = MyWorkerID;            
            base.OnActionExecuting(filterContext);
        }
    }
}
