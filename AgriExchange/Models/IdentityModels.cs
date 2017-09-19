﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AgriExchange.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<CropEntry> CropEntries { get; set; }
        public DbSet<UserBadges> UserBadges { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<BlogLikes> BlogLikes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentLikes> CommentLikes { get; set; }
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<VendorApplications> VendorApplications { get; set; }
        public DbSet<BlogTags> BlogTags { get; set; }
        public DbSet<Forcast> Forcasts { get; set; }
    }
}