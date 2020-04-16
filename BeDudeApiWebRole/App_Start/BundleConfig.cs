using System.Web;
using System.Web.Optimization;

namespace BeDudeApiWebRole
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/2.10201f46.chunk.js",
                      "~/Scripts/main.e7be4b93.chunk.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/2.c6095b75.chunk.css",
                      "~/Content/main.9013e065.chunk.css"));

        }
    }
}
