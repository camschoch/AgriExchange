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
            if (this.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (this.User.IsInRole("Gardener"))
            {
                return RedirectToAction("Index", "Gardener");
            }
            else if (this.User.IsInRole("Vendor"))
            {
                return RedirectToAction("Index", "Vendor");
            }
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
            model.IsApproved = false;
            var currentApplications = (from data in context.VendorApplications where data.Email.ToLower() == model.Email.ToLower() select data).ToList();
            if (currentApplications.Count == 0)
            {
                context.VendorApplications.Add(model);
                context.SaveChanges();
                return RedirectToAction("SubmissionSuccess");
            }
            else
            {
                return RedirectToAction("SubmissionFailed");
            }
        }
        public ActionResult SubmissionSuccess()
        {
            return View();
        }
        public ActionResult SubmissionFailed()
        {
            return View();
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