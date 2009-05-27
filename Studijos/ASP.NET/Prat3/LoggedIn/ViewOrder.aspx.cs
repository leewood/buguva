using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoggedIn_ViewOrder : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        int id = 0;
        try
        {
            id = int.Parse(e.CommandArgument.ToString());
        }
        catch (Exception)
        {
        }
        Order order = null;
        if (id > 0)
        {
            order = (from j in context.Orders where j.id == id select j).First();
        }
        switch (e.CommandName)
        {
            case "Confirm":
                {
                    order.Confirm();
                    context.SubmitChanges();
                    this.DataBind();
                }
                break;
            case "Reject":
                {
                    order.Reject();
                    context.SubmitChanges();
                    this.DataBind();
                }
                break;
            case "Back": Page.Response.Redirect("~/LoggedIn/OrdersList.aspx"); break;
        }
        
    }
    protected void PersonLabel_DataBinding(object sender, EventArgs e)
    {
        Control cnt = FormView1.FindControl("UserView1");
        if (cnt != null)
        {
            UserView view = (UserView)cnt;
            view.ViewUserName = DataBinder.Eval(FormView1.DataItem, "Person").ToString();
        }
    }
}
