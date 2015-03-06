using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mime;

namespace MovieFinder.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Share()
        {
            return View();
        }

        public ActionResult Download()
        {
            return File(ConfigurationManager.AppSettings["MovieTubeSetup"], MediaTypeNames.Application.Octet, "MTDownloader.exe");
        }

        public ActionResult Download2()
        {
            return File(ConfigurationManager.AppSettings["MovieTubeInstaller"], MediaTypeNames.Application.Octet, "MovieTube.Installer.exe");
        }

        public string Message(string uid, string cn, long? timeStamp, string pcName, string version)
        {
            return "Firedrive.com and Sockshare.com are temporarily out of service";
        }

        public ActionResult Schedule()
        {
            var model = new ScheduleModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Schedule(DateTime from, DateTime to)
        {
            var model = new ScheduleModel()
            {
                Activities = ScheduleService.GetActivities(from, to)
            };
            return View(model);
        }

    }
}
