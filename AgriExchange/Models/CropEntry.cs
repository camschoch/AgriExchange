using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class CropEntry
    {
        public int ID { get; set; }
        public ApplicationUser User { get; set; }
        public string Crop { get; set; }
        public DateTime DatePlanted { get; set; }
        public DateTime DateHarvested { get; set; }
        public bool IsPublic { get; set; }
    }
}