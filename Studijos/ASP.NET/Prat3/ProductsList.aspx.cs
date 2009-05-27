using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Linq.SqlClient;
using System.Web.UI.HtmlControls;

public partial class ProductsList : ExtendedPage
{
    public ProductsList()
    {
        this.PreRender += new EventHandler(ProductsList_PreRender);
    }

    void ProductsList_PreRender(object sender, EventArgs e)
    {
        Label5.Text = Backet.MyActiveCurrency;   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string filters = "";
        string sep = "";
        Label5.Text = Backet.MyActiveCurrency;
        if ((Request.Params["category"] != null) && (Request.Params["category"] != ""))
        {
            filters += sep + String.Format("(Category == \"{0}\")", Request.Params["category"]);
            sep = " && ";
        }
        if (NameTextBox.Text != "")
        {
            filters += sep + String.Format("(Name.IndexOf(\"{0}\") >= 0)", NameTextBox.Text);
            sep = " && ";
        }
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        var list = (from g in context.Products select g).ToList();
        
        if (LowPrice.Text != "")
        {
            try
            {
                decimal lowPrice = decimal.Parse(LowPrice.Text);
                filters += sep + String.Format("(ActivePrice >= {0})", lowPrice);
                sep = " && ";
            }
            catch (Exception)
            {
            }
        }
        if (HighPrice.Text != "")
        {
            try
            {
                decimal highPrice = decimal.Parse(HighPrice.Text);
                filters += sep + String.Format("(ActivePrice <= {0})", highPrice);
                sep = " && ";
            }
            catch (Exception)
            {
            }
        }
        LinqDataSource1.Where = filters;
        LinqDataSource1.DataBind();
        ListView1.DataBind();
        HtmlTableRow tr = (HtmlTableRow)ListView1.FindControl("Tr1");
        if (tr != null)
        {
            DataPager pager = ((DataPager)tr.FindControl("Td1").FindControl("DataPager1"));
            int pageCount = (pager.TotalRowCount / pager.PageSize) + ((pager.TotalRowCount % pager.PageSize > 0) ? 1 : 0);
            if (pageCount < 2)
            {
                tr.Visible = false;
            }
        }

    }

    protected void PreviewButtonClick(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/ShowProduct.aspx?id={0}", ((ImageButton)sender).CommandArgument.ToString()));
    }

    protected void OrderButtonClick(object sender, EventArgs e)
    {
        Backet.CurrentBacket.OrderProduct(int.Parse(((ImageButton)sender).CommandArgument.ToString()));
        this.DataBind();
        
    }

    protected void DeleteButtonClick(object sender, EventArgs e)
    {
        int id = int.Parse(((ImageButton)sender).CommandArgument.ToString());
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        context.Products.DeleteOnSubmit(context.Products.First(p => p.id == id));
        context.SubmitChanges();
        LinqDataSource1.DataBind();
        ListView1.DataBind();
        //this.DataBind();
        this.Response.Redirect(String.Format("~/ProductsList.aspx?category={1}", id, Request.Params["category"]));
    }

    protected void EditButtonClick(object sender, EventArgs e)
    {
        int id = int.Parse(((ImageButton)sender).CommandArgument.ToString());
        this.Response.Redirect(String.Format("~/LoggedIn/AdminZone/EditProduct.aspx?id={0}&category={1}", id, Request.Params["category"]));

    }

    protected void NewButtonClick(object sender, EventArgs e)
    {
        int id = int.Parse(((ImageButton)sender).CommandArgument.ToString());
        this.Response.Redirect("~/LoggedIn/AdminZone/NewProduct.aspx?category=" + Request.Params["category"]);

    }



}
