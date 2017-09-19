using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class VendorController : Controller

    {
        private ApplicationDbContext context;
        public VendorController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Vendor
        public ActionResult Index()
        {
            return View();

        }
        public ActionResult SubmitApplication()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SubmitApplication(VendorApplications model)
        {
            model.IsApproved = false;
            context.VendorApplications.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewVendor()
        {
            return View();
        }



    }
}