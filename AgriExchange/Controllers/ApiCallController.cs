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
            string userName = User.Identity.Name;
            int addressId = 0;
            var user = (from data in context.Users where data.UserName == userName select data).First();            
            var allAddresses = (from data in context.UserAddresses where data.User.Id == user.Id select data);
            foreach (var item in allAddresses)
            {
                addressId = item.ID;
            }
            var addressObject = (from data in context.Addresses.Include("City").Include("City.State") where data.ID == addressId select data).First();
            string realAddress = addressObject.addressLine.ToLower();
            var state = addressObject.City.State.abbreviation;
            string city = addressObject.City.City.Replace(" ", "%20");
            string splitAddress = realAddress.Replace(".", string.Empty).Replace(" ", "%20");
            var searchString = splitAddress + "%2C%20" + city + "%2C%20" + state;
            ApiCalls.GeoLocationApi(searchString);
            return RedirectToAction("Index", "Home");
        }
    }
}