using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Rating
    {
        public int ID { get; set; }
        public ApplicationUser UserRated { get; set; }
        public int Rate { get; set; }
        public ApplicationUser RatingUser { get; set; }

    }
}