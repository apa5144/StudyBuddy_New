using System.Web.Optimization;

namespace StudyBuddy
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/respond").Include(
                      "~/Scripts/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/base").Include(
                     "~/Scripts/Shamcey/perfect-scrollbar/js/perfect-scrollbar.jquery.js",
                     "~/Scripts/Shamcey/jquery/jquery.js",
                     "~/Scripts/Shamcey/popper.js/popper.js",
                     "~/Scripts/Shamcey/bootstrap/bootstrap.js",
                     "~/Scripts/Shamcey/js/shamcey.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTables").Include(
                     "~/Scripts/Shamcey/datatables/jquery.dataTables.js",
                     "~/Scripts/Shamcey/datatables-responsive/dataTables.responsive.js",
                     "~/Scripts/Shamcey/select2/js/select2.min.js"));

            bundles.Add(new StyleBundle("~/Content/base").Include(
                     "~/Content/Shamcey/perfect-scrollbar/css/perfect-scrollbar.css",
                     "~/Content/Shamcey/font-awesome/css/font-awesome.css",
                     "~/Content/Shamcey/Ionicons/css/ionicons.css",
                     "~/Content/Shamcey/css/shamcey.css"));


            bundles.Add(new StyleBundle("~/Content/DataTables").Include(
                     "~/Content/Shamcey/datatables/jquery.dataTables.css",
                     "~/Content/Shamcey/select2/css/select2.min.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                     "~/Content/Site.css"));
        }
    }
}
