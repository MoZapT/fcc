using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace FamilyControlCenter
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ////  Web-API-Konfiguration und -Dienste
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);

            ////  add filters to the application configuration
            //config.Filters.Add(new GlobalExceptionFilterAttribute());

            //  Web-API-Routen
            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //     name: "DefaultApi",
            //     routeTemplate: "{controller}/{action}/{id}",
            //     defaults: new { id = RouteParameter.Optional }
            // );
            //config.Routes.MapHttpRoute(
            //     name: "DefaultApi",
            //     routeTemplate: "api/{controller}/{action}/{id}",
            //     defaults: new { id = RouteParameter.Optional }
            // );

            ////  swagger config
            //config
            // .EnableSwagger(c =>
            // {
            //     c.SingleApiVersion("v1", "Intranet Api");
            //     c.SchemaId(x => x.FullName);
            // })
            // .EnableSwaggerUi(c =>
            // {
            //     //c.EnableOAuth2Support(
            //     //    clientId: "worsacon.api.interface",
            //     //    clientSecret: "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=",
            //     //    realm: "test-realm",
            //     //    appName: "Swagger UI"
            //     //additionalQueryStringParams: new Dictionary<string, string>() { { "foo", "bar" } }
            //     //);
            // });
        }
    }
}
