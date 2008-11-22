using System;
using System.Collections.Generic;
using System.Linq;
using mvc.Common;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

/**
 * 
 * class: UserCookie
 * 
 * Author: Rimvydas
 * 
 * Info: NIEKAS NEVEIKIA, NORS TURĖTŲ!!!!!!!!
 * 
 */

namespace mvc.Common
{

    
    public class UserCookie
    {
        protected string cookieName;

        public HttpCookie cookie;
        
        public UserCookie(string cookieName)
        {
            this.cookieName = cookieName;

            cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie == null)
            {
                Debugger.Instance.addMessage("nerastas cookie " + cookieName);
                
                HttpCookie newCookie = new HttpCookie(this.cookieName);
                newCookie.Expires = DateTime.Now.AddDays(7);
                HttpContext.Current.Response.Cookies.Add(newCookie);
                HttpContext.Current.Request.Cookies.Add(newCookie);

                cookie = newCookie;
            }
            else
            {
                Debugger.Instance.addMessage("rastas cookie " + cookieName);
            }
        }

        public string getValue(string valueName)
        {
            if (this.cookie != null)
            {
                if (this.cookie.Values[valueName] != null)
                    return this.cookie.Values[valueName];
                else
                    return "";
            }
            else
                return "";
        }

        public void setValue(string valueName, string value)
        {
            if (this.cookie != null)
            {
                this.cookie.Values[valueName] = value;
            }
        }

        public Boolean excist()
        {
            return (this.cookie != null) ? true : false;
        }

    }
}
