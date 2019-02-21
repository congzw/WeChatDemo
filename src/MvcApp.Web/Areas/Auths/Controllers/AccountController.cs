using System;
using System.Web.Mvc;
using CommonFx.Common;
using CommonFx.Common.Ioc;
using CommonFx.Common.Security;
using CommonFx.Common.Web.Mvc;
using MvcApp.Web.Areas.Auths.Models;

namespace MvcApp.Web.Areas.Auths.Controllers
{
    public class AccountController : MyControllerBase
    {
        private readonly IAuthManager _authManager;

        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var result = SignInManager.PasswordSignIn(model.UserName, model.Password, false, false);
            //    if (result == SignInStatus.Success)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        ModelState.AddModelError("", "The user name or password provided is incorrect.");
            //    }
            //}
            //return View(model);

            if (ModelState.IsValid)
            {
                if (model.UserName == "admin")
                {
                    _authManager.SignIn(model.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            _authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

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
