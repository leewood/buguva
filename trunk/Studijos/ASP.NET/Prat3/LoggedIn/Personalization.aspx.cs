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
using System.IO;
using System.Threading;
using System.Globalization;

public partial class LoggedIn_Personalization : ExtendedPage
{
    public override string StyleSheetTheme
    {
        get
        {
            return Theme;
        }
        set
        {
            base.StyleSheetTheme = value;
        }
    }

    public LoggedIn_Personalization()
    {
        this.PreRender += new EventHandler(LoggedIn_Personalization_PreRender);
    }

    void LoggedIn_Personalization_PreRender(object sender, EventArgs e)
    {
        DropDownList1.SelectedValue = Session["Theme"].ToString();
    }


    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!Page.IsPostBack)
        {
            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            var conf = (from a in context.UserConfigs where a.Username == Page.User.Identity.Name select a).ToList();
            if (conf.Count == 0)
            {
                UserConfig uconf = new UserConfig();
                uconf.Username = Page.User.Identity.Name;                
                string cult = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                int index = cult.IndexOf("-");
                string lang = cult;
                string cultL = "";
                if (index >= 0)
                {
                    lang = cult.Substring(0, index);
                    cultL = cult.Substring(index + 1);
                }
                uconf.Culture = cultL;
                uconf.Language = lang;
                uconf.Theme = Page.Theme;
                context.UserConfigs.InsertOnSubmit(uconf);
                context.SubmitChanges();
                TextBox1.Text = lang;
                TextBox2.Text = cultL;
                DropDownList1.SelectedValue = Page.Theme;
            }
            else
            {
                UserConfig confU = conf[0];
                TextBox1.Text = confU.Language;
                TextBox2.Text = confU.Culture;
                //DropDownList1.SelectedValue = confU.Theme;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        var conf = (from a in context.UserConfigs where a.Username == Page.User.Identity.Name select a).ToList();
        conf[0].Language = TextBox1.Text;
        conf[0].Culture = TextBox2.Text;
        conf[0].Theme = DropDownList1.SelectedValue;
        context.SubmitChanges();
        Page.Culture = (conf[0].Culture != "") ? (conf[0].Language + "-" + conf[0].Culture) : conf[0].Language;
        MyCulture = "";
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(MyCulture);
        Session["Theme"] = conf[0].Theme;
        this.Response.Redirect("~/LoggedIn/Personalization.aspx");
    }
}
