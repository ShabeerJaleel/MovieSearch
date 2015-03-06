using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mime;
using System.IO;

namespace BlueTube.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public FileResult Download()
        {
            DataService.LogDownload(Request.UserHostAddress);
            Response.AddHeader("Content-Length",
                new FileInfo(Path.Combine(Server.MapPath("~/App_Data"), "BlueTube.Installer.exe")).Length.ToString(System.Globalization.CultureInfo.InvariantCulture));
            Response.BufferOutput = false;
            Response.Buffer = false;
            return File(ConfigurationManager.AppSettings["BlueTubeAppSetup"], MediaTypeNames.Application.Octet, "Bluetube.Installer.exe");
            //using (var fStream = new FileStream(Path.Combine(Server.MapPath("~/App_Data"), "BlueTube.Installer.exe"), FileMode.Open))
            //{
            //    WriteDataToOutputStream(fStream, fStream.Length, "BlueTube.Installer.exe", HttpContext);
            //}
        }

        private void PrepareResponseStream(string clientFileName, HttpContextBase context, long sourceStreamLength)
        {
            context.Response.ClearHeaders();
            context.Response.Clear();

            context.Response.ContentType = "APPLICATION/OCTET-STREAM";
            context.Response.AddHeader("Content-Disposition", string.Format("filename=\"{0}\"", clientFileName));

            //set cachebility to private to allow IE to download it via HTTPS. Otherwise it might refuse it
            //see reason for HttpCacheability.Private at http://support.microsoft.com/kb/812935
            context.Response.Cache.SetCacheability(HttpCacheability.Private);
            context.Response.Buffer = false;
            context.Response.BufferOutput = false;
            context.Response.AddHeader("Content-Length", sourceStreamLength.ToString(System.Globalization.CultureInfo.InvariantCulture));
        }

        private void WriteDataToOutputStream(Stream sourceStream, long sourceStreamLength, string clientFileName, HttpContextBase context)
        {
            PrepareResponseStream(clientFileName, context, sourceStreamLength);
            const int BlockSize = 4 * 1024 * 1024;
            byte[] buffer = new byte[BlockSize];
            int bytesRead;
            using (Stream outStream = context.Response.OutputStream)
            {
                while ((bytesRead = sourceStream.Read(buffer, 0, BlockSize)) > 0)
                {
                    outStream.Write(buffer, 0, bytesRead);
                }
                outStream.Flush();
            }
        }

    }
}
