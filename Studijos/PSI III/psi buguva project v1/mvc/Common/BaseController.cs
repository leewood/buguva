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
         * autentifikavimas
         */
        public Authentication auth = null;


        public RoadHelper road = null;

        /**
         * konstruktorius
         */

        public BaseController()
        {
            this.userSession = new UserSession();
            this.auth = new Authentication();
            this.road = new RoadHelper();
        }

        public List<T> filteredAndSorted<T>(List<T> source, string filter, string sorting)
        {
            ViewData["filter"] = filter;
            if (filter != null)
            {
                source = source.Filter(filter);
            }
            Common.Sortings sorter = new Sortings(sorting);
            string sortCommand = sorter.getSortString();
            ViewData["sorting"] = sorting;
            if (sortCommand != "")
            {
                source = source.Sort(sortCommand);
            }
            return source;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["MyWorkerID"] = userSession.workerID;            
            base.OnActionExecuting(filterContext);

            if (this.auth.isLoggedIn() == false)
                Response.Redirect("/Login/");
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
