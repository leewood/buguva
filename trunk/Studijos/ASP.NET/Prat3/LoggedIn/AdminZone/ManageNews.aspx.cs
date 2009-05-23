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

public partial class LoggedIn_AdminZone_ManageNews : ExtendedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //((Label)ListView1.FindControl("InsertPlace").FindControl("InsertCreator")).Text = User.Identity.Name;
        //((Label)ListView1.FindControl("InsertPlace").FindControl("InsertCreateDate")).Text = DateTime.Now.ToString();
        //((Label)ListView1.FindControl("InsertPlace").FindControl("InsertLastModifiedDate")).Text = DateTime.Now.ToString();
        HtmlTableRow tr = (HtmlTableRow)ListView1.FindControl("Tr1");
        DataPager pager = ((DataPager)tr.FindControl("Td1").FindControl("DataPager1"));
        int pageCount = (pager.TotalRowCount / pager.PageSize) + ((pager.TotalRowCount % pager.PageSize > 0) ? 1 : 0);
        if (pageCount < 2)
        {
            tr.Visible = false;
        }
    }

    public void CategoryInit(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        ListViewDataItem cnt = (ListViewDataItem)list.Parent;
        New newData = (New)cnt.DataItem;
        if (newData != null)
        {
            list.DataBind();
            int index = 0;
            for (int i = 0; i < list.Items.Count; i++)
            {
                ListItem item = list.Items[i];
                if (item.Value == newData.Category)
                {
                    index = i;
                }
            }
            list.SelectedIndex = index;
        }
    }


    public void CategoryInsertInit(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        
    }
    public void CategoryChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        if (list.SelectedIndex < 0)
        {
            list.DataBind();
            list.SelectedIndex = 0;
           
        }

        ((TextBox)list.Parent.FindControl("CategoryTextBox")).Text = list.SelectedValue;
    }

    public void InitCreator(object sender, EventArgs e)
    {
        Label label = (Label)sender;
        label.Text = User.Identity.Name;
    }

    public void InitDate(object sender, EventArgs e)
    {
        Label label = (Label)sender;
        label.Text = DateTime.Now.ToString();
    }

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
