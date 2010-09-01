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

public partial class GenresList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        int pageSize = GridView1.PageSize;
        try
        {
            pageSize = int.Parse(pageSizeTextBox.Text);
        }
        catch (FormatException)
        {
        }
        GridView1.PageSize = pageSize;
        mainSqlDataSource.DataBind();
        GridView1.DataBind();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string key = GridView1.DataKeys[rowIndex].Value.ToString();
            Page.Response.Redirect("GenresChangeForm.aspx?mode=Edit&id=" + key);
        }
        else if (e.CommandName == "Select")
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            string key = GridView1.DataKeys[rowIndex].Value.ToString();
            Page.Response.Redirect("GenresChangeForm.aspx?mode=View&id=" + key);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Page.Response.Redirect("GenresChangeForm.aspx?mode=Insert");
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        Page.Response.Redirect("GenresChangeForm.aspx?mode=Insert");
    }
}
