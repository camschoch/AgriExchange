using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ApiDataClasses
{
    public class FarmersMarketData
    {
        public List<resultsFarmerItem> results { get; set; }
        public List<marketdetailsItem> marketdetails { get; set; }
    }
    public class resultsFarmerItem
    {
        public string id { get; set; }
        public string marketname { get; set; }       
    }
    public class marketdetailsItem
    {
        public string Address { get; set; }
        public string Products { get; set; }
    }
}