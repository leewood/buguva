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

public partial class LoggedIn_AdminZone_EditNew : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DateHiddenField.Value = DateTime.Now.ToString();
        ((HiddenField)FormView1.FindControl("ModifiedHiddenField")).Value = DateTime.Now.ToString();

    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        int id = int.Parse(Request.Params["id"]);
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        var g = (from h in context.News where h.id == id select h.Category).ToList()[0];
        Response.Redirect("~/PublicNews.aspx?category=" + g);
    }
    protected void UpdateCancelButton_Click(object sender, EventArgs e)
    {
        int id = int.Parse(Request.Params["id"]);
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        var g = (from h in context.News where h.id == id select h.Category).ToList()[0];
        Response.Redirect("~/PublicNews.aspx?category=" + g);
    }
}
