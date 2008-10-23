using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvc.Views.Home
{
    public partial class Example : ViewPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormView1.PageIndex = GridView1.SelectedIndex;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
