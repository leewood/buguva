using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
/// <summary>
/// Summary description for ThemesModel
/// </summary>
/// 
public class ThemeDef
{
    public string Name
    {
        get;
        set;
    }
}

public class ThemesModel
{

    public List<ThemeDef> Themes
    {
        get
        {
            List<string> result = new List<string>();
            System.IO.FileInfo finfo = new System.IO.FileInfo(HttpContext.Current.Request.PhysicalPath);
            System.IO.DirectoryInfo dinfo = new System.IO.DirectoryInfo(finfo.Directory.Parent.FullName + "\\App_Themes");
            DirectoryInfo[] list = dinfo.GetDirectories();
            var res = (from a in list select new ThemeDef() { Name = a.Name }).ToList();
            return res;
        }
    }

	public ThemesModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}
