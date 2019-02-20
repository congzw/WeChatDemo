using System.Web.Mvc;
using System.Web.Optimization;

namespace MvcApp.Web
{
    public partial class MvcConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //https://docs.microsoft.com/en-us/aspnet/mvc/overview/performance/bundling-and-minification


            //hack for relative url problems! only bind same folder
            bundles.Add(new StyleBundle("~/Content/libs/basic/bootstrap/css/bootstrap").Include(
                      "~/Content/libs/basic/bootstrap/css/bootstrap.css",
                      "~/Content/libs/basic/bootstrap/css/bootstrap-theme.css"));

            bundles.Add(new StyleBundle("~/Content/css/layout").Include(
                      "~/Content/css/layout.css"));

            //todo by config
            BundleTable.EnableOptimizations = false;
        }
    }
}

namespace MvcApp.Web.Controllers
{
    public class BundleController : Controller
    {
        public ActionResult SetBundle(bool enabled = true)
        {
            BundleTable.EnableOptimizations = enabled;
            return Content("Bundle EnableOptimizations: " + enabled);
        }
    }
}