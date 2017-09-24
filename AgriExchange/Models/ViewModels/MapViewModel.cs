using AgriExchange.Models.ApiDataClasses;
using AgriExchange.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class MapViewModel
    {
        public List<GeoAddress> results { get; set; }
        public List<ApplicationUser> Vendor { get; set; }
        public float lat { get; set; }
        public float lng { get; set; }

    }
}