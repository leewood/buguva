using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for Rates
/// </summary>
/// 

public class CultureSpecificCurrencyItem
{
    public string Code
    {
        get;
        set;
    }

    public string Description
    {
        get;
        set;
    }
}

public class CurrencyItem
{
    public string Code
    {
        get;
        set;
    }

    public Dictionary<string, string> Descriptions
    {
        get;
        set;
    }

    public string GetDescription(string language)
    {
        if (Descriptions != null)
        {
            if (Descriptions.ContainsKey(language))
            {
                return Descriptions[language];
            }
            else if (Descriptions.ContainsKey("en"))
            {
                return Descriptions["en"];
            }
            else 
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }

    public void SetDescription(string language, string description)
    {
        if (Descriptions == null)
        {
            Descriptions = new Dictionary<string,string>();
        }
        if (Descriptions.ContainsKey(language))
        {
            Descriptions[language] = description;
        }
        else
        {
            Descriptions.Add(language, description);
        }

    }
}

public class Rates
{
    public Rates()
    {
    }
    //
    // TODO: Add constructor logic here
    //
    private static string getLang()
    {
        string name = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        int index = name.IndexOf("-");
        if (index >= 0)
        {
            name = name.Substring(0, index);
        }
        return name;
    }

    public static List<CultureSpecificCurrencyItem> RatesList
    {
        get
        {
            lt.lb.webservices.ExchangeRates rates = new lt.lb.webservices.ExchangeRates();
            XmlNode nodes = rates.getListOfCurrencies();
            List<CurrencyItem> items = new List<CurrencyItem>();
            foreach (XmlNode node in nodes.ChildNodes)
            {
                CurrencyItem item = new CurrencyItem();
                item.Code = node.SelectSingleNode("currency").FirstChild.Value;
                foreach (XmlNode desc in node.SelectNodes("description"))
                {
                    item.SetDescription(desc.Attributes["lang"].Value, desc.FirstChild.Value);
                }
                items.Add(item);
            }
            string lang = getLang();
            var r = (from d in items select new CultureSpecificCurrencyItem() { Code = d.Code, Description = d.GetDescription(lang) }).ToList();
            r.Add(new CultureSpecificCurrencyItem() { Code = "LTL", Description = LitasName(lang) });
            return r;
        }
    }

    public static string LitasName(string language)
    {
        switch (language)
        {
            case "lt": return "Lietuvos litas";
            case "en": return "Lithuanian Litas";
            default: return "Lithuanian Litas";
        }        
    }

    public static decimal GetCurrentExchangeRate(string currencyCode)
    {
        if (currencyCode == "LTL")
        {
            return 1;
        }
        else
        {
            lt.lb.webservices.ExchangeRates rates = new lt.lb.webservices.ExchangeRates();
            return rates.getCurrentExchangeRate(currencyCode);
        }

    }

    public static decimal PriceInCurrency(decimal realPrice, string realCurrency, string currency)
    {
        lt.lb.webservices.ExchangeRates rates = new lt.lb.webservices.ExchangeRates();
        decimal rR = GetCurrentExchangeRate(realCurrency);
        decimal nR = GetCurrentExchangeRate(currency);
        return realPrice * rR / nR;
    }

}

