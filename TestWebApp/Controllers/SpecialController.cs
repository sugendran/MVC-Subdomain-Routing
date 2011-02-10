using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class SpecialController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Info = "The special controller";
            return View();
        }

    }
}
