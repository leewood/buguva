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

    public int ProductID
    {
        get
        {
            if (Product != null)
            {
                return Product.id;
            }
            else
            {
                return 0;
            }
        }
    }

    public decimal Price
    {
        get
        {
            return OnePrice * Count;
        }
        
    }

    public int PriceCents
    {
        get
        {
            
            return ((int)(Price * 100)) % 100;
        }
    }

    public int PriceLits
    {
        get
        {
            return ((int)(Price * 100)) / 100; 
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
                d[i].RowIndex = i;
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
        set
        {
            CurrentBacket.Currency = value;
        }
    }

    public static decimal Total
    {
        get
        {
            return BacketLines.Sum(d => d.Price);
        }
    }

    public static int TotalCents
    {
        get
        {
            return ((int)(Total * 100)) % 100;
        }
    }

    public static int TotalLits
    {
        get
        {
            return ((int)(Total * 100)) / 100;
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
        var h = (from i in TempOrder.OrderLines where i.ProductID == id select i).ToList();

        if (h.Count > 0)
        {
            h[0].Count++;
        }
        else
        {
            TempOrder.OrderLines.Add(line);
        }
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
        Order TempOrder2 = new Order();
        TempOrder2.OrderDate = DateTime.Now;
        TempOrder2.Status = 1;
        TempOrder2.Person = HttpContext.Current.User.Identity.Name;
        TempOrder2.Description = description;
        TempOrder2.OrderLines = new System.Data.Linq.EntitySet<OrderLine>();
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();

        foreach (OrderLine line in TempOrder.OrderLines)
        {
            int id = line.ProductID.Value;
            OrderLine line2 = new OrderLine() { Count = line.Count, ProductID = line.ProductID, Product = (from d in context.Products where d.id == id select d).First() };
            TempOrder2.OrderLines.Add(line2);
        }

        context.Orders.InsertOnSubmit(TempOrder2);
        context.SubmitChanges();
        _tempOrder = null;
    }

    public void OrderIt(string description)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Order TempOrder2 = new Order();            
            TempOrder2.OrderDate = DateTime.Now;
            TempOrder2.Status = 1;
            TempOrder2.Person = HttpContext.Current.User.Identity.Name;
            TempOrder2.Description = description;
            TempOrder2.OrderLines = new System.Data.Linq.EntitySet<OrderLine>();
            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            
            foreach (OrderLine line in TempOrder.OrderLines)
            {
                int id = line.ProductID.Value;           
                OrderLine line2 = new OrderLine() { Count = line.Count, ProductID = line.ProductID, Product = (from d in context.Products where d.id == id select d).First() };
                TempOrder2.OrderLines.Add(line2);
            }
            
            context.Orders.InsertOnSubmit(TempOrder2);
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
