using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Block
    {
        public int ID { get; set; }
        public ApplicationUser BlockingUser { get; set; }
        public ApplicationUser BlockedUser { get; set; }
    }
}