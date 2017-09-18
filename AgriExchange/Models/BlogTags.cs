using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class BlogTags
    {
        public int ID { get; set; }
        public Tags Tag { get; set;}
        public BlogPost Blog { get; set; }
    }}