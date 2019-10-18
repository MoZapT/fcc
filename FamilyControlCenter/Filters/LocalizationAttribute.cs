using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        private readonly string _defaultLanguage = "ru-RU";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CultureInfo culture;
            string lang = (string)filterContext.RouteData.Values["lang"] ?? _defaultLanguage;
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