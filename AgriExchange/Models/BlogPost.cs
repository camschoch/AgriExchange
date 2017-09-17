﻿using System;
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
    }
}