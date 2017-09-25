using AgriExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class AddressController : Controller
    {
        ApplicationDbContext context;
        public AddressController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Address model)
        {
            UserAddress junction = new UserAddress();
            ApplicationUser user = StaticClasses.UserRetriever.RetrieveUser(User, context);
            junction.Address = GetAddress(model);
            junction.User = user;
            var existingJunction = (from data in context.UserAddresses where data.User.Id == junction.User.Id select data).ToList();
            if (existingJunction.Count > 0)
            {
                existingJunction[0].Address = junction.Address;
                context.SaveChanges(); 
            }
            else
            {
                context.UserAddresses.Add(junction);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Gardener");
        }
        private Address GetAddress(Address model)
        {
            model.City = GetCity(model);
            model.Zip = GetZip(model);
            model.Zone = StaticClasses.ApiCalls.CurrentZoneApi(model.Zip.zip.ToString());
            var addresses = (from data in context.Addresses where data.addressLine == model.addressLine && data.City.City == model.City.City && data.Zip.zip == model.Zip.zip select data).ToList();
            if (addresses.Count > 0)
            {
                return (from data in context.Addresses where data.addressLine == model.addressLine && data.City.City == model.City.City && data.Zip.zip == model.Zip.zip select data).First();
            }
            else
            {
                context.Addresses.Add(model);
                context.SaveChanges();
                return (from data in context.Addresses where data.addressLine == model.addressLine && data.City.City == model.City.City && data.Zip.zip == model.Zip.zip select data).First();

            }
        }

        private ZipCode GetZip(Address model)
        {
            var Zips = (from data in context.Zips where data.zip == model.Zip.zip select data).ToList();
            if (Zips.Count > 0)
            {
                return (from data in context.Zips where data.zip == model.Zip.zip select data).First();
            }
            else
            {
                context.Zips.Add(model.Zip);
                context.SaveChanges();
                return (from data in context.Zips where data.zip == model.Zip.zip select data).First();
            }
        }

        private Cities GetCity(Address model)
        {
            model.City.State = GetState(model);
            var cities = (from data in context.Cities where data.City == model.City.City && data.State.State == model.City.State.State select data).ToList();
            if (cities.Count > 0)
            {
                return (from data in context.Cities where data.City == model.City.City && data.State.State == model.City.State.State select data).First();
            }
            else
            {
                context.Cities.Add(model.City);
                context.SaveChanges();
                return (from data in context.Cities where data.City == model.City.City && data.State.State == model.City.State.State select data).First();
            }
        }

        private States GetState(Address model)
        {
            var States = (from data in context.States where data.State.ToLower() == model.City.State.State.ToLower() select data).ToList();
            if (States.Count > 0)
            {
                return (from data in context.States where data.State.ToLower() == model.City.State.State.ToLower() select data).First();
            }
            else
            {
                return model.City.State;
            }
        }
    }
}