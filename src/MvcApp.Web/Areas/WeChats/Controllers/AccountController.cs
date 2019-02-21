using System;
using System.Web.Mvc;
using CommonFx.Common;
using CommonFx.Common.Ioc;
using CommonFx.Common.Security;
using CommonFx.Common.Web.Mvc;
using MvcApp.Web.Areas.Auths.Models;

namespace MvcApp.Web.Areas.WeChats.Controllers
{
    public class AccountController : MyControllerBase
    {
        public ActionResult _FocusLogin()
        {
            return PartialView();
        }

        public ActionResult GetFocusLoginUser(string senceId)
        {
            var loginSuccess = CheckFocusLogin(senceId);
            if (loginSuccess)
            {
                var authManager = CoreServiceProvider.LocateService<IAuthManager>();
                authManager.SignOut();
                authManager.SignIn("MockUser" + DateTime.Now.ToFormat(), false);
                return MyJson("MockUser");
            }
            return MyJson<string>(null);
        }

        private static int invokeCount = 0;
        private static bool CheckFocusLogin(string senceId)
        {
            //todo wechat api logic!
            invokeCount++;
            if (invokeCount >= 5)
            {
                invokeCount = 0;
                return true;
            }
            return false;
        }
    }
}
