using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class LoggedIn_AdminZone_InsertNew : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DateHiddenField.Value = DateTime.Now.ToString();
            UserHiddenField.Value = Page.User.Identity.Name;
            if ((Request.Params["category"] != null) && (Request.Params["category"] != ""))
            {
                DropDownList list = ((DropDownList)FormView1.FindControl("CategoryDropDown"));
                list.SelectedValue = Request.Params["category"];
                list.Visible = false;
                ((Label)FormView1.FindControl("CategoryLabel")).Visible = false;
                ((Label)FormView1.FindControl("CategorySeparator")).Visible = false;
                
            }
        }
    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        Response.Redirect("~/PublicNews.aspx?category=" + Request.Params["category"]);
    }
    protected void InsertCancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PublicNews.aspx?category=" + Request.Params["category"]);
    }
}
