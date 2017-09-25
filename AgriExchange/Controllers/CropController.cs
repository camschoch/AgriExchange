using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class CropController : Controller
    {
        public ApplicationDbContext context;
        public CropController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Crop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CropEntry model)
        {
            model.DatePlanted = DateTime.Now;
            model.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
            model.DateHarvested = DateTime.Now;
            context.CropEntries.Add(model);
            context.SaveChanges();
            return RedirectToAction("Index","Gardener");
        }
        public ActionResult Harvest(int id)
        {
            CropEntry crop = (from data in context.CropEntries where data.ID == id select data).First();
            crop.DateHarvested = DateTime.Now;
            context.SaveChanges();
            AddHarvestBadge(crop);
            return RedirectToAction("Index", "Gardener");
        }
        public ActionResult ChangePublic(int id)
        {
            CropEntry crop = (from data in context.CropEntries where data.ID == id select data).First();
            if (crop.IsPublic)
            {
                crop.IsPublic = false;
                context.SaveChanges();
            }
            else
            {
                crop.IsPublic = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Gardener");
        }
        private void AddHarvestBadge(CropEntry crop)
        {
            var badges = (from data in context.Badges where data.Name == crop.Crop select data).ToList();
            if(badges.Count > 0)
            {
                UserBadges badge = new UserBadges();
                badge.Badge = badges[0];
                badge.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
                badge.DateEarned = DateTime.Now;
                context.UserBadges.Add(badge);
                context.SaveChanges();
            }
            else
            {
                Badge badge = new Badge();
                badge.Name = crop.Crop;
                context.Badges.Add(badge);
                context.SaveChanges();
                UserBadges userBadge = new UserBadges();
                userBadge.User = StaticClasses.UserRetriever.RetrieveUser(User, context);
                userBadge.Badge = (from data in context.Badges where data.Name == badge.Name select data).First();
                userBadge.DateEarned = DateTime.Now;
                context.UserBadges.Add(userBadge);
                context.SaveChanges();
            }
        }
    }
}