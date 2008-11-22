using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mvc.Common;
using System.Web.SessionState;
using System.Security.Cryptography;
using System.Text;
using mvc.Models;

/**
 * 
 * class: Authentication
 * 
 * Author: Rimvydas
 * 
 * Info: vartotojo prisijungimo valdymas
 * 
 */

namespace mvc.Common
{
    public class Authentication
    {
        private UserSession userSession = null;

        private UserCookie userCookie = null;

        public Authentication()
        {
            this.userSession = new UserSession();
            this.userCookie = new UserCookie("poa_login");
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

            //Debugger.Instance.addMessage("metodas login");

            if (this.checkLoginInfo(name, password))
            {
                //Debugger.Instance.addMessage("rasta informacija apie vartotoja");
                
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

        public void logout()
        {
            this.userSession.destroyInformation();
        }

        public bool loginWithCookie()
        {
            string name = this.userCookie.getValue("name");
            string auth = this.userCookie.getValue("password");

            if (name.Length > 0 && auth.Length > 0)
            {
                //Debugger.Instance.addMessage("sekmingai nuskaityta informacija");
                return this.login(name, auth, true);
            }

            return false;
        }

    }
}
