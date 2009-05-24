using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BacketControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Increase": Backet.CurrentBacket.Increase(int.Parse(e.CommandArgument.ToString())); this.DataBind();  break;
            case "Decrease": Backet.CurrentBacket.Descrease(int.Parse(e.CommandArgument.ToString())); this.DataBind(); break;
            case "DeleteItem": Backet.CurrentBacket.RemoveLine(int.Parse(e.CommandArgument.ToString())); this.DataBind(); break;
            case "Clear": Backet.CurrentBacket.Clear(); this.DataBind(); break;
            case "Order": Backet.CurrentBacket.OrderIt(""); this.DataBind(); break;
        }
    }

    public void LabelDataBind(object sender, EventArgs e)
    {
        ((Label)sender).Text = Backet.Total.ToString("0.00") + " " + Backet.MyActiveCurrency;
    }
}
