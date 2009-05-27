using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserView : System.Web.UI.UserControl
{
    public UserView()
    {
        this.PreRender += new EventHandler(UserView_PreRender);
    }

    void UserView_PreRender(object sender, EventArgs e)
    {
        FormView1.Visible = true;
        if (this.ViewUserName != "")
        {
            if (Page.User.IsInRole("Admin") || Page.User.Identity.Name == this.ViewUserName)
            {
                LinqDataSource1.Where = "Username == \"" + this.ViewUserName + "\"";
                LinqDataSource1.DataBind();
                FormView1.DataBind();
            }
            else
            {
                FormView1.Visible = false;
            }
        }
        else
        {
            FormView1.Visible = false;
        }        
    }

    public string ViewUserName
    {
        get
        {
            if (ViewState["UserName"] == null)
            {
                ViewState["UserName"] = "";
            }
            return ViewState["UserName"].ToString();
        }
        set
        {
            ViewState["UserName"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
