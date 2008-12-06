using System;
using System.Collections.Generic;
using System.Linq;
using mvc.Common;
using mvc.Models;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

/**
 * 
 * class: UserInformation
 * 
 * Author: Rimvydas feat Karolis
 * 
 * Info: padeda greitai gauti info apie vartotoja
 * 
 */

namespace mvc.Common
{
    public class UserSession
    {
        public string myProjectsListUrl = "/Projects/ListMyProjects";

        public string myAdminUrl = "/Workers/List";

        public string myReportsUrl = "/Projects/ListMyProjects"; // vadovybes ataskaita        

        public UserSession()
        {
           
        }

        public void destroyInformation()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public string getHomepageUrl()
        {
            switch (this.userLevel)
            {
                case 1: return this.myProjectsListUrl;
                case 2: return this.myProjectsListUrl;
                case 3: return this.myAdminUrl;
                case 4: return this.myReportsUrl;
            }

            return "/";
        }
        
        public bool loged
        {
            get
            {
                if (HttpContext.Current.Session["level"] != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int ItemsPerPage
        {
            get
            {
                if (HttpContext.Current.Session["itemsPerPage"] != null)
                {
                    return (int)HttpContext.Current.Session["itemsPerPage"];
                }
                else
                {
                    return 25;
                }

            }
            set
            {
                HttpContext.Current.Session["itemsPerPage"] = value;
            }
        }

        public int userLevel
        {
            get
            {
                if (HttpContext.Current.Session["level"] != null)
                {
                    return (int)HttpContext.Current.Session["level"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["level"] = value;
            }
        }

        public string userLevelName
        {
            get
            {
                if ((this.userLevel != 0) && (this.userLevel <= Models.User.LevelNames.Length))
                {
                    return Models.User.LevelNames[this.userLevel - 1];
                }
                else
                {
                    return "nežinomas lygis";
                }
            }
        }

        public int workerID
        {
            get
            {
                if (HttpContext.Current.Session["workerId"] != null)
                {
                    return (int)HttpContext.Current.Session["workerId"];
                }
                else
                {
                    //return 0;
                    return 1; //<--- Cia tik meginimo tikslais, kai normaliai veiks loginas, sito neturetu but
                }
            }
            set
            {
                HttpContext.Current.Session["workerId"] = value;
            }
        }

        public string userName
        {
            get
            {
                if (HttpContext.Current.Session["username"] != null)
                {
                    return (string)HttpContext.Current.Session["username"];
                }
                else
                {
                    return "unknow";
                }
            }
            set
            {
                HttpContext.Current.Session["username"] = value;
            }
        }

        public int workerDepartment
        {
            get
            {
                if (HttpContext.Current.Session["workerDepartment"] != null)
                {
                    return (int)HttpContext.Current.Session["workerDepartment"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["workerDepartment"] = value;
            }
        }

        public string workerName
        {
            get
            {
                if (HttpContext.Current.Session["workerName"] != null)
                {
                    return (string)HttpContext.Current.Session["workerName"];
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["workerName"] = value;
            }
        }

        public string workerSurname
        {
            get
            {
                if (HttpContext.Current.Session["workerSurname"] != null)
                {
                    return (string)HttpContext.Current.Session["workerSurname"];
                }
                else
                {
                    return "";
                }
            }
            set
            {
                HttpContext.Current.Session["workerSurname"] = value;
            }
        }

        public int userId
        {
            get
            {
                if (HttpContext.Current.Session["userId"] != null)
                {
                    return (int)HttpContext.Current.Session["userId"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session["userId"] = value;
            }
        }

        public bool isSimpleUser()
        {
            return (this.userLevel == 1);
        }

        public bool isDepartmentMaster()
        {
            return (this.userLevel == 2);
        }

        public bool isAdministrator()
        {
            return (this.userLevel == 3);
        }

        public bool isAntanas()
        {
            return (this.userLevel == 4);
        }

        public bool canViewAtaskaitos()
        {
            return (isAdministrator() || isSimpleUser() || isDepartmentMaster() || isAntanas());
        }

        public bool canViewAdmin()
        {
            return (isAdministrator());
        }

        public bool canViewAllProjectsReoprts()
        {
            return (isAdministrator() || isAntanas());
        }

        public bool canEditProjects()
        {
            return (!isAntanas());
        }

        public bool canEditAnyProject()
        {
            return (isAdministrator() || isDepartmentMaster());
        }
    }
}
