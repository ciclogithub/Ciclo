using System.Web;
using System.Web.Optimization;

namespace Ciclo
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

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/site").Include(
                      "~/Scripts/js/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/painel").Include(
                    "~/Scripts/plugins/validate/js/jquery.validationEngine-pt_BR.js",
                    "~/Scripts/plugins/validate/js/jquery.validationEngine.js",
                    "~/Scripts/js/painel.js"));

            bundles.Add(new ScriptBundle("~/bundles/instrutores").Include(
                      "~/Scripts/js/instrutores.js"));

            bundles.Add(new ScriptBundle("~/bundles/template").Include(
                "~/Scripts/customScrollbar.min.js",
                "~/Scripts/packery.pkgd.min.js",
                "~/Scripts/query.hoverdir.js",
                "~/Scripts/jquery.vide.min.js",
                "~/Scripts/jquery.fullpage.js",
                "~/Scripts/isotope.pkgd.js",
                "~/Scripts/main.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/template").Include(
                    "~/Content/normalize.css",
                    "~/Content/font-awesome.min.css",
                    "~/Content/icomoon.css",
                    "~/Content/customScrollbar.css",
                    "~/Content/animate.css",
                    "~/Content/jquery.fullpage.css",
                    "~/Content/transitions.css",
                    "~/Content/main.css",
                    "~/Content/color.css",
                    "~/Scripts/plugins/validate/css/validationEngine.jquery.css",
                    "~/Content/responsive.css"));
        }
    }
}
