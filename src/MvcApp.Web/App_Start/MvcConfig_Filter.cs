using System.Web.Mvc;

namespace MvcApp.Web
{
    public partial class MvcConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
