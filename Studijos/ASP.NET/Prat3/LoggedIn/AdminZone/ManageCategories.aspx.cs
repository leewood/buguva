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
using System.Collections.Generic;

public partial class LoggedIn_AdminZone_ManageCategories : ExtendedPage
{    

    public Dictionary<string, string> Filters
    {
        get
        {
            if (ViewState["Filters"] == null)
            {
                ViewState["Filters"] = new Dictionary<string, string>();
            }
            return (Dictionary<string, string>)ViewState["Filters"];
        }
        set
        {
            ViewState["Filters"] = value;
        }
    }

    public string SortMode
    {
        get
        {
            if (ViewState["SortMode"] != null)
            {
                return ViewState["SortMode"].ToString();
            }
            else
            {
                return "";
            }
        }
        set
        {
            ViewState["SortMode"] = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        HtmlTableRow tr = (HtmlTableRow)ListView1.FindControl("Tr1");
        DataPager pager = ((DataPager)tr.FindControl("Td1").FindControl("DataPager1"));
        int pageCount = (pager.TotalRowCount / pager.PageSize) + ((pager.TotalRowCount % pager.PageSize > 0) ? 1 : 0);
        if (pageCount < 2)
        {
            tr.Visible = false;
        }
    }

    public void updateFilter()
    {
        LinqDataSource1.Where = "";
        string filter = "";
        string sep = "";
        foreach (KeyValuePair<string, string> pair in Filters)
        {
            if (pair.Value != "")
            {
                filter += sep + "(" + pair.Key + pair.Value + ")";
                sep = "&&";
            }
        }
        LinqDataSource1.Where = filter;
        LinqDataSource1.DataBind();
        LinqDataSource1.OrderBy = SortMode;
        ListView1.DataBind();
    }

    public void FilterClick(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Panel panel = (Panel)ListView1.FindControl(btn.CommandArgument + "FilterPanel");
        panel.Visible = !panel.Visible;
    }

    public void FilterOK(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        Panel panel = (Panel)ListView1.FindControl(btn.CommandArgument + "FilterPanel");
        panel.Visible = false;
        Control parent = btn.Parent;
        TextBox textBox = (TextBox)parent.FindControl(btn.CommandArgument + "Filter");
        SetFilter(btn.CommandArgument, textBox.Text);
        updateFilter();
    }

    public void SetFilter(string name, string value)
    {
        if (Filters.ContainsKey(name))
        {
            Filters[name] = value;
        }
        else
        {
            Filters.Add(name, value);
        }
    }

    public void Sort(object sender, EventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        SortMode = btn.CommandArgument;
        updateFilter();
    }
}
