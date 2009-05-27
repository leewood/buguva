using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BacketControl : System.Web.UI.UserControl
{
    public BacketControl()
    {
        this.PreRender += new EventHandler(BacketControl_PreRender);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    void BacketControl_PreRender(object sender, EventArgs e)
    {
        Control cnt = ListView1.FindControl("TotalValueLabel");
        if (cnt != null)
        {
            Literal lit = (Literal)cnt;
            lit.Text = Backet.TotalLits + "<span class = \"price_cents\">" + Backet.TotalCents + "</span> " + Backet.MyActiveCurrency;
        }
                    
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
            case "MyOrders": Response.Redirect("~/LoggedIn/OrdersList.aspx"); break;
        }
    }

    public void LabelDataBind(object sender, EventArgs e)
    {
       // ((Label)sender).Text = Backet.Total.ToString("0.00") + " " + Backet.MyActiveCurrency;
    }


}
