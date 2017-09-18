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
            CurlRequest.Curl();
            return View();
        }

        public ActionResult HttpCall()
        {
            string searchName = "Bora%20Berry";
            string typeSearch = "tfvitem=";
            CurlRequest.Http(searchName, typeSearch);
            return View();
        }
    }
}