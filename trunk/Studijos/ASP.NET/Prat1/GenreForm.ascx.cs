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

public partial class GenreForm : System.Web.UI.UserControl
{
    public string RedirectURL
    {
        get;
        set;
    }

    public string ActiveID
    {
        get
        {
            
            return SelectedIDHiddenField.Value;
        }
        set
        {

            SelectedIDHiddenField.Value = value;
            animeDataSource.DataBind();
            FormView1.DataBind();
            FormView1.PageIndex = 0;
        }
    }

    public FormViewMode FormMode
    {
        get
        {
            return FormView1.CurrentMode;
        }
        set
        {
            FormView1.ChangeMode(value);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Validate();
        if (!Page.IsPostBack)
        {
            if ((Page.Request.QueryString["id"] != null) && (Page.Request.QueryString["id"] != ""))
            {

                this.ActiveID = Page.Request.QueryString["id"];
            }
            if ((Page.Request.Params["mode"] != null) && (Page.Request.Params["mode"] != ""))
            {
                string mode = Page.Request.Params["mode"];
                switch (mode)
                {
                    case "View": this.FormMode = FormViewMode.ReadOnly; break;
                    case "Edit": this.FormMode = FormViewMode.Edit; break;
                    case "Insert": this.FormMode = FormViewMode.Insert; break;
                    default: this.FormMode = FormViewMode.ReadOnly; break;
                }
            }
        }

    }
    protected void FormView1_ItemInserted(object sender, FormViewInsertedEventArgs e)
    {
        
        if (Page.IsValid)
        {
            this.Page.Response.Redirect(RedirectURL);
        }
    }
    protected void FormView1_ItemUpdated(object sender, FormViewUpdatedEventArgs e)
    {
        if (Page.IsValid)
        {
            this.Page.Response.Redirect(RedirectURL);
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        this.Page.Response.Redirect(RedirectURL);
    }
    protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
    {
        this.Page.Response.Redirect(RedirectURL);
    }
    protected void FormView1_ItemCommand(object sender, FormViewCommandEventArgs e)
    {
        if (e.CommandName == "Cancel")
        {
            this.Page.Response.Redirect(RedirectURL);
        }
    }

}

