using AgriExchange.Models;
using AgriExchange.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.StaticClasses
{
    public static class UserRetriever
    {
        public static ApplicationUser RetrieveUser(System.Security.Principal.IPrincipal User)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            string userName = User.Identity.GetUserName();
            ApplicationUser user = (from data in context.Users where data.UserName == userName select data).First();
            return user;
        }
    }

}