using System;
using System.Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Web.Security;




[System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class WebDataService : DataService<Prat2Model.Prat2Entities>
{
    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(IDataServiceConfiguration config)
    {
        // TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
        // Examples:
        config.SetEntitySetAccessRule("*", EntitySetRights.None);
        config.SetEntitySetAccessRule("Product", EntitySetRights.AllRead);
        config.SetServiceOperationAccessRule("*", ServiceOperationRights.All);
        config.SetServiceOperationAccessRule("Order", ServiceOperationRights.All);
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

    [WebGet]    
    public string Order(string products, string username, string password)
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
