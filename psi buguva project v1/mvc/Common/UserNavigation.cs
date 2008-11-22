using System;
using System.Collections.Generic;
using System.Linq;
using mvc;
using mvc.Common;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;


namespace mvc.Common
{
    public class UserNavigation
    {
        public string controller;

        public string action;

        public UserSession userSession = null;

        public UserNavigation(string controller, string action)
        {
            this.controller = controller;
            this.action = action;

            this.userSession = new UserSession();
        }

        public bool isInAdmin()
        {
            return (
                
                controller == "Users" ||
                controller == "Workers" ||
                controller == "Import"

                )? true : false;
        }

        public bool isInAtaskaitos()
        {
            return (

                controller == "Projects"  ||
                controller == "Sections" 

                ) ? true : false;
        }

    }
}