using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

//nebaigta
namespace mvc.Common
{
    public class SiteMap
    {
        //--------------------------------------------------------------------------

        static readonly SiteMap instance = new SiteMap();
        
        public string siteName = "Organizacijos apskaita®";

        public UserSession userSession = null;

        private IDictionary<string, string> map = null;

        //--------------------------------------------------------------------------

        public SiteMap()
        {
            this.userSession = new UserSession();
            this.map = new Dictionary<string, string>();
        }

        public static SiteMap Instance
        {
            get
            {
                return instance;
            }
        }

        private void nameController()
        {

        }

        private IDictionary<string, string> leftMeniu()
        {
            
            
            
            return map;
        }

        private void assign(string parentController, string childController)
        {
            this.map.Add(parentController, childController);
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