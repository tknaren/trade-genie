using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AzTGWebAppBL;

namespace AzTGWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      
        public ActionResult News()
        {
            // Commented as news api is not functioning as expected.
            //NewsInterfaceBL newsInterfaceBL = new NewsInterfaceBL();

            //string[] categories = { "Business", "Sports", "Health", "Science", "Technology" };

            //var latestNews = newsInterfaceBL.GetLatestNews(categories);

            //return View(latestNews);

            return View();
        }
    }
}