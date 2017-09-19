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

        public ActionResult WeatherApiCall()
        {
            string hourlyTemp = "daily-high-temperature";
            string location = "/43.029494/-87.904047";
            ApiCalls.WeatherApi(hourlyTemp, location);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult FruitApiCall()
        {
            string searchName = "apple";
            string typeSearch = "search=";
            ApiCalls.FruitApi(searchName, typeSearch);
            return View();
        }
    }
}