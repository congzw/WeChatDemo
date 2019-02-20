using System.Web.Mvc;
using MvcApp.Web.Areas.Demos.Models;

namespace MvcApp.Web.Areas.Demos.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFooAppService _fooAppService;

        public HomeController(IFooAppService fooAppService)
        {
            _fooAppService = fooAppService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = _fooAppService.SayHi();
            return View();
        }
    }
}
