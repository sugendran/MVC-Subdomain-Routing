using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSubdomainRouting
{
    public static class RouteCollectionExtensions
    {
        public static Route MapSubDomainRoute(this RouteCollection routes, string name, string subDomain, string url)
        {
            return MapSubDomainRoute(routes, name, subDomain, url, null /* defaults */, (object)null /* constraints */);
        }

        public static Route MapSubDomainRoute(this RouteCollection routes, string name, string subDomain, string url, object defaults)
        {
            return MapSubDomainRoute(routes, name, subDomain, url, defaults, (object)null /* constraints */);
        }

        public static Route MapSubDomainRoute(this RouteCollection routes, string name, string subDomain, string url, object defaults, object constraints)
        {
            return MapSubDomainRoute(routes, name, subDomain, url, defaults, constraints, null /* namespaces */);
        }

        public static Route MapSubDomainRoute(this RouteCollection routes, string name, string subDomain, string url, string[] namespaces)
        {
            return MapSubDomainRoute(routes, name, subDomain, url, null /* defaults */, null /* constraints */, namespaces);
        }

        public static Route MapSubDomainRoute(this RouteCollection routes, string name, string subDomain, string url, object defaults, string[] namespaces)
        {
            return MapSubDomainRoute(routes, name, subDomain, url, defaults, null /* constraints */, namespaces);
        }

        public static Route MapSubDomainRoute(this RouteCollection routes, string name, string subDomain, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (subDomain == null)
            {
                throw new ArgumentNullException("subDomain");
            }

            Route route = new SubDomainRoute(subDomain, url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens = new RouteValueDictionary();
                route.DataTokens["Namespaces"] = namespaces;
            }

            routes.Add(name, route);

            return route;
        }
    }
}
