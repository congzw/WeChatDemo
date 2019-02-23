using System.Web.Mvc;
using CommonFx.Common.Web.Mvc;

namespace MvcApp.Web.Areas.WeChats.Controllers
{
    public class OAuthController : MyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        //?code=CODE&state=STATE
        public ActionResult Callback(string code, string state)
        {
            return View("LoginResult");
        }
    }
}
