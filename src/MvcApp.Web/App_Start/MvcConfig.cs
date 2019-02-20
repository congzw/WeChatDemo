using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcApp.Web
{
    public partial class MvcConfig
    {
        public static void Init()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            RegisterBundles(BundleTable.Bundles);
        }
    }
}
