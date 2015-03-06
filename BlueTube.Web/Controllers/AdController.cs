using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace BlueTube.Web.Controllers
{
    public class AdController : Controller
    {
        //
        // GET: /Ad/

        public ActionResult Index(string id, string uid)
        {
            return View("Index",(object)id);
        }

        public bool ShowAds(string uid)
        {
            DataService.LogShowAd(uid);
            return Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAds"]);
        }

    }
}
