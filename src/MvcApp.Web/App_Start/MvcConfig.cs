using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CommonFx.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MvcApp.Web.MvcConfig), "PreStart")]
namespace MvcApp.Web
{
    public partial class MvcConfig
    {
        private static readonly IMyLogHelper LogHelper = MyLogHelper.Resolve(typeof(MvcConfig));

        public static void PreStart()
        {
            LogHelper.Log("PreStart: " + MyProjectHelper.Resolve().GetProjectPrefix());
        }

        public static void Init()
        {
            LogHelper.Log("Init");
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }
    }
}
