using System.Web.Mvc;
using CommonFx.Common.Web.Mvc;

namespace MvcApp.Web.Areas.Demos
{
    public class DemosAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Demos";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.AutoRegisterAreaRoutes(context);
        }
    }
}