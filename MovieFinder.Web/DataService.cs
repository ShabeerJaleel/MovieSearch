using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data.SQLite;
using System.Configuration;
using MovieFinder.Web.Data;

namespace MovieFinder.Web
{
    public class DataService
    {
        private static string InstallFile;
        private static string DownloadFile;
        private static string ShowAdsFile;

        static DataService ()
	    {
             try
            {
                DownloadFile = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "download.txt");
                InstallFile = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "install.txt");
                ShowAdsFile = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "showads.txt");
            }
            catch { }
	    }
        public static void LogInstall(string message, string computername, string os, string uid)
        {
            lock (InstallFile)
            {
                try
                {
                    if (!String.IsNullOrWhiteSpace(message) ||
                        !String.IsNullOrWhiteSpace(computername) ||
                        !String.IsNullOrWhiteSpace(os))
                    {

                        System.IO.File.AppendAllText(InstallFile, 
                            String.Format("Machine:{0},UID:{1},OS:{2},Details:{3},Date:{4}{5}",
                            computername, uid, os, message, DateTime.Now.ToString(), Environment.NewLine));
                    }
                }
                catch { }
            }
        }
        public static void LogDownload(string ip)
        {
            lock (DownloadFile)
            {
                File.AppendAllText(DownloadFile, String.Format("IP: {0}, Time: {1}{2}",
                        ip, DateTime.Now.ToString(),
                        Environment.NewLine));
            }
        }

        public static void RemoveLink(Guid uid, string link)
        {

            try
            {
                using (var db = new MovieFinderEntities())
                {
                    var r = db.AccessLogs.FirstOrDefault(x => x.UniqueID == uid );
                    if (r == null) 
                        return;
                    var ml = db.MovieLinks.FirstOrDefault(x => x.DowloadUrl == link && x.FailedAttempts <= 3);
                    if (ml != null)
                    {
                        if (MovieTube.Client.Scraper.VideoScraperBase.ValidateUrl(ml.DowloadUrl) == MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist)
                        {
                            ml.FailedAttempts = 5;
                            ml.LastValidatedBy = uid;
                            db.SaveChanges();
                        }
                    }
                }
            }
            catch { }
        }
        public static void LogShowAd(Guid uid, string ipAddress, string countryCode, DateTime clientTime, string pcName, string version)
        {
            try
            {
                using(var db = new MovieFinderEntities())
                {
                    var r = db.AccessLogs.FirstOrDefault(x => x.UniqueID == uid);
                    if (r != null)
                        r.IPAddress = ipAddress;
                    else
                    {
                        r = new AccessLog
                        {
                            IPAddress = ipAddress,
                            UniqueID = uid
                        };
                        db.AccessLogs.AddObject(r);
                    }

                    r.PCName = pcName;
                    r.ClientTime = clientTime;
                    r.Timestamp = DateTime.Now;
                    r.CountryCode = countryCode;
                    r.Version = version;
                    r.AccessCount++;
                    db.SaveChanges();
                }
                
            }
            catch { }

        }
        public static void LogAd()
        {
        }


        public static int MovieDBVersion
        {
            get
            {
                var path = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["MovieDBRaw"]);
                using (var connection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", path)))
                {
                    connection.Open();
                    using (var cmd = new SQLiteCommand(connection))
                    {

                        cmd.CommandText = "SELECT * FROM Settings LIMIT 1";
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            return (int)reader["Version"];
                        }
                    }
                }

            }
        }
    }
}