using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

namespace MovieFinder.Web.Controllers
{
    public class AdController : Controller
    {
        //
        // GET: /Ad/

        public ActionResult Index(string id, string uid, string cn)
        {
            return View("Index", (object)id);
        }

        public bool ShowAds(string uid, string cn, long? timeStamp, string pcName, string version)
        {
            Guid guid = Guid.NewGuid();
            var dt = new DateTime(2000,1,1);
            try
            {
                guid = new Guid(uid);
                dt = new DateTime(timeStamp.Value);
            }
            catch { return false; }
            DataService.LogShowAd(guid, Request.UserHostAddress, cn,dt, pcName, version );
            return Convert.ToBoolean(ConfigurationManager.AppSettings["ShowAds"]);
        }

    }
}
