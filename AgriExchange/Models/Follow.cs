using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Follow
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser FollowedUser { get; set; }
    }
}