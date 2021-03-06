﻿using AgriExchange.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(AgriExchange.Startup))]
namespace AgriExchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }   
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);                
                var user = new ApplicationUser();
                user.UserName = "lars.alfonse@gmail.com";
                string userPWD = "Qwerty!1";
                user.BlockedUntil = new DateTime(1900, 1, 1);
                var chkUser = UserManager.Create(user, userPWD);
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
            } 
            if (!roleManager.RoleExists("Vendor"))
            {
                var role = new IdentityRole();
                role.Name = "Vendor";
                roleManager.Create(role);

            }   
            if (!roleManager.RoleExists("Gardener"))
            {
                var role = new IdentityRole();
                role.Name = "Gardener";
                roleManager.Create(role);

            }
        }

    }
}
