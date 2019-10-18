using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApiFCC.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly string _defaultLanguage = "ru-RU";

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            CultureInfo culture;
            string lang = (string)filterContext.ControllerContext.RouteData.Values["lang"] ?? _defaultLanguage;
            try
            {
                culture = new CultureInfo(lang);
            }
            catch (Exception /*e*/)
            {
                culture = new CultureInfo(_defaultLanguage);
                //throw new NotSupportedException(String.Format("ERROR: Invalid language code '{0}'.", lang));
            }

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}