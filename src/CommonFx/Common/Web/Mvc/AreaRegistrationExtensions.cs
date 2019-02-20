using System.Web.Mvc;

namespace CommonFx.Common.Web.Mvc
{
    public static class AreaRegistrationExtensions
    {
        public static void AutoRegisterAreaRoutes(this AreaRegistration areaRegistration, AreaRegistrationContext context)
        {
            var myProjectHelper = MyProjectHelper.Resolve();
            var defaultProjectPrefix = myProjectHelper.GetProjectPrefix();

            //context.MapRoute(
            //    name: areaRegistration.AreaName + "_default",
            //    url: "{site}/" + areaRegistration.AreaName + "/{controller}/{action}",
            //    defaults: new { area = areaRegistration.AreaName },
            //    namespaces: new[] { string.Format("{0}.Web.Areas.{1}.Controllers", defaultProjectPrefix, areaRegistration.AreaName) }
            //    );         

            context.MapRoute(
                name: areaRegistration.AreaName + "_default",
                url: areaRegistration.AreaName + "/{controller}/{action}",
                defaults: new { area = areaRegistration.AreaName },
                namespaces: new[] { string.Format("{0}.Web.Areas.{1}.Controllers", defaultProjectPrefix, areaRegistration.AreaName) }
                );
        }
    }
}
