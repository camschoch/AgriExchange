using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgriExchange.Models;

namespace AgriExchange.Controllers
{
    public class VendorApplicationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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
            return RedirectToAction("Index");
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
