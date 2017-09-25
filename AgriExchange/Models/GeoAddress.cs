using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class GeoAddress
    {
        public string address { get; set; }
        public List<float> coordinant { get; set; }

    }
}