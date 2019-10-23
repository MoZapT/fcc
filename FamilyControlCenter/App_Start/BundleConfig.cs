using System.Web;
using System.Web.Optimization;

namespace FamilyControlCenter
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                        "~/Scripts/Base/Base.js",
                        "~/Scripts/Base/CustomizedTypeahead.js",
                        "~/Scripts/Base/DatePicker.js",
                        "~/Scripts/typeahead.bundle.js",
                        "~/Scripts/typeahead.jquery.js",
                        "~/Scripts/bloodhound.js",
                        "~/Scripts/bootstrap-datepicker.js",
                        "~/Scripts/bootstrap-datepicker.min.js",
                        "~/Scripts/locales/bootstrap-datepicker.de.min.js",
                        "~/Scripts/locales/bootstrap-datepicker.ru.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/fontawesome/fontawesome.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/typeahead.css"));

            bundles.Add(new StyleBundle("~/Content/fontawesome").Include(
                      "~/Content/webfonts/fontawesome.css",
                      "~/Content/webfonts/brands.css",
                      "~/Content/webfonts/solid.css"
                      ));
        }
    }
}
