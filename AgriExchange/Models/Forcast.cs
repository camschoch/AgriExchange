using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class Forcast
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public double HighTemp { get; set; }
        public double LowTemp { get; set; }
        public double Percipitation { get; set; }
        public ApplicationUser User { get; set; }

    }

}
