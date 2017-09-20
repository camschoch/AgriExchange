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
            SetBlogTags(model);
            return RedirectToAction("index");
        }

        private void SetBlogTags(BlogPost model)
        {
            string[] tags = model.Tags.Replace(", ", "-").Replace(",", "-").Split('-');
            foreach(string tag in tags)
            {
                GetBlogTag(model, tag);
            }
            

        }

        private void GetBlogTag(BlogPost model, string tag)
        {
            BlogTags blogTag = new BlogTags();
            blogTag.Blog = (from data in context.BlogPosts.Include("User") where data.Title == model.Title && data.User == model.User && data.DatePosted == model.DatePosted select data).First();
            blogTag.Tag = GetTag(tag);
            List<BlogTags> tags = (from data in context.BlogTags.Include("Blog").Include("Tag") where data.Blog.ID == blogTag.Blog.ID && data.Tag.ID == blogTag.Tag.ID select data).ToList();
            if(tags.Count > 0)
            {
                return;
            }
            else
            {
                context.BlogTags.Add(blogTag);
                context.SaveChanges();
                return;

            }
        }

        private Tags GetTag(string tag)
        {
            Tags tagEntry = new Tags();
            tagEntry.Tag = tag;
            List<Tags> tags = (from data in context.Tags where data.Tag == tagEntry.Tag select data).ToList();
            if(tags.Count > 0)
            {
                return tags[0];
            }
            else
            {
                context.Tags.Add(tagEntry);
                context.SaveChanges();
                return (from data in context.Tags where data.Tag == tagEntry.Tag select data).First();
            }
        }
    }
}