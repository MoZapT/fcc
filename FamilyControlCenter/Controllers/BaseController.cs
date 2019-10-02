using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
        }

        public void BeforeLoadAction()
        {
            ModelState.Clear();
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            string culture = "ru-RU";

            try
            {
                culture = Request.RawUrl.Split('/')[1];
            }
            catch (Exception) { }

            Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
    }
}