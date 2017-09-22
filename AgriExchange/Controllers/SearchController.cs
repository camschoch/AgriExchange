using AgriExchange.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgriExchange.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(string SearchParameter)
        {
            SearchViewModel model = new SearchViewModel();
            model.SearchParameters = SearchParameter;
            return View(model);
        }
    }
}