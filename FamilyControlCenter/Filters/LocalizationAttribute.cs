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
            string lang = (string)filterContext.RouteData.Values["lang"];
            if (string.IsNullOrWhiteSpace(lang))
            {
                return;
            }

            CultureInfo culture;
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