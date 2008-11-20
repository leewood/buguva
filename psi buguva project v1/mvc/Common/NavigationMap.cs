using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Xml;
using System.Web;

//nebaigta
namespace mvc.Common
{
    public class NavigationMap
    {
        //--------------------------------------------------------------------------

        static readonly NavigationMap instance = new NavigationMap();
        
        public string siteName = "Organizacijos apskaita®";

        public UserSession userSession = null;

        private IDictionary<string, string> map = null;

        private string definedTopNavigation;

        //--------------------------------------------------------------------------

        public NavigationMap()
        {
            this.userSession = new UserSession();
            this.map = new Dictionary<string, string>();

        }

        public static NavigationMap Instance
        {
            get
            {
                return instance;
            }
        }

        public void defineTopNavigation(string name)
        {
            this.definedTopNavigation = name;
        }

        public void assignController(string name)
        {
            
        }

        public string RenderHtml()
        {
            string a;
            a = "testas";

            return a;
        }

        //--------------------------------------------------------------------------

    }
}