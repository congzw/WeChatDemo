using System.Web.Mvc;
using CommonFx.Common.Security;
using MvcApp.Web.Areas.Auths.Models;

namespace MvcApp.Web.Areas.Auths.Controllers
{
    public class AccountController : Controller
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
    }
}
