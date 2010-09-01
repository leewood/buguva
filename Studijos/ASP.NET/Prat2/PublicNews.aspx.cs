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

public partial class PublicNews : ExtendedPage
{
    public void updateFilter()
    {
        string filter = "";
        string separ = "";
        if ((this.Request.Params["category"] != null) && (this.Request.Params["category"] != ""))
        {
            filter = String.Format("(Category = \"{0}\")", this.Request.Params["category"]);
            separ = "&&";
        }
        string dateFrom = CreateDateFromTextBox.Text;
        string dateTo = CreateDateToTextBox.Text;
        if (dateFrom != "")
        {
            filter += separ + "(CreateDate >= DateTime.Parse(\"" + dateFrom + "\"))";
            separ = "&&";
        }
        if (dateTo != "")
        {
            filter += separ + "(CreateDate <= DateTime.Parse(\"" + dateTo + "\"))";
            separ = "&&";
        }
        LinqDataSource1.Where = filter;
        LinqDataSource1.DataBind();
        ListView1.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        updateFilter();
    }

    protected void InsertButtonInit(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        btn.Visible = Page.User.IsInRole("Admin");
    }

    protected void ListView1_ItemInserting(object sender, ListViewInsertEventArgs e)
    {
        Page.Response.Redirect("~/LoggedIn/AdminZone/InsertNew.aspx?category=" + Page.Request.Params["category"]);
    }
    protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
    {                
    }
    protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            Page.Response.Redirect("~/LoggedIn/AdminZone/EditNew.aspx?id=" + e.CommandArgument.ToString());
        }
        if (e.CommandName == "Delete")
        {
            ListView1.DeleteItem(ListView1.SelectedIndex);
        }
    }

    public void EditButtonClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        Page.Response.Redirect("~/LoggedIn/AdminZone/EditNew.aspx?id=" + btn.CommandArgument.ToString());
    }

    public void InsertButtonClick(object sender, EventArgs e)
    {        
        Page.Response.Redirect("~/LoggedIn/AdminZone/InsertNew.aspx?category=" + Page.Request.Params["category"]);
    }


    public void DeleteButtonClick(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        ListViewDataItem item =(ListViewDataItem)btn.Parent;
        ListView1.DeleteItem(item.DataItemIndex);
        
    }


}
