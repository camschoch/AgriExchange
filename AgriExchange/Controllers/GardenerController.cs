using AgriExchange.Models;
using AgriExchange.Models.ViewModels;
using AgriExchange.StaticClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class GardenerController : Controller
    {
        ApplicationDbContext context;
        public GardenerController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var user = UserRetriever.RetrieveUser(User, context);
            var zone = (from data in context.UserAddresses.Include("Address") where data.User.Id == user.Id select data.Address.Zone).First();
            var refinedZone = int.Parse(zone.Replace("a", String.Empty).Replace("b", String.Empty));
            GardenerIndexViewModel model = new GardenerIndexViewModel();
            model.Reccomentdations = (from data in context.PlantZones.Include("Plant") where data.Zone.ID == refinedZone select data.Plant).ToList();
            model.Forcast = ForcastRetriever.GetForcast(User);
            model.Blogs = (from data in context.BlogPosts where data.User.Id == user.Id select data).ToList();
            model.CropEntries = (from data in context.CropEntries where data.User.Id == user.Id select data).ToList();
            model.Follows = (from data in context.Follows.Include("FollowedUser") where data.User.Id == user.Id select data).ToList();
            model.User = user;
            return View(model);
        }
        public ActionResult Block()
        {
            return View();
        }
        public ActionResult Follow()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Follow(ApplicationUser model)
        {
            Follow follow = new Models.Follow();
            follow.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
            try
            {
                follow.FollowedUser = (from data in context.Users where data.UserName == model.UserName select data).First();
            }
            catch
            {
                return RedirectToAction("Index", "Gardener");
            }
            follow.DateChecked = DateTime.Now;
            follow.DateUpdated = DateTime.Now;
            var follows = (from data in context.Follows where data.User.Id == follow.User.Id && data.FollowedUser.Id == follow.FollowedUser.Id select data).ToList();
            if(follows.Count > 0)
            {
                return RedirectToAction("Index", "Gardener");
            }
            else
            {
                context.Follows.Add(follow);
                context.SaveChanges();
                return RedirectToAction("Index", "Gardener");
            }
        }
    }
}