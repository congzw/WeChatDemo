using System.Web.Mvc;
using CommonFx.Common.Web.Mvc;

namespace MvcApp.Web.Areas.WeChats.Controllers
{
    public class DemoController : MyControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowScan()
        {
            return View();
        }

        public ActionResult Callback(string code, string state)
        {
            //redirect_uri?code=CODE&state=STATE
            ViewBag.Message = string.Format("redirect_uri?code={0}&state={1}", code, state);
            return View();
        }
    }
}