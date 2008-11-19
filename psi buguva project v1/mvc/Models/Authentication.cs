using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvc.Common;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.Text;

/**
 * 
 * class: Authentication
 * 
 * Author: Rimvydas
 * 
 * Info: vartotojo prisijungimo valdymas
 * 
 */

namespace mvc.Models
{
    public class Authentication
    {

        private static Authentication oInstance;

        private UserSession userSession = null;

        protected Authentication()
        {
            this.userSession = new UserSession();
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


        private string encrypt(string password)
        {
            byte[] pass = Encoding.UTF8.GetBytes(password);
            MD5 md5 = new MD5CryptoServiceProvider();
            string strPassword = Encoding.UTF8.GetString(md5.ComputeHash(pass));
            return strPassword;
        }

        private bool checkLoginInfo(string name, string password)
        {
            POADataModelsDataContext data = new POADataModelsDataContext();
            return data.Users.Where(user => (user.login_name == name && user.password == password)).Count() > 0;
        }

        private Models.User getUserInfo(string name, string password)
        {
            POADataModelsDataContext data = new POADataModelsDataContext();
            return data.Users.Where(user => (user.login_name == name && user.password == password)).First();
        }

        public bool login(string name, string password, bool remember)
        {
            this.userSession.destroyInformation();

            if (this.checkLoginInfo(name, password))
            {
                Models.User user = this.getUserInfo(name, password);
                this.userSession.userId = user.id;
                this.userSession.userName = user.login_name;
                this.userSession.userLevel = user.level;

                return true;
            }
            else
                return false;
        }

        public bool isLoggedIn()
        {
            return this.userSession.loged;
        }



    }
}
