using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestOrderingService : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        OrderingService service = new OrderingService();
        service.userHeader = new UserHeader() { UserName = TextBox1.Text, Password = TextBox2.Text };
        service.Order(TextBox3.Text);
        
    }
}
