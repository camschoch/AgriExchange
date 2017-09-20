using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class BlogViewModel
    {
        List<BlogPost> Blogs { get; set; }
        List<BlogTags> Tags { get; set; }
        List<BlogLikes> BlogLikes { get; set; }
        List<Comment> Comments { get; set; }
        List<CommentLikes> CommentLikes { get; set; }
        List<ApplicationUser> Users { get; set; }
    }
}