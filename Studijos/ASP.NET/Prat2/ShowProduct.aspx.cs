using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ShowProduct : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void PriceLabel_DataBinding(object sender, EventArgs e)
    {
        CurrencyShow cur = (CurrencyShow)(((Label)sender).Parent.FindControl("CurrencyShow4"));
        Product pr = ((Product)((FormView)((Control)((Label)sender).Parent).BindingContainer).DataItem);
        cur.RealCurrency = pr.Currency;
        cur.RealPrice = pr.Price;
        //cur.DataBind();
    }


    public void hideAdminButtons(HtmlContainerControl toolbar)
    {
        for (int i = 0; i < 6; i++)
        {
            if (User.IsInRole("Admin"))
            {
                toolbar.Controls[i * 2 + 1].Visible = true;
            }
            else
            {
                toolbar.Controls[i * 2 + 1].Visible = false;
            }
        }
    }

    protected void NewButton_Load(object sender, EventArgs e)
    {
        hideAdminButtons((HtmlContainerControl)((Button)sender).Parent.Parent);
    }
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Order": e.ToString(); break;
            case "New": Page.Response.Redirect("~/LoggedIn/AdminZone/NewProduct.aspx"); break;
            case "Edit": Page.Response.Redirect("~/LoggedIn/AdminZone/EditProduct.aspx?id=" + e.CommandArgument); break;
        }
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        Page.Response.Redirect("~/LoggedIn/ProductsList.aspx");
    }
}
