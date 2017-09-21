using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class BlogPost
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Tags { get; set; }
        public DateTime DatePosted { get; set; }

        public static bool AllowedToPost(String UserID)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            var user = dbContext.Users.FirstOrDefault(x => x.Id == UserID);
            if (user == null)
                return false;
            return (DateTime.Now >= user.BlockedUntil);
        }
    }
}