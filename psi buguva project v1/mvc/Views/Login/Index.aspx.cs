using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace mvc.Views.Login
{
    public partial class Index : ViewPage
    {
        protected void loginbutton_Click(object sender, EventArgs e)
        {
            Label4.Text = "Jungiamasi...";


            mvc.Models.Authentication auth = mvc.Models.Authentication.instance();

            string name = input_loginname.Text;
            string password = input_password.Text;

            bool remember = (input_rememberme.Text != "")? true : false;

            if (auth.login(name, password, remember))
                Label4.Text = "Prisijungta";
            else
                Label4.Text = "Blogas slaptažodsi arba vardas";

        }
    }
}
