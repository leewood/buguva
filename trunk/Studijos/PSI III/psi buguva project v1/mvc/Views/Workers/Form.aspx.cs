using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace mvc.Views.Workers
{
    public partial class Form : ViewPage<mvc.Models.Worker>
    {

        protected void FormView1_ItemInserted(object sender, System.Web.UI.WebControls.FormViewInsertedEventArgs e)
        {
            this.Page.Response.Redirect("../Workers/Index");
             
        }
    }
}
