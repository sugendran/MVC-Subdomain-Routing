using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Info = "Default controller";
            return View();
        }
    }
}
