using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class BlogController : Controller
    {
        ApplicationDbContext context;
        public BlogController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Create(BlogPost model)
        {
            model.User = StaticClasses.UserRetriever.RetrieveUser(User);
            model.DatePosted = DateTime.Now;
            context.BlogPosts.Add(model);
            context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}