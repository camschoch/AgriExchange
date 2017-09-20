using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class BlogViewModel
    {
        public ApplicationUser User { get; set; }
        public List<BlogPost> Blogs { get; set; }
        public List<BlogTags> Tags { get; set; }
        public List<BlogLikes> BlogLikes { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CommentLikes> CommentLikes { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public BlogViewModel()
        {
            Blogs = new List<BlogPost>();
            BlogLikes = new List<BlogLikes>();
            Comments = new List<Comment>();
            CommentLikes = new List<Models.CommentLikes>();
            Tags = new List<BlogTags>();
            Users = new List<ApplicationUser>();
        }
    }
}