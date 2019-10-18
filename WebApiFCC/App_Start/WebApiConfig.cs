using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiFCC.Filters;

namespace WebApiFCC
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste
            config.Filters.Add(new LocalizationAttribute());

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
