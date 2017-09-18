using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmitVendorApplication()
        {
            VendorApplications model = new VendorApplications();
            return View(model);
        }
        [HttpPost]
        public ActionResult SubmitVendorApplication(VendorApplications model)
        {
            context.VendorApplications.Add(model);
            context.SaveChanges();
            return RedirectToAction("SubmissionSuccess");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}