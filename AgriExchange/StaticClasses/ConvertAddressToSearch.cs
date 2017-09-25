using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.StaticClasses
{
    public class ConvertAddressToSearch
    {
        static public string ConvertAddress(string userName, ApplicationDbContext context)
        {
            int addressId = 0;
            var user = (from data in context.Users where data.UserName == userName select data).First();
            var allAddresses = (from data in context.UserAddresses.Include("Address") where data.User.Id == user.Id select data);
            foreach (var item in allAddresses)
            {
                addressId = item.Address.ID;
            }
            var addressObject = (from data in context.Addresses.Include("City").Include("City.State") where data.ID == addressId select data).First();
            string realAddress = addressObject.addressLine.ToLower();
            var state = addressObject.City.State.abbreviation;
            string city = addressObject.City.City.Replace(" ", "%20");
            string splitAddress = realAddress.Replace(".", string.Empty).Replace(" ", "%20");
            var searchString = splitAddress + "%2C%20" + city + "%2C%20" + state;
            return searchString;
        }
    }
}