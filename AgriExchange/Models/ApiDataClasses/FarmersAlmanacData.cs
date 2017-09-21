using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ApiDataClasses
{
    public class FarmersAlmanacData
    {
        public List<resultsFarmerItem> results { get; set; }
    }
    public class resultsFarmerItem
    {
        public string id { get; set; }
        public string marketname { get; set; }
    }
}