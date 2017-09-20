using AgriExchange.Models;
using AgriExchange.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class ApiCallController : Controller
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
            return RedirectToAction("Index", "Home");
        }
        public ActionResult GeoLocationApi()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userName = User.Identity.Name;
            var user = (from data in context.Users where data.UserName == userName select data).First();
            var addresses = (from data in context.UserAddresses where data.ID.ToString() == user.Id select data).First();
            var realAddress = addresses.Address;
            string addressString = "1600%20Amphitheatre%20Parkway%2C%20Mountain%20View%2C%20CA";
            ApiCalls.GeoLocationApi(addressString);
            return RedirectToAction("Index", "Home");
        }
    }
}