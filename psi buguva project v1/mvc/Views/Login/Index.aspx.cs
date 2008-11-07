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
            loginbutton.Text = "laba";
        }
    }
}
