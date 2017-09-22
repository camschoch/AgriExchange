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
        // GET: Map
        public ActionResult Index()
        {
            string userName = User.Identity.Name;
            var item = ApiCalls.FarmersMarketApi(userName);
            return View(item);
        }

        public ActionResult Map()
        {
            
            return View();
        }
    }
}