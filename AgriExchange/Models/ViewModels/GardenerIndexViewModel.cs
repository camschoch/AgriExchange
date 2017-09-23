using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class GardenerIndexViewModel
    {
        public List<Forcast> Forcast { get; set; }
        public List<Plant> Reccomentdations { get; set; }
        public List<Follow> Follows { get; set; }
        public List<CropEntry> CropEntries { get; set; }
        public GardenerIndexViewModel()
        {
            Forcast = new List<Forcast>();
            Reccomentdations = new List<Plant>();
        }
    }
}