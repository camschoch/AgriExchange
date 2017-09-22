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
    public class ReportController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Report/ReportIndex
        public ActionResult ReportIndex()
        {
            return View(db.Reports.ToList());
        }

        /*
        // GET: Report/ReportDetails/5
        public ActionResult ReportDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }
        */

        // GET: Report/ReportCreate
        public ActionResult ReportCreate()
        {
            return View();
        }

        // POST: Report/ReportCreate
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportCreate([Bind(Include = "ReportedBlogPost,ReportedComment,ReportingUser,ID,Reason")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Reports.Add(report);
                db.SaveChanges();
                DisplaySuccessMessage("Has append a Report record");
                return RedirectToAction("ReportIndex");
            }

            DisplayErrorMessage();
            return View(report);
        }

        // GET: Report/ReportEdit/5
        public ActionResult ReportEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }


        // POST: ReportReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportEdit([Bind(Include = "ReportedBlogPost,ReportedComment,ReportingUser,ID,Reason")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Has update a Report record");
                return RedirectToAction("ReportIndex");
            }
            DisplayErrorMessage();
            return View(report);
        }
        public ActionResult BlockUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BlockUser([Bind(Include = "ReportedBlogPost,ReportedComment,ReportingUser,ID,Reason")] Report report)
        {
            if (ModelState.IsValid)
            {
                db.Entry(report.ReportedBlogPost.User).State = EntityState.Modified;
                db.SaveChanges();
                DisplaySuccessMessage("Blocked user record from posted until " +
                                      report.ReportedBlogPost.User.BlockedUntil);
                return RedirectToAction("ReportIndex");
            }
            DisplayErrorMessage();
            return View(report);
        }

        // GET: Report/ReportDelete/5
        public ActionResult ReportDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Report report = db.Reports.Find(id);
            if (report == null)
            {
                return HttpNotFound();
            }
            return View(report);
        }

        // POST: Report/ReportDelete/5
        [HttpPost, ActionName("ReportDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ReportDeleteConfirmed(int id)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Report record");
            return RedirectToAction("ReportIndex");
        }

        // POST: Report/ReportDelete/5
        [HttpPost, ActionName("PostDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ReportDeleteConfirmed(int id, int postId)
        {
            Report report = db.Reports.Find(id);
            db.Reports.Remove(report);
            BlogPost post = db.BlogPosts.Find(postId);
            db.BlogPosts.Remove(post);
            db.SaveChanges();
            DisplaySuccessMessage("Has delete a Report record");
            return RedirectToAction("ReportIndex");
        }

        private void DisplaySuccessMessage(string msgText)
        {
            TempData["SuccessMessage"] = msgText;
        }

        private void DisplayErrorMessage()
        {
            TempData["ErrorMessage"] = "Save changes was unsuccessful.";
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
