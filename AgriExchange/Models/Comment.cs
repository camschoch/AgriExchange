﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public BlogPost Blog { get; set; }
        public ApplicationUser User { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
    }
}