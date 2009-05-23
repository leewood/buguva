using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Web;
using System.Web;
using System.Data.Linq;
using System.Data.Entity;
using System.Data.Linq.SqlClient;


[System.ServiceModel.ServiceBehavior(IncludeExceptionDetailInFaults = true)]
public class SearchService : DataService<MainDBDataClassesDataContext>
{
    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(IDataServiceConfiguration config)
    {
        config.SetEntitySetAccessRule("*", EntitySetRights.All);
        config.SetServiceOperationAccessRule("*", ServiceOperationRights.AllRead);
        config.RegisterKnownType(typeof(MainDBDataClassesDataContext));
        config.RegisterKnownType(typeof(Rates));
        config.RegisterKnownType(typeof(Backet));
        config.RegisterKnownType(typeof(Category));
        
        
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
    }

    [WebGet]
    public IQueryable<Product> ProductsByCategory(string category)
    {
        MainDBDataClassesDataContext context = new MainDBDataClassesDataContext();

        return from c in context.Products
               where c.Category == category
               select c;
    }
    
}
