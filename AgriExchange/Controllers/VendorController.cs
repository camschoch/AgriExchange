using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using AgriExchange.Models.ViewModels;

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
            AdminVendorViewModel model;
            if (User.IsInRole("Adminvendor"))
            {
                string roleId = (from data in context.Roles where data.Name == "Vendor" select data.Id).First();
                model = new AdminVendorViewModel();

                model.Vendors = context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(roleId)).ToList();
                model.Applications = context.VendorApplications.ToList();
            }
            else
            {
                
                 model = new AdminVendorViewModel();
            }
           

            return View(model);


        }



    }
}