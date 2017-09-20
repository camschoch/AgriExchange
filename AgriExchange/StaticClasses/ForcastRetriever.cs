using AgriExchange.Models;
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
            if (context.Forcasts.First().Date == todayDate && context.Forcasts.First().User.Id == user.Id)
            {
                return context.Forcasts.ToList();
            }
            else
            {
                //make api call to update DB
                return context.Forcasts.ToList();
            }
        }
    }
}