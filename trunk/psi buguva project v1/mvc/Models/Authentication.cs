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


        //--------------- Rimvydai tau sioks toks Linq examplas -----------
        // Jis praktiskai patikrina ar duombazej egzistuoja useris su 'loginu' name ir passwordu 'password'
        public bool checkLoginInfo(string name, string password)
        {
            //Prisijungiam prie db, anksciau konstruktoriuje reikejo nurodyti ConnectionStringa, bet padariau, kad nereikalautu
            //Ima dabar by default POAConnectionString, aisq jei nor gali konstruktoriuje nurodyti kita
            //Praktiskai POADataModelsDataContext klases sukurimas atitinka Connectiono sukurima ir parengima Linq uzklausom
            POADataModelsDataContext data = new POADataModelsDataContext();  
            //Pasiimam siek tiek duomenu is db
            //sakinys gal siek tiek neiprastas, bet user => (salyga), reikia, kad issinkti is Users tik tokius objektus
            // kurie tenkina norodyta salyga
            // Users yra tipo Linq.Table<User>, Where grazina ta pati tipa tik atfiltruota
            // Si tipas turi geru metodu, kaip ToList(), ToArray(), Count() ir pan.
            // Na tikiuosi bus naudingas bent kazkiek sitas examplas
            return data.Users.Where(user => (user.login_name == name && user.password == password)).Count() > 0; 
            
        }

        public bool login(string name, string password, bool remember)
        {
            // Karoli, kaip naudotis sesija?
            //Session("Stocks") = "asd";

            return this.isLoggedIn();
        }

        public bool isLoggedIn()
        {
            return true;
        }



    }
}
