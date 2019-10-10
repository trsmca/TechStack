using System.Web;
using System.Web.Optimization;

namespace Stack
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
                       "~/Scripts/jquery.validate.unobtrusive*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"
                      , "~/Scripts/respond.js"
                      , "~/Scripts/bootstrap.file-input.js"
                      ));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/megamenu.js",
                      "~/Scripts/jquery.easing.1.3.js",
                      "~/Scripts/jquery.waypoints.min.js",
                      "~/Scripts/jquery.stellar.min.js",
                      "~/Scripts/owl.carousel.min.js",
                      "~/Scripts/jquery.simplePagination.js",
                      "~/Scripts/main.js",
                      "~/Scripts/solid-menu.js",
                      "~/Scripts/notifications.js",
                      "~/Scripts/jquery-comments.min.js",
                       "~/Scripts/custombox.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css",
                      "~/Content/font-awesome.css",
                      "~/Content/animate.css",
                      "~/Content/icomoon.css",
                      "~/Content/simple-line-icons.css",
                      "~/Content/owl.carousel.min.css",
                      "~/Content/owl.theme.default.min.css",
                      "~/Content/style-new.css",
                      "~/Content/sky-forms.css",
                      "~/Content/simplePagination.css",
                      "~/Content/navigation.css",
                      "~/Content/solid-menu.css",
                      "~/Content/lobibox.css",
                      "~/Content/jquery-comments.css",
                      "~/Content/loader.css",
                      "~/Content/custombox.css",
                      "~/Content/mn/main.css",
                      "~/Content/mn/util.css"));
        }
    }
}
