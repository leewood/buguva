using System;
using System.Collections.Generic;
using System.Linq;
using mvc.Common;
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
        public UserSession()
        {
           
        }

        public void destroyInformation()
        {
            HttpContext.Current.Session.RemoveAll();
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

        public int workerID
        {
            get
            {
                if (HttpContext.Current.Session["workerId"] != null)
                {
                    return (int)HttpContext.Current.Session["myWorkerID"];
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


    }
}
