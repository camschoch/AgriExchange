using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Tags
    {
        public int ID { get; set; }
        public BlogPost Blog { get; set; }
        public string Tag { get; set; }
    }
}