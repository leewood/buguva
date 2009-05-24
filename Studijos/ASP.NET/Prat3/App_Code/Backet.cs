using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Backet
/// </summary>
/// 
public class BacketLine
{
    public Product Product
    {
        get;
        set;
    }

    public decimal Price
    {
        get
        {
            return OnePrice * Count;
        }
        
    }

    public String Name
    {
        get;
        set;
    }

    public int Count
    {
        get;
        set;
    }

    public decimal OnePrice
    {
        get;
        set;
    }
    public int RowIndex
    {
        get;
        set;
    }

}

public class Backet
{
	public Backet()
	{

	}

    private Order _tempOrder;
    private string _currency = "LTL";

    public string Currency
    {
        get
        {
            return _currency;
        }
        set
        {
            _currency = value;
        }
    }

    public void Clear()
    {
        this._tempOrder = null;
    }


    public Order TempOrder
    {
        get
        {
            if (_tempOrder == null)
            {
                _tempOrder = new Order();
                _tempOrder.Person = (HttpContext.Current.User.Identity.IsAuthenticated) ? HttpContext.Current.User.Identity.Name : "Guest";
                _tempOrder.Status = 0;                
            }
            return _tempOrder;
        }
        set
        {
            _tempOrder = value;
        }
    }

    public static List<BacketLine> BacketLines
    {
        get
        {
            var d = (from h in OrderLines select new BacketLine() { Product = h.Product, Count = h.Count ?? 0, OnePrice = Rates.PriceInCurrency(h.ProductPrice, "LTL", MyActiveCurrency), Name = h.ProductName }).ToList();
            for (int i = 0; i < d.Count; i++)
            {
                d[i].RowIndex = 0;
            }
            return d;
        }
    }

    public static string MyActiveCurrency
    {
        get
        {
            return CurrentBacket.Currency;
        }
    }

    public static decimal Total
    {
        get
        {
            return BacketLines.Sum(d => d.Price);
        }
    }

    public static List<OrderLine> OrderLines
    {
        get
        {
            return CurrentBacket.TempOrder.OrderLines.ToList();
        }
    }


    public void OrderProduct(int id)
    {
        OrderLine line = new OrderLine();
        line.Count = 0;
        line.ProductID = id;
        line.Count = 1;
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        line.Product = (from d in context.Products where d.id == id select d).First();
        if (TempOrder.OrderLines == null)
        {
            TempOrder.OrderLines = new System.Data.Linq.EntitySet<OrderLine>();
        }
        TempOrder.OrderLines.Add(line);        
    }

    public void RemoveLine(int lineNo)
    {
        TempOrder.OrderLines.RemoveAt(lineNo);
    }

    public void Increase(int lineNo)
    {
        TempOrder.OrderLines[lineNo].Count++;
    }

    public void Descrease(int lineNo)
    {
        if (TempOrder.OrderLines[lineNo].Count <= 1)
        {
            TempOrder.OrderLines.RemoveAt(lineNo);
        }
        else
        {
            TempOrder.OrderLines[lineNo].Count--;
        }
    }

    public void OrderIt(string description, string userName)
    {
        TempOrder.OrderDate = DateTime.Now;
        TempOrder.Status = 1;
        TempOrder.Person = userName;
        TempOrder.Description = description;
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
        context.Orders.InsertOnSubmit(TempOrder);
        context.SubmitChanges();
        _tempOrder = null;
    }

    public void OrderIt(string description)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            TempOrder.OrderDate = DateTime.Now;
            TempOrder.Status = 1;
            TempOrder.Person = HttpContext.Current.User.Identity.Name;
            TempOrder.Description = description;
            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            context.Orders.InsertOnSubmit(TempOrder);
            context.SubmitChanges();
            _tempOrder = null;
        }
        else
        {
            HttpContext.Current.Response.Redirect("~/Login.aspx");
        }
    }

    public static Backet CurrentBacket
    {
        get
        {
            if (HttpContext.Current.Session["MyBacket"] == null)
            {
                HttpContext.Current.Session["MyBacket"] = new Backet();
            }
            return (Backet)HttpContext.Current.Session["MyBacket"];
        }
        set
        {
            HttpContext.Current.Session["MyBacket"] = value;
        }
    }
}
