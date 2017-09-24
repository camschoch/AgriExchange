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
        public ActionResult Like(int id)
        {
            CommentLikes like = new CommentLikes();
            like.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
            like.Comment = (from data in context.Comments where data.ID == id select data).First();
            var likes = (from data in context.CommentLikes where data.Comment.ID == like.Comment.ID && data.User.Id == like.User.Id select data).ToList();
            if (likes.Count > 0)
            {
                context.CommentLikes.Remove(likes[0]);
                context.SaveChanges();
            }
            else
            {
                context.CommentLikes.Add(like);
                context.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}