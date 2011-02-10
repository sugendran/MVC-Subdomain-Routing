using System.Web;
using System.Web.Routing;
using MvcSubdomainRouting;

namespace TestWebApp.Constraints
{
    public class SubDomainConstraint : ISubDomainConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.ContainsKey(parameterName))
            {
                switch (values[parameterName].ToString().ToLower())
                {
                    case "www":
                    case "yourdomain":
                        return false;
                    default:
                        return true;
                }
            }
            return false;
        }
    }
}