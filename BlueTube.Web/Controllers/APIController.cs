using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mime;
using System.Configuration;
using System.IO;

namespace BlueTube.Web.Controllers
{
    public class APIController : Controller
    {
        //
        // GET: /API/
        public string AppVersion(string uid)
        {
            return ConfigurationManager.AppSettings["AppVersion"];
        }

        public string Key()
        {
            return ConfigurationManager.AppSettings["WeakKey"];
        }

        [HttpPost]
        public string Install(string message, string computername, string os, string uid)
        {
            DataService.LogInstall(message, computername, os, uid);
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


        public ActionResult DownloadApp(string uid)
        {
            return File(ConfigurationManager.AppSettings["BlueTubeApp"], MediaTypeNames.Application.Zip, "BlueTube.zip");
        }

    }
}
