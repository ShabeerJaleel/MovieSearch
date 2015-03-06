using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;
using System.Configuration;
using System.IO;
using Ionic.Zip;

namespace MovieFinder.Web.Controllers
{
    public class APIController : Controller
    {
        //
        // GET: /API/
        private static object sync = new object();

        public ActionResult DBVersion()
        {
            return Json(DataService.MovieDBVersion, JsonRequestBehavior.AllowGet);
        }

        public string AppVersion()
        {
            return ConfigurationManager.AppSettings["AppVersion"];
        }

        public string AppVersionEx()
        {
            return "1.0.0.1";
        }

        public string Key()
        {
            return ConfigurationManager.AppSettings["WeakKey"];
        }

        [HttpPost]
        public string Install(string error, string computername, string os)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(error) ||
                    !String.IsNullOrWhiteSpace(computername) ||
                    !String.IsNullOrWhiteSpace(os))
                {
                    var path = Path.Combine(Server.MapPath("~/App_Data"), "install.txt");
                    System.IO.File.AppendAllText(path, String.Format("Machine: {0}, OS: {1}, Error: {2}, Date: {3}{4}",
                        computername, os, error, DateTime.Now.ToString(), Environment.NewLine));

                }
            }
            catch (Exception ex) { return ex.Message; }
            return "ok";

        }

        [HttpPost]
        public void Suggestion(string text, string version)
        {
            if (String.IsNullOrWhiteSpace(text))
                return;
            if (String.IsNullOrWhiteSpace(version))
                return;
            text = text.Substring(0, Math.Min(1000, text.Length));
            
            try
            {
                var path = Path.Combine(Server.MapPath("~/App_Data"), "suggestions.txt");
                System.IO.File.AppendAllText(path, String.Format("Version: {0}, Suggestion: {1}, Date: {2}{3}{4}",
                    version, text, DateTime.Now.ToString(), Environment.NewLine, Environment.NewLine));
            }
            catch (Exception ex) {  }
        }

        [HttpPost]
        public void RemoveLink(Guid uid, string link)
        {
            DataService.RemoveLink(uid, link);
        }

        public ActionResult DownloadDB()
        {

            return File(GetDBFileName(), MediaTypeNames.Application.Zip, "movie.zip");
        }


        public ActionResult DownloadApp()
        {
            return File(ConfigurationManager.AppSettings["MovieApp"], MediaTypeNames.Application.Zip, "MovieTube.zip");
        }

        public ActionResult DownloadAppEx()
        {
            return File(ConfigurationManager.AppSettings["BlueTubeApp"], MediaTypeNames.Application.Zip, "BlueTube.zip");
        }

        private string GetDBFileName()
        {
            var v = DataService.MovieDBVersion;
            lock (sync)
            {
                var fileName = Server.MapPath(String.Format("/App_Data/movie_{0}.zip", v));
                if (!System.IO.File.Exists(fileName))
                {
                    using (var zip = new ZipFile())
                    {
                        zip.AddFile(Server.MapPath(ConfigurationManager.AppSettings["MovieDBRaw"]), "");
                        zip.Save(fileName);
                    }
                }
                return fileName;
            }
        }
    }
}
