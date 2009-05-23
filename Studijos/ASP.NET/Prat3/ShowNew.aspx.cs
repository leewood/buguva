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

public partial class ShowNew : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DateHiddenField.Value = DateTime.Now.ToString();
        UserHiddenField.Value = (User.Identity.IsAuthenticated) ? User.Identity.Name : "Guest";
    }
    protected void EditButton_DataBinding(object sender, EventArgs e)
    {
        Button btn = ((Button)sender);
        object dataItem = DataBinder.GetDataItem(btn.Parent);
        string creator = DataBinder.Eval(dataItem, "Creator").ToString();
        if (User.Identity.Name != creator)
        {
            btn.Visible = false;
        }

    }
}
