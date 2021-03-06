﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.SkyWise
{
    public class WeatherData
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public int baseTemperature { get; set; }
        public List<unitItem> unit { get; set; }
        public int value { get; set; }
        public string validDate { get; set; }
        public string description { get; set; }
        public List<seriesItem> series { get; set; }
        public string name { get; set; }
        public string products { get; set; }
        
    }

    public class seriesItem
    {
        public string validDate { get; set; }
        public string value { get; set; }
    }
    public class unitItem
    {
        public string description { get; set; }
        public string label { get; set; }
    }
}