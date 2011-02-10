using System;
using System.Web;
using System.Web.Routing;

namespace MvcSubdomainRouting
{
    public class SubDomainRoute : Route
    {
        private readonly string _subDomain;
        private readonly bool _isVariableSubdomain;

        public SubDomainRoute(string subDomain, string url, IRouteHandler routeHandler)
            : this(subDomain, url, null, null, null, routeHandler)
        {

        }

        public SubDomainRoute(string subDomain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : this(subDomain, url, defaults, null, null, routeHandler)
        {

        }

        public SubDomainRoute(string subDomain, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler)
            : this(subDomain, url, defaults, constraints, null, routeHandler)
        {

        }

        public SubDomainRoute(string subDomain, string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler)
            : base(url, defaults, constraints, dataTokens, routeHandler)
        {
            if (string.IsNullOrWhiteSpace(subDomain))
            {
                throw new ArgumentNullException("subDomain");
            }
            if (subDomain[0] == '{')
            {
                _isVariableSubdomain = true;
                _subDomain = subDomain.Substring(1, subDomain.Length - 2);
            }
            else
            {
                _isVariableSubdomain = false;
                _subDomain = subDomain.ToLower();
            }
        }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (!_isVariableSubdomain)
            {
                var uri = httpContext.Request.Url;
                if (uri == null)
                {
                    return null;
                }

                var url = uri.Host;
                var index = url.IndexOf(".");

                if (index < 0)
                {
                    return null;
                }
                var possibleSubDomain = url.Substring(0, index).ToLower();

                if (possibleSubDomain == _subDomain)
                {
                    var result = base.GetRouteData(httpContext);
                    return result;
                }
                return null;
            }
            return base.GetRouteData(httpContext);
        }

        protected override bool ProcessConstraint(HttpContextBase httpContext, object constraint, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (parameterName == _subDomain && constraint is ISubDomainConstraint)
            {
                var uri = httpContext.Request.Url;
                if (uri != null)
                {
                    var url = uri.Host;
                    var index = url.IndexOf(".");
                    if (index > 0)
                    {
                        var possibleSubDomain = url.Substring(0, index).ToLower();
                        values.Add(_subDomain, possibleSubDomain);
                        return ((ISubDomainConstraint)constraint).Match(httpContext, this, parameterName, values, routeDirection);
                    }
                }
            }
            return base.ProcessConstraint(httpContext, constraint, parameterName, values, routeDirection);
        }
    }

}