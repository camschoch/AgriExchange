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
        public ActionResult Create(int ID)
        {
            BlogPost blog = (from data in context.BlogPosts where data.ID == ID select data).First();
                
            return RedirectToAction("Index", "Blog", new { id = ID});
        }
    }
}