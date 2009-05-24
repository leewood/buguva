using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Security;

/// <summary>
/// Summary description for OrderingService
/// </summary>
/// 

[WebService(Namespace = "http://nku2007.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.None)]
public class UserHeader : System.Web.Services.Protocols.SoapHeader
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

[WebService(Namespace = "http://nku2007.com/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class OrderingService : System.Web.Services.WebService {

    public UserHeader userHeader; 

    public OrderingService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string OrderWithoutHeader(string products, string username, string password)
    {
        if ((products == null) || (products == ""))
        {
            throw new Exception("You must choose at least one product");

        }
        if ((username == null) || (username == ""))
        {
            throw new Exception("You must give username");
        }
        if ((password == null))
        {
            throw new Exception("You must give password");
        }
        MembershipProvider provider = System.Web.Security.Membership.Provider;

        if (!provider.ValidateUser(username, password))
        {
            throw new Exception("Fail to login. Check your username and password");
        }
        Backet backet = new Backet();
        string[] lines = products.Split(new char[] { ',' });

        foreach (string line in lines)
        {
            try
            {
                backet.OrderProduct(int.Parse(line));
            }
            catch (Exception)
            {
            }
        }

        backet.OrderIt("", username);
        return "Order accepted";

    }

    [WebMethod]
    [SoapHeader("userHeader")]
    public string Order(string products)
    {
        if (this.userHeader == null)
        {
            throw new Exception("Services is available for registered users only");

        }
        if ((products == null) || (products == ""))
        {
            throw new Exception("You must choose at least one product");
            
        }
        MembershipProvider provider = System.Web.Security.Membership.Provider;                        

        if (!provider.ValidateUser(userHeader.UserName, userHeader.Password))
        {
            throw new Exception("Fail to login. Check your username and password");
        }
        Backet backet = new Backet();
        string[] lines = products.Split(new char[] { ',' });

        foreach (string line in lines)
        {
            try
            {
                backet.OrderProduct(int.Parse(line));
            }
            catch (Exception)
            {
            }
        }

        backet.OrderIt("", userHeader.UserName);
        return "Order accepted";
    }
    
}

