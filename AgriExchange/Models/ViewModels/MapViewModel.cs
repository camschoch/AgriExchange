using AgriExchange.StaticClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class MapViewModel
    {
        ApplicationDbContext context;
        public MapViewModel()
        {
            context = new ApplicationDbContext();
        }
        public void makingList()
        {
            
        }
    }
}