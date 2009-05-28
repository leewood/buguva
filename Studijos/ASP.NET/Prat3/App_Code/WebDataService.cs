using System;
using System.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web.Security;
using System.Linq.Expressions;




[System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class WebDataService : DataService<ServiceModel.Entities>
{


    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(IDataServiceConfiguration config)
    {
        // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
        // Examples:
        //config.SetEntitySetAccessRule("*", EntitySetRights.All);
        config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        config.SetEntitySetAccessRule("*", EntitySetRights.All);
        config.SetEntitySetAccessRule("Product", EntitySetRights.AllRead);
        
        config.SetEntitySetAccessRule("Categories", EntitySetRights.AllRead);
        config.SetEntitySetAccessRule("Order", EntitySetRights.AllRead);
        config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        config.SetServiceOperationAccessRule("MyOrders", ServiceOperationRights.All);
        config.SetServiceOperationAccessRule("MakeOrder", ServiceOperationRights.All);
        
    }

    protected override void HandleException(HandleExceptionArgs args)
    {
        if (args.Exception.InnerException is ArgumentException)
        {
            ArgumentException e = (ArgumentException)
                        args.Exception.InnerException;
            args.Exception = new DataServiceException(400,
                    "PropertySyntaxError:" + e.ParamName,
                    "human readable description of problem",
                    "en-US",
                     e);
        }
        else
        {
            args.Exception = new DataServiceException(400, args.Exception.StackTrace, "", "en-US", args.Exception);
        }
    }

    [WebInvoke] 
    public IQueryable<ServiceModel.Order> MyOrders()
    {
        MembershipProvider provider = System.Web.Security.Membership.Provider;
        if ((username == null) && (password == null))
        {
            throw new Exception("Failed to login");
        }
        if (!provider.ValidateUser(username, password))
        {
            throw new Exception("Failed to login");
        }
        ServiceModel.Entities enti = new ServiceModel.Entities();
        return from a in enti.Order where a.Person == username select a;

    }

    private string username = null;
    private string password = null;

    protected override void OnStartProcessingRequest(ProcessRequestArgs args)
    {
        args.RequestUri.OriginalString.ToString();
        string s = args.RequestUri.OriginalString;
        username = null;
        password = null;
        int index = s.IndexOf("username=");        
        if (index >= 0)
        {
            int i = s.IndexOf("&", index);
            int j = i;
            if (i < 0)
            {
                i = s.Length;
                j = s.Length - 1;
            }
            username = s.Substring(index + 9, i - index - 9);
            s = s.Remove(index, j - index + 1);
            if (s[s.Length - 1] == '?') s = s.Remove(s.Length - 1);
        }
        index = s.IndexOf("password=");
        if (index >= 0)
        {
            int i = s.IndexOf("&", index);
            int j = i;
            if (i < 0)
            {
                i = s.Length;
                j = s.Length - 1;
            }
            password = s.Substring(index + 9, i - index - 9);
            s = s.Remove(index, j - index + 1);
            if (s[s.Length - 1] == '?') s = s.Remove(s.Length - 1);
        }

        //ProcessRequestArgs arg2 = new ProcessRequestArgs();
        //MyArgs arg2 = new MyArgs();
        
        base.OnStartProcessingRequest(args);
    }

    [QueryInterceptor("Order")]
    public Expression<Func<ServiceModel.Order, bool>> Intercept()
    {
        
        return c => false;
    }


    [WebGet]    
    public string MakeOrder(string products, string username, string password)
    {
        if ((products == null) || (products == ""))
        {
            return "You must choose at least one product";
        }
        if ((username == null) || (username == ""))
        {
            return "You must give a username";
        }
        if (password == null)
        {
            return "You must give a password";
        }
        MembershipProvider provider = System.Web.Security.Membership.Provider;

        if (!provider.ValidateUser(username, password))
        {
            return "Fail to login. Check your username and password";
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

}
