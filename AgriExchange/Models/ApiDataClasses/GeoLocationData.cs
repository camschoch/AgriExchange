using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ApiDataClasses
{
    public class GeoLocationData
    {
        public List<resultsItem> results { get; set; }        
    }
    public class resultsItem
    {
        public List<geometryItem> geometry { get; set; }
        public string formatted_address { get; set; }
    }
    public class geometryItem
    {
        public List<locationItem> location { get; set; }
    }
    public class locationItem
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }
}