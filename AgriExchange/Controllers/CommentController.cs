using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class CommentController : Controller
    {
        ApplicationDbContext context;
        public CommentController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Comment
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            comment.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
            comment.Blog = (from data in context.BlogPosts where data.ID == comment.Blog.ID select data).First();
            comment.PostDate = DateTime.Now;
            context.Comments.Add(comment);
            context.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
        public ActionResult Report(int ID)
        {
            Report report = new Report();
            report.ReportedComment = (from data in context.Comments where data.ID == ID select data).First();
            return View(report);
        }
        [HttpPost]
        public ActionResult Report(Report report)
        {
            report.ReportingUser = StaticClasses.UserRetriever.RetrieveUser(User, context);
            report.ReportedComment = (from data in context.Comments where data.ID == report.ReportedComment.ID select data).First();
            context.Reports.Add(report);
            context.SaveChanges();
            return RedirectToAction("SuccessfulReport");
        }
        public ActionResult SuccessfulReport()
        {
            return View();
        }
    }
}