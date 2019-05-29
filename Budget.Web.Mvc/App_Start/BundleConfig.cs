using System.Web.Optimization;

namespace Budget.Web.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*========================= CSS ======================*/

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                      "~/Content/bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/bgrid").Include(
                      "~/Content/bootstrap-grid.css"));

            bundles.Add(new StyleBundle("~/Content/breboot").Include(
                      "~/Content/bootstrap-reboot.css"));

            /*========================= JS ======================*/
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bselect").Include(
                     "~/Scripts/bootstrap-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepicker").Include(
                    "~/Scripts/bootstrap-datepicker.js",
                    "~/Scripts/locales/bootstrap-datepicker.ru.js"));
        }
    }
}
