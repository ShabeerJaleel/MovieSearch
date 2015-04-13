using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MovieTube.Web.Models;
using MovieTube.Data;

namespace MovieTube.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository repository;

        public HomeController()
        {
            repository = new Repository();
        }

        //
        // GET: /Home/
       // [OutputCache(Duration=3600)]
        public ActionResult Index()
        {
            var tn = repository.List(String.Empty, String.Empty, null, null);
            var d = new JavaScriptSerializer().Serialize(new
            {
                showWatchView = false,
                activeView = "",
                thumbInfo = tn
            });
            return View("Index",(object)d);
        }

        public ActionResult Watch(string language, int year, string id, string title)
        {
           var movie = repository.QueryMovie(id);

            var d = new JavaScriptSerializer().Serialize(new
            {
                showWatchView = true,
                activeView = "watch",
                movie = movie,
                thumbInfo = new ThumbNailVm()
            });

            return View("Index", (object)d);
        }

        public ActionResult Player()
        {
            return View("_Player");
        }

    }
}
