using System.Web.Mvc;
using System.Web.Routing;
using MvcSubdomainRouting;
using TestWebApp.Constraints;

namespace TestWebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapSubDomainRoute("FixedController",
                                     "specialcontroller", // SubDomain 
                                     "{action}/{id}", // URL with parameters
                                      new { controller = "Special", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                                      );

            routes.MapSubDomainRoute("OtherRoute",
                                     "{someValue}", // SubDomain 
                                     "{action}/{id}", // URL with parameters
                                      new { controller = "Other", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                                      new { someValue = new SubDomainConstraint() } // constraint so we only deal the subdomains we want
                                      );
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}