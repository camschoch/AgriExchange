using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class PlantZone
    {
        public int ID { get; set; }
        public Plant Plant { get; set; }
        public Zone Zone { get; set; }
    }
}