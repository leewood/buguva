using System;
using System.Data;
using System.Configuration;
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
/// <summary>
/// Summary description for ExtendedPage
/// </summary>
public class ExtendedPage: System.Web.UI.Page
{
    private string _theme = "";
    

    public override string Theme
    {
        get
        {
            if (_theme == "")
            {
                if (Session["Theme"] == null)
                {
                    if (this.User.Identity.IsAuthenticated)
                    {
                        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
                        var conf = (from a in context.UserConfigs where a.Username == User.Identity.Name select a).ToList();
                        if (conf.Count > 0)
                        {
                            Session["Theme"] = conf[0].Theme;
                        }
                    }
                }
                if (Session["Theme"] != null)
                {
                    _theme = Session["Theme"].ToString();
                }
                else
                {
                    _theme = "Default";
                }
            }
            return _theme;
        }
        set
        {
            _theme = value;
        }
    }

    private string _cult = "";

    public string MyCulture
    {
        get
        {
            if (_cult == "")
            {
                MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
                var conf = (from a in context.UserConfigs where a.Username == Page.User.Identity.Name select a).ToList();
                if (conf.Count > 0)
                {
                    _cult = (conf[0].Culture != "") ? (conf[0].Language + "-" + conf[0].Culture) : conf[0].Language;
                }
                else
                {
                    _cult = "en-US";
                }
            }
            return _cult;
        }
        set
        {
            _cult = "";
        }
    }

	public ExtendedPage()
	{
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(MyCulture);
        UICulture = MyCulture;        
	}

    protected override void InitializeCulture()
    {
        base.InitializeCulture();
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(MyCulture);
        UICulture = MyCulture;        

    }
}
