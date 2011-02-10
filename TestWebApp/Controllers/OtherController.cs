using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestWebApp.Controllers
{
    public class OtherController : Controller
    {
        public ActionResult Index(string someValue)
        {
            ViewBag.Info = string.Concat("Subdomain is ", someValue);
            return View();
        }

    }
}
