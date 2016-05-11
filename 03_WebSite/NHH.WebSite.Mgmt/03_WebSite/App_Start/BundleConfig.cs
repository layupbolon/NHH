using System.Web;
using System.Web.Optimization;

namespace NHH.WebSite.Management
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/css/bootstrap.min.css",
                      "~/Content/assets/css/font-awesome.min.css",
                      "~/Content/assets/css/jquery-ui.min.css",
                      "~/Content/assets/css/chosen.css",
                      "~/Content/assets/css/datepicker.css",
                      "~/Content/assets/css/colorbox.css",
                      "~/Content/assets/css/ace.min.css",
                      "~/Content/assets/css/global.css",
                      "~/Content/assets/css/chart.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/JsonNet.js",
                        "~/Scripts/json2.js",
                        "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/assets/js/ace-extra.min.js",
                      "~/Content/assets/js/bootstrap.min.js",
                      "~/Scripts/jquery-ui.min.js",
                      "~/Scripts/jquery.ui.touch-punch.min.js",
                      "~/Content/assets/js/fuelux/fuelux.spinner.min.js",
                      "~/Content/assets/js/date-time/bootstrap-datepicker.min.js",
                      "~/Content/assets/js/date-time/bootstrap-timepicker.min.js",
                      "~/Content/assets/js/date-time/moment.min.js",
                      "~/Content/assets/js/date-time/daterangepicker.min.js",
                      "~/Scripts/jquery.knob.min.js",
                      "~/Scripts/jquery.autosize.min.js",
                      "~/Scripts/jquery.inputlimiter.1.3.1.min.js",
                      "~/Scripts/jquery.maskedinput.min.js",
                      "~/Scripts/jquery.colorbox-min.js",
                      "~/Scripts/chosen.jquery.min.js",
                      "~/Scripts/typeahead.jquery.min.js",
                      "~/Content/assets/js/bootstrap-tag.min.js",
                      "~/Content/assets/js/ace-elements.min.js",
                      "~/Content/assets/js/ace.min.js"));

        }
    }
}
