using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.SkyWise
{
    public class FruitData
    {
        public List<fruitDataItem> results { get; set; }
        public int tfvcount { get; set; }
    }
    public class fruitDataItem
    {
        public string tfvname { get; set; }
        public string botname { get; set; }
        public string othname { get; set; }
        public string imageurl { get; set; }
        public string description { get; set; }
        public string uses { get; set; }
        public string propagation { get; set; }
        public string soil { get; set; }
        public string climate { get; set; }
        public string health { get; set; }

    }
}