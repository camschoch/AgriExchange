using AgriExchange.Models;
using AgriExchange.Models.ViewModels;
using AgriExchange.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class MapController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Map
        public ActionResult Index()
        {
            MapViewModel model = new MapViewModel();
            string userName = User.Identity.Name;
            model.results = ApiCalls.FarmersMarketApi(userName, context);

            var vendorId = context.Roles.First(r => r.Name == "Vendor").Id;
            var vendor = context.Users.Where(u => u.Roles.Any(r => r.RoleId == vendorId)).ToList();
            model.Vendor = vendor;
            return View(model);
        }

        public ActionResult ShowMarkerMarkets(float lat, float lng)
        {
            MapViewModel model = new MapViewModel();
            string userName = User.Identity.Name;
            model.results = ApiCalls.FarmersMarketApi(userName, context);
            var vendorId = context.Roles.First(r => r.Name == "Vendor").Id;
            var vendor = context.Users.Where(u => u.Roles.Any(r => r.RoleId == vendorId)).ToList();
            model.Vendor = vendor;
            model.lat = lat;
            model.lng = lng;
            return View("Index", model);
        }
        public ActionResult ShowMarkerVendor(string userId)
        {
            MapViewModel model = new MapViewModel();
            string userName = User.Identity.Name;
            model.results = ApiCalls.FarmersMarketApi(userName, context);
            var vendorRoleId = context.Roles.First(r => r.Name == "Vendor").Id;
            var vendorList = context.Users.Where(u => u.Roles.Any(r => r.RoleId == vendorRoleId)).ToList();
            model.Vendor = vendorList;
            var vendor = vendorList.Where(a => a.Id == userId);
            //model.lat = lat;
            //model.lng = lng;
            return View("Index", model);
        }
    }
}