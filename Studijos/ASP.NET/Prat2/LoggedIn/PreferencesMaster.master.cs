using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class LoggedIn_PreferencesMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Menu1_Init(object sender, EventArgs e)
    {
        System.IO.FileInfo finfo = new System.IO.FileInfo(this.Page.Request.Url.AbsolutePath);
        for (int i = 0; i < Menu1.Items.Count; i++)
        {
            if (Menu1.Items[i].NavigateUrl == finfo.Name)
            {
                Menu1.Items[i].Selected = true;
            }
            else
            {
                Menu1.Items[i].Selected = false;
            }
        }
    }
}
