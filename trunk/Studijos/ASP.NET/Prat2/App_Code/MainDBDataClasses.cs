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

/// <summary>
/// Summary description for MainDBDataClasses
/// </summary>
public partial class MainDBDataClassesDataContext : System.Data.Linq.DataContext
{

}

public partial class New
{
    public int CommentsCount
    {
        get
        {
            if (this.Comments != null)
            {
                return this.Comments.Count;
            }
            else
            {
                return 0;
            }
        }
    }
}

public partial class OrderLine
{
    public string ProductName
    {
        get
        {
            if (this.Product != null)
            {
                return this.Product.Name;
            }
            else
            {
                return "";
            }
        }
    }

    public decimal ProductPrice
    {
        get
        {
            if (this.Product != null)
            {
                return Rates.PriceInCurrency(this.Product.Price, this.Product.Currency, "LTL");
            }
            else
            {
                return 0;
            }
        }
    }
}

public partial class Category
{
    public int NewsCount
    {
        get
        {
            if (this.News != null)
            {
                return this.News.Count;
            }
            else
            {
                return 0;
            }
        }
    }

    public string Title
    {
        get
        {
            return String.Format("{0} ({1})", Name, NewsCount);
        }
    }

}