using System.Web.Mvc;
using System.Web.Routing;

namespace FamilyControlCenter
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
               name: "LocalizedDefault",
               url: "{lang}/{controller}/{action}",
               defaults: new { controller = "Home", action = "Index" },
               //constraints: new { lang = "es-ES|fr-FR|en-US" }
               constraints: new { lang = @"(\w{2})|(\w{2}-\w{2})" }   // en or en-US
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", lang = "ru-RU" }
            );
        }
    }
}
