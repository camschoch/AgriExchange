using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models
{
    public class VendorApplications
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public bool IsApproved { get; set; }
    }
}