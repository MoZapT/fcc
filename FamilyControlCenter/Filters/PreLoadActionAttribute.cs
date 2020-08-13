using FamilyControlCenter.Controllers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter.Filters
{
    public class PreLoadActionAttribute : ActionFilterAttribute
    {
        private readonly string _defaultLanguage = "ru-RU";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = (Controller)filterContext.Controller;
            controller.ModelState.Clear();
        }
    }
}