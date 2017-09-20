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
        public ActionResult Index()
        {
            ApplicationUser user = StaticClasses.UserRetriever.RetrieveUser(User);
            BlogViewModel model = new BlogViewModel();
            model.Blogs = (from data in context.BlogPosts.Include("User") where data.User.Id == user.Id select data).ToList();
            model.Comments = GetComments(model.Blogs);
            model.BlogLikes = GetBlogLikes(model.Blogs);
            model.CommentLikes = GetCommentLikes(model.Comments);
            model.Tags = GetBlogTags(model.Blogs);
            return View(model);
        }

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
            foreach(BlogPost blog in blogs)
            {
                likes.AddRange((from data in context.BlogLikes.Include("Blog").Include("User") where data.Blog.ID == blog.ID select data).ToList());
            }
            return likes;
        }

        private List<Comment> GetComments(List<BlogPost> blogs)
        {
            List<Comment> comments = new List<Comment>();
            foreach(BlogPost blog in blogs)
            {
                comments.AddRange((from data in context.Comments.Include("Blog").Include("User") where data.Blog.ID == blog.ID select data).ToList());
            }
            return comments;
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