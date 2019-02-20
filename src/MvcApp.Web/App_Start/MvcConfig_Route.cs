using System.Web.Mvc;
using System.Web.Routing;

namespace MvcApp.Web
{
    public partial class MvcConfig
    {
        private static void RegisterRoutes(RouteCollection routes)
        {
            var projectPrefix = GetProjectPrefix();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default_Root",
                url: "",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { projectPrefix + ".Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default_Common",
                url: "Common/{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" },
                namespaces: new[] { projectPrefix + ".Web.Controllers" }
            );
        }

        private static string GetProjectPrefix()
        {
            return "MvcApp";
        }
    }
}
