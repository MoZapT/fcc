using FamilyControlCenter.Filters;
using System.Web;
using System.Web.Mvc;

namespace FamilyControlCenter
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LocalizationAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
