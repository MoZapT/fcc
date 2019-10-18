using System.Web;
using System.Web.Mvc;
using WebApiFCC.Filters;

namespace WebApiFCC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
