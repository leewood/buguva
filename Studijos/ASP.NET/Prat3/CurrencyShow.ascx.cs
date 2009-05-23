using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CurrencyShow : System.Web.UI.UserControl, System.Web.UI.IBindableControl
{

    [System.ComponentModel.Bindable(true, System.ComponentModel.BindingDirection.TwoWay)]
    [System.ComponentModel.Browsable(true)]    
    public string RealCurrency
    {
        get
        {
            if (ViewState["RealCurrency"] == null)
            {
                ViewState["RealCurrency"] = "";
            }
            return ViewState["RealCurrency"].ToString();
        }
        set
        {
            ViewState["RealCurrency"] = value;
            if ((Currency == null) || (Currency == ""))
            {
                Currency = value;
            }
        }
    }

    private string _rPS;

    [System.ComponentModel.Bindable(true, System.ComponentModel.BindingDirection.TwoWay)]
    [System.ComponentModel.Browsable(true)]
    public string RealPriceString
    {
        get
        {
            return _rPS;
        }
        set
        {
            _rPS = value;
            RealPrice = decimal.Parse(_rPS);
        }
    }

    [System.ComponentModel.Bindable(true)]
    [System.ComponentModel.Browsable(true)]
    public decimal RealPrice
    {
        get
        {
            if (ViewState["RealPrice"] == null)
            {
                ViewState["RealPrice"] = (Decimal)0;
            }
            return (Decimal)ViewState["RealPrice"];
            //return _realPrice;
        }
        set
        {
            ViewState["RealPrice"] = value;
            recalcPrice();
        }
    }

    [System.ComponentModel.Bindable(true)]
    [System.ComponentModel.Browsable(true)]
    public string Currency
    {
        get
        {
            if ((DropDownList1.SelectedValue == null) || (DropDownList1.SelectedValue == ""))
            {
                DropDownList1.SelectedValue = RealCurrency;
                return RealCurrency;
            }
            return DropDownList1.SelectedValue;
        }
        set
        {
            if ((value != "") && (value != null))
            {
                LinqDataSource1.DataBind();
                DropDownList1.SelectedValue = value;
                DropDownList1.DataBind();
                
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LinqDataSource1.DataBind();
        recalcPrice();        
        if (!Page.IsPostBack)
        {
            Currency = RealCurrency;
        }
        //((WebControl)DropDownList1.Parent.Parent).Attributes.Add("class", "item");
        //bla.Text = "|" + RealCurrency + " " + RealPriceString + "|";
    }


    private void recalcPrice()
    {
        decimal price = Rates.PriceInCurrency(RealPrice, RealCurrency, Currency);
        Label1.Text = price.ToString("0.00");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        recalcPrice();
    }

    #region IBindableControl Members

    public void ExtractValues(System.Collections.Specialized.IOrderedDictionary dictionary)
    {
        //throw new NotImplementedException();
        dictionary.ToString();
    }

    #endregion
}
