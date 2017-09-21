using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using AgriExchange.Models.ViewModels;
using Microsoft.AspNet.Identity;

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
            if (User.IsInRole("Admin"))
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
        
        public ActionResult ApproveVendor(int id)
        {
          var Application =  (from data in context.VendorApplications where data.ID == id select data).First();
              Application.IsApproved = true;
            return RedirectToAction("CreateUser", new {email = Application.Email});

        }

        //I need to possibly reroute user to registration page that allows a drop down menu to be selected as a gardener/vendor/admin
        //public ActionResult CreateUser(string email)
        //{
        //    ApplicationUser model = new ApplicationUser();
        //    model.Email = email;
        //    return View(model);
            
    }
}