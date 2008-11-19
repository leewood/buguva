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
        private Models.POADataModelsDataContext _dBDataContext = null;
       
        /**
         * informacija apie vartotoja
         */
        public UserSession userSession = null;

        /**
         * konstruktorius
         */ 
        public BaseController()
        {
            this.userSession = new UserSession();   
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["MyWorkerID"] = userSession.workerID;            
            base.OnActionExecuting(filterContext);
        }

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

        public Models.Worker WorkerInformation
        {
            get
            {
                return DBDataContext.Workers.Where(worker => worker.id == userSession.workerID).First();
            }
        }

        public Models.User UserInformation
        {
            get
            {
                return DBDataContext.Users.Where(user => user.id == userSession.userId).First();
            }
        }
    }
}
