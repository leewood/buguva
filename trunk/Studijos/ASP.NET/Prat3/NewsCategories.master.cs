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

public partial class NewsCategories : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    public void UpdateMenu()
    {

            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            var dataSource = (from a in context.Categories select new MenuItem() { Text = a.Name, NavigateUrl = "~/ProductsList.aspx?category=" + a.Name }).ToList();
            this.Menu1.Items.Clear();
            string allText = GetLocalResourceObject("All").ToString();
            dataSource.Add(new MenuItem() { Text = allText, NavigateUrl = "~/ProductsList.aspx" });
            foreach (MenuItem item in dataSource)
            {
                Menu1.Items.Add(item);
            }            
    }
    protected void Menu1_Init(object sender, EventArgs e)
    {
        UpdateMenu();
        System.IO.FileInfo finfo = new System.IO.FileInfo(this.Page.Request.Url.AbsolutePath);
        string add = ((Request.Params["category"] != null) && (Request.Params["category"] != "")) ? ("?category=" + Request.Params["category"]) : "";
        for (int i = 0; i < Menu1.Items.Count; i++)
        {
            if (Menu1.Items[i].NavigateUrl == "~/" + finfo.Name + add)
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
