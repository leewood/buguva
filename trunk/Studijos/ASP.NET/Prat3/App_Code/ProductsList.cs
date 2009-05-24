using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductsList
/// </summary>
public class ProductsList
{
	public ProductsList()
	{
        
	}
    public static List<Product> Products
    {
        get
        {
            MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();
            return (from g in context.Products select g).ToList();
        }
    }
}
