using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Http;

namespace FamilyControlCenter.Controllers
{
    public class BaseApiController : ApiController
    {
        public BaseApiController()
        {
        }

        public void ChangeLanguage()
        {
            string culture = "ru-RU";

            try
            {
                culture = Request.RequestUri.LocalPath.Split('/')[1];
            }
            catch (Exception) { }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
    }
}