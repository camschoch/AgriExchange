using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.Models.ViewModels
{
    public class AdminVendorViewModel
    {
        public List<ApplicationUser> Vendors { get; set; }
        public List<VendorApplications> Applications { get; set; }

        public AdminVendorViewModel()
        {
                Vendors = new List<ApplicationUser>();
                Applications = new List<VendorApplications>();
        }
    }
}