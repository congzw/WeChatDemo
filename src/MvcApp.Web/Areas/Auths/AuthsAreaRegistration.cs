using System.Web.Mvc;
using CommonFx.Common.Web.Mvc;

namespace MvcApp.Web.Areas.Auths
{
    public class AuthsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Auths";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.AutoRegisterAreaRoutes(context);
        }
    }
}