using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class UserBadges
    {
        public int ID { get; set; }
        public Badge Badge { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime DateEarned { get; set; }
    }
}