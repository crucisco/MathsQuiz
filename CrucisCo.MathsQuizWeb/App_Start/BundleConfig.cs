using System.Web;
using System.Web.Optimization;

namespace CrucisCo.MathsQuizWeb
{
    public class BundleConfig
    {
            public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(            
                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/angular.min.js",
                        "~/Scripts/angular-route.min.js",
                        "~/Scripts/angular-animate.min.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Scripts/angular-ui/ui-slider.js",
                        "~/Scripts/Chart.min.js",
                        "~/Scripts/angular-chart.min.js",
   
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom")
                .Include("~/Scripts/app/ng.MathsQuiz.js")
                .IncludeDirectory("~/Scripts/app/filters", "*.js", false)
                .IncludeDirectory("~/Scripts/app/models", "*.js", false));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css",
                      "~/Content/ui-bootstrap-csp.css",
                      "~/Content/angular-chart.min.css",
                      "~/Content/ui-slider.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                    //"~/Content/themes/base/base.css",  (this causes non-minified double include)
                    "~/Content/themes/base/core.css",
                    "~/Content/themes/base/datepicker.css",
                    "~/Content/themes/base/theme.css",
                    "~/Content/themes/base/slider.css"
                ));
        }
    }
}
