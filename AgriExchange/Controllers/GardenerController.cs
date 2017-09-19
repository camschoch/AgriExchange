using AgriExchange.Models;
using AgriExchange.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class GardenerController : Controller
    {
        ApplicationDbContext context;
        public GardenerController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            GardenerIndexViewModel model = new GardenerIndexViewModel();
           // model.Forcast = GetForcast();
            return View();
        }

        //private List<Forcast> GetForcast()
        //{
        //    return;
        //}
    }
}