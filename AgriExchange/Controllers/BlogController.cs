using AgriExchange.Models;
using AgriExchange.Models.ViewModels;
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
        public ActionResult Index(int? id)
        {
            ApplicationUser user = StaticClasses.UserRetriever.RetrieveUser(User, context);
            BlogViewModel model = new BlogViewModel();
            if (id != null)
            { 
                model.User = user;
                model.Blogs = (from data in context.BlogPosts.Include("User") where (data.ID == id) select data).ToList();
                model.Comments = GetComments(model.Blogs);
                model.BlogLikes = GetBlogLikes(model.Blogs);
                model.CommentLikes = GetCommentLikes(model.Comments);
                model.Tags = GetBlogTags(model.Blogs);
                return View(model);
            }

            
            model.User = user;
            model.Blogs = (from data in context.BlogPosts.Include("User") where data.User.Id == user.Id select data).ToList();
            model.Comments = GetComments(model.Blogs);
            model.BlogLikes = GetBlogLikes(model.Blogs);
            model.CommentLikes = GetCommentLikes(model.Comments);
            model.Tags = GetBlogTags(model.Blogs);
            return View(model);
        }

        //public ActionResult Index(int id)
        //{
        //    ApplicationUser user = StaticClasses.UserRetriever.RetrieveUser(User, context);
        //    BlogViewModel model = new BlogViewModel();
        //    model.User = user;
        //    model.Blogs = (from data in context.BlogPosts.Include("User") where (data.User.Id == user.Id && data.ID == id) select data).ToList();
        //    model.Comments = GetComments(model.Blogs);
        //    model.BlogLikes = GetBlogLikes(model.Blogs);
        //    model.CommentLikes = GetCommentLikes(model.Comments);
        //    model.Tags = GetBlogTags(model.Blogs);
        //    return View(model);
        //}


        private List<BlogTags> GetBlogTags(List<BlogPost> blogs)
        {
            List<BlogTags> tags = new List<BlogTags>();
            foreach (BlogPost blog in blogs)
            {
                tags.AddRange((from data in context.BlogTags.Include("Blog").Include("Tag") where data.Blog.ID == blog.ID select data).ToList());
            }
            return tags;
        }

        private List<CommentLikes> GetCommentLikes(List<Comment> comments)
        {
            List<CommentLikes> likes = new List<CommentLikes>();
            foreach (Comment comment in comments)
            {
                likes.AddRange((from data in context.CommentLikes.Include("Comment").Include("User") where data.Comment.ID == comment.ID select data).ToList());
            }
            return likes;
        }

        private List<BlogLikes> GetBlogLikes(List<BlogPost> blogs)
        {
            List<BlogLikes> likes = new List<BlogLikes>();
            foreach (BlogPost blog in blogs)
            {
                likes.AddRange((from data in context.BlogLikes.Include("Blog").Include("User") where data.Blog.ID == blog.ID select data).ToList());
            }
            return likes;
        }

        private List<Comment> GetComments(List<BlogPost> blogs)
        {
            List<Comment> comments = new List<Comment>();
            foreach (BlogPost blog in blogs)
            {
                comments.AddRange((from data in context.Comments.Include("Blog").Include("User") where data.Blog.ID == blog.ID select data).ToList());
            }
            return comments;
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BlogPost model)
        {
            model.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
            model.DatePosted = DateTime.Now;
            var follows = (from data in context.Follows where data.FollowedUser.Id == model.User.Id select data).ToList();
            foreach(Follow follow in follows)
            {
                follow.DateUpdated = DateTime.Now;
                context.SaveChanges(); 
            }
            context.BlogPosts.Add(model);
            context.SaveChanges();
            SetBlogTags(model);
            return RedirectToAction("index");
        }
        public ActionResult Report(int ID)
        {
            Report report = new Report();
            report.ReportedBlogPost = (from data in context.BlogPosts where data.ID == ID select data).First();
            return View(report);
        }
        [HttpPost]
        public ActionResult Report(Report report)
        {
            report.ReportingUser = StaticClasses.UserRetriever.RetrieveUser(User, context);
            report.ReportedBlogPost = (from data in context.BlogPosts where data.ID == report.ReportedBlogPost.ID select data).First();
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
            BlogLikes like = new BlogLikes();
            like.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
            like.Blog = (from data in context.BlogPosts where data.ID == id select data).First();
            var likes = (from data in context.BlogLikes where data.Blog.ID == like.Blog.ID && data.User.Id == like.User.Id select data).ToList();
            if(likes.Count > 0)
            {
                context.BlogLikes.Remove(likes[0]);
                context.SaveChanges();
            }
            else
            {
                context.BlogLikes.Add(like);
                context.SaveChanges();
            }
            return Redirect(Request.UrlReferrer.ToString());
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
            blogTag.Blog = model;
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