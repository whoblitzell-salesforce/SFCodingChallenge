using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TwitterFeedReader.Api;
using TwitterFeedReader.Utility;

namespace TwitterFeedReader.Controllers
{
    public class HomeController : Controller
    {
        [Compress]
        public ActionResult Index()
        {
            ViewBag.Title = "Twitter Feed Reader Coding Challenge";
            return View();
        }
    }
}
