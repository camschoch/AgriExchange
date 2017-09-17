using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Report
    {
        public int ID { get; set; }
        public ApplicationUser ReportingUser { get; set; }
        public BlogPost ReportedBlogPost { get; set; }
        public Comment ReportedComment { get; set; }
        public string Reason { get; set; }
    }
}