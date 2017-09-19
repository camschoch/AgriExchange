using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class GardenerIndexViewModel
    {
        List<Forcast> Forcast { get; set; }
        public GardenerIndexViewModel()
        {
            Forcast = new List<Forcast>();
        }
    }
}