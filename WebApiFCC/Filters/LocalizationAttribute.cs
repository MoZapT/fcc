using System;
using System.Globalization;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiFCC.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _defaultLanguage = "ru-RU";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            CultureInfo culture;
            string lang = (string)actionContext.ControllerContext.RouteData.Values["lang"] ?? _defaultLanguage;
            try
            {
                culture = new CultureInfo(lang);
            }
            catch (Exception)
            {
                culture = new CultureInfo(_defaultLanguage);
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}