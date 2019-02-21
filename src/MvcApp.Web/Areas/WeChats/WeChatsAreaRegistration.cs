using System.Web.Mvc;
using CommonFx.Common.Web.Mvc;

namespace MvcApp.Web.Areas.WeChats
{
    public class WeChatsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "WeChats";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            this.AutoRegisterAreaRoutes(context);
        }
    }
}