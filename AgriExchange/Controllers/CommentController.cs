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
            comment.PostDate = DateTime.Now;
            context.Comments.Add(comment);
            context.SaveChanges();
            return RedirectToAction("Index", "Blog");
        }
    }
}