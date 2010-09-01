using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Installation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        if (context.DatabaseExists())
        {            
            Label1.Text = GetLocalResourceObject("Fail").ToString();
        }
        else
        {
            context.CreateDatabase();            
            Label1.Text = GetLocalResourceObject("OK").ToString();
        }
    }
}
