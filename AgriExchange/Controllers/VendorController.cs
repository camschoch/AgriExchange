using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
<<<<<<< HEAD
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


=======
        } 
>>>>>>> f4d6901ae89ffaaca6dcac18c2dc340276ffe5e5
    }
}