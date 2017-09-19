using AgriExchange.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CurlCall()
        {
            string hourlyTemp = "daily-high-temperature";
            string location = "/43.029494/-87.904047";
            CurlRequest.Curl(hourlyTemp, location);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult HttpCall()
        {
            string searchName = "apple";
            string typeSearch = "search=";
            CurlRequest.Http(searchName, typeSearch);
            return View();
        }
    }
}