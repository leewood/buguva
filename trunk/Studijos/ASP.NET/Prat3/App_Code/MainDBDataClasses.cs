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
using System.Data.Services;

/// <summary>
/// Summary description for MainDBDataClasses
/// </summary>
///


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

[IgnoreProperties(new string[] { "ProductName", "ProductPrice" })]
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

[IgnoreProperties(new string[] { "NewsCount", "Title" })]
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

public partial class Order
{
    public void Confirm()
    {
        this.Status = 2;
        this.ConfirmDate = DateTime.Now;
    }

    public void Reject()
    {
        this.Status = 3;
    }

    public void Complete()
    {
        this.Status = 4;
    }

    public bool SpecButtonsVisible
    {
        get
        {
            return ((this.Status == 1) && (HttpContext.Current.User.IsInRole("Admin")));
        }
    }



    public string StatusString
    {
        get
        {
            switch (Status)
            {
                case 0: return "New";
                case 1: return "Ordered";
                case 2: return "Confirmed";
                case 3: return "Rejected";
                case 4: return "Done";
                default: return "";
            }
        }
    }

}

public partial class Product
{
    public decimal ActivePrice
    {
        get
        {
            return Rates.PriceInCurrency(this.Price, this.Currency, Backet.MyActiveCurrency);
        }
    }
}