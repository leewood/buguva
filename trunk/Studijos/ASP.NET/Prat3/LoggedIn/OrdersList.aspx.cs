using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoggedIn_OrdersList : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ((Request.Params["mode"] == "all") && (User.IsInRole("Admin")))
        {
            LinqDataSource1.Where = "";
        }
        else
        {
            LinqDataSource1.Where = String.Format("Person == \"{0}\"", User.Identity.Name);
        }
        LinqDataSource1.DataBind();
        ListView1.DataBind();
    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Preview": Response.Redirect(String.Format("~/LoggedIn/ViewOrder.aspx?id={0}", e.CommandArgument.ToString())); break;
        }
    }

    protected void PreviewButtonClick(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/LoggedIn/ViewOrder.aspx?id={0}", ((ImageButton)sender).CommandArgument.ToString()));
    }
}
