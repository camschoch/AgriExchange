using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgriExchange.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AgriExchange.Controllers
{
    public class VendorApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext context;
        public VendorApplicationsController()
        {
            context = new ApplicationDbContext();
        }

        public VendorApplicationsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            context = new ApplicationDbContext();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: VendorApplications
        public ActionResult Index(string searchString)
        {
            string searchemailadddress = searchString;
            //var students = from s in db.Students
            //select s;
            //students = students.Where(s => s.LastName.Contains(searchString)
            //                   || s.FirstMidName.Contains(searchString));

            var vendorlist = from vendor in db.VendorApplications
                select vendor;
            if (!String.IsNullOrWhiteSpace(searchemailadddress))
            {
                vendorlist = vendorlist.Where(vendor => vendor.Email.Contains(searchemailadddress));
            }

            return View(vendorlist.ToList());
        }

        public ActionResult ApproveThisVendor(int id)
        {
            VendorApplications vendorApplications = db.VendorApplications.Find(id);
            vendorApplications.IsApproved = true;
            db.Entry(vendorApplications).State = EntityState.Modified;
            db.SaveChanges();
            RegisterViewModel model = new  RegisterViewModel();
            model.Email = vendorApplications.Email;
            return View(model);
        }
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> ApproveThisVendor(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, BlockedUntil = new DateTime(1900, 1, 1) };
                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    await this.UserManager.AddToRoleAsync(user.Id, "Vendor");
                    return RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // GET: VendorApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorApplications vendorApplications = db.VendorApplications.Find(id);
            if (vendorApplications == null)
            {
                return HttpNotFound();
            }
            return View(vendorApplications);
        }

        // GET: VendorApplications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorApplications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Email,IsApproved")] VendorApplications vendorApplications)
        {
            if (ModelState.IsValid)
            {
                db.VendorApplications.Add(vendorApplications);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vendorApplications);
        }

        // GET: VendorApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorApplications vendorApplications = db.VendorApplications.Find(id);
            if (vendorApplications == null)
            {
                return HttpNotFound();
            }
            return View(vendorApplications);
        }

        // POST: VendorApplications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Email,IsApproved")] VendorApplications vendorApplications)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendorApplications).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vendorApplications);
        }

        // GET: VendorApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VendorApplications vendorApplications = db.VendorApplications.Find(id);
            if (vendorApplications == null)
            {
                return HttpNotFound();
            }
            return View(vendorApplications);
        }

        // POST: VendorApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VendorApplications vendorApplications = db.VendorApplications.Find(id);
            db.VendorApplications.Remove(vendorApplications);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
