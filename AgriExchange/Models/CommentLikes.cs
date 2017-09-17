using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class CommentLikes
    {
        public int ID { get; set; }
        public Comment Comment { get; set; }
        public ApplicationUser User { get; set; }
    }
}