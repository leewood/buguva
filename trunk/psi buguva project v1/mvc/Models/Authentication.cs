using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace mvc.Models
{
    public class Authentication
    {
        private static Authentication oInstance;

        protected Authentication()
        {
        }

        public static Authentication instance()
        {
            if (HttpContext.Current == null)
            {
                if (oInstance == null)
                {
                    oInstance = new Authentication();
                }
                return oInstance;
            }

            if (!HttpContext.Current.Items.Contains("Authentication"))
            {
                HttpContext.Current.Items.Add("Authentication", new Authentication());
            }

            return (Authentication)HttpContext.Current.Items["Authentication"];
        }
        
        public bool login(string name, string password, bool remember)
        {
            // Karoli, kaip naudotis sesija?
            Session("Stocks") = "asd";

            return this.isLoggedIn();
        }

        public bool isLoggedIn()
        {
            return true;
        }



    }
}
