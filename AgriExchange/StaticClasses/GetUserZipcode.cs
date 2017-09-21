using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.StaticClasses
{
    static public class GetUserZipcode
    {
        static public string UserZipcode(string userName, ApplicationDbContext context)
        {
            int addressId = 0;
            var user = (from data in context.Users where data.UserName == userName select data).First();
            var allAddresses = (from data in context.UserAddresses where data.User.Id == user.Id select data);
            foreach (var item in allAddresses)
            {
                addressId = item.ID;
            }
            var zipCodeObject = (from data in context.Addresses.Include("Zip") where data.ID == addressId select data).First();
            string zipCode = zipCodeObject.Zip.zip.ToString();
            return zipCode;
        }
    }
}