using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace mvc.Views.Import
{
    public partial class Index : ViewPage
    {
  

        protected void Label1_DataBinding(object sender, EventArgs e)
        {

        }

        protected void FileUpload1_DataBinding1(object sender, EventArgs e)
        {
            Label1.Text = "pataikei";
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}
