using System.Web;
using System.Web.Optimization;

namespace Application.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);

            BundleTable.EnableOptimizations = false;
        }

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
          "~/Content/spice-shopee/css/bootstrap.min.css",
          "~/Content/spice-shopee/font-awesome/css/font-awesome.min.css",
          "~/Content/spice-shopee/css/style.css",
          "~/Content/spice-shopee/css/responsive.css",
          "~/Content/site.css",
          "~/Content/dark-theme.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/spice-shopee").Include(
                      "~/Scripts/jquery-2.1.1.min.js",
                      "~/Scripts/autocomplete/jquery-ui.min.js",
                      "~/Content/spice-shopee/js/bootstrap.min.js",
                      "~/Content/spice-shopee/js/bootstrap-hover-dropdown.min.js"));

            //<script src="Content/spice-shopee/js/jquery-1.11.1.min.js"></script>
            //<script src="Content/spice-shopee/js/jquery-migrate-1.2.1.min.js"></script>
            //<script src="Content/spice-shopee/js/bootstrap.min.js"></script>
            //<script src="Content/spice-shopee/js/bootstrap-hover-dropdown.min.js"></script>
            //<script src="Content/spice-shopee/js/jquery.magnific-popup.min.js"></script>
            //<script src="Content/spice-shopee/js/custom.js"></script>
        }
    }
}
