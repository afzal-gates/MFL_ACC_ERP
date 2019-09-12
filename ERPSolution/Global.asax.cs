using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ERP.Core;

namespace ERPSolution
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
          
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.Email;

            GlobalConfiguration.Configuration.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.MediaTypeMappings.Add(
            new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));
        }

        protected void Application_PostAuthorizeRequest()
        {
            System.Web.HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }
    }
}
