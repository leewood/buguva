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
    protected void Page_Load(object sender, EventArgs e)
    {
        string filters = "";
        string sep = "";
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
        DataPager pager = ((DataPager)tr.FindControl("Td1").FindControl("DataPager1"));
        int pageCount = (pager.TotalRowCount / pager.PageSize) + ((pager.TotalRowCount % pager.PageSize > 0) ? 1 : 0);
        if (pageCount < 2)
        {
            tr.Visible = false;
        }

    }

    protected void PreviewButtonClick(object sender, EventArgs e)
    {
        Response.Redirect(String.Format("~/ShowProduct.aspx?id={0}", ((ImageButton)sender).CommandArgument.ToString()));
    }
}
