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

public partial class Login : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Backet.CurrentBacket != null)
        {
            Backet.CurrentBacket.Clear();
        }

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        e.Authenticated = Membership.ValidateUser(Login1.UserName, Login1.Password);
        
    }
}
