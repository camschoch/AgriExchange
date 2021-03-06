﻿using AgriExchange.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgriExchange.StaticClasses
{
    public static class ForcastRetriever
    {
        public static List<Forcast> GetForcast(System.Security.Principal.IPrincipal User)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            DateTime today = DateTime.Now;
            string todayDate = today.Year.ToString() + "-" + today.Month.ToString() + "-" + today.Day;
            ApplicationUser user = UserRetriever.RetrieveUser(User, context);
            if (context.Forcasts.ToList().Count > 0)
            {
                if (context.Forcasts.First().Date == todayDate && context.Forcasts.First().User.Id == user.Id)
                {
                    return context.Forcasts.ToList();
                }
                else
                {
                    string userName = User.Identity.Name;
                    string convertedAddress = ConvertAddressToSearch.ConvertAddress(userName, context);
                    var geoLocation = ApiCalls.GeoLocationApiUserAddress(userName, context, convertedAddress);
                    string paramater = "/" + geoLocation[0] + "/" + geoLocation[1];
                    ApiCalls.WeatherApi(paramater, User, context);
                    return context.Forcasts.ToList();
                }
            }
            else
            {
                string userName = User.Identity.Name;
                string convertedAddress = ConvertAddressToSearch.ConvertAddress(userName, context);
                var geoLocation = ApiCalls.GeoLocationApiUserAddress(userName, context, convertedAddress);
                string paramater = "/" + geoLocation[0] + "/" + geoLocation[1];
                ApiCalls.WeatherApi(paramater, User, context);
                //make api call to update DB
                return context.Forcasts.ToList();
            }
        }
    }
}