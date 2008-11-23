using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Common;

namespace mvc.Views.Login
{
    public partial class Index : ViewPage
    {
        protected void loginbutton_Click(object sender, EventArgs e)
        {
            Label4.Text = "Jungiamasi...";

            Authentication auth = new Authentication();

            string name = input_loginname.Text;
            string password = input_password.Text;

            bool remember = input_rememberme.Checked;

            if (auth.login(name, password, remember))
            {
                Label4.Text = "Prisijungta";

                if (remember)
                {
                    var newCookie = new HttpCookie("poa_login");

                    // kai uz pinigus programinat, sitaip niekad nedarykit
                    newCookie.Values["name"] = name;
                    newCookie.Values["password"] = password;

                    newCookie.Expires = DateTime.Now.AddDays(10);
                    Response.AppendCookie(newCookie);
                }

                UserSession userSession = new UserSession();

                if (userSession.isAdministrator())
                {
                    Response.Redirect("/Workers/");
                }
                else
                {
                    Response.Redirect("/Projects/");
                }
            }
            else
            {
                Label4.Text = "Blogas slaptažodis arba prisijungimo vardas";
            }
        }
    }
}
