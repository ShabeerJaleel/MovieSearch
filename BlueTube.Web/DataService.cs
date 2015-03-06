using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace BlueTube.Web
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
        public static void LogShowAd(string uid)
        {
            lock (ShowAdsFile)
            {
                File.AppendAllText(ShowAdsFile, String.Format("ID: {0}, Time: {1}{2}",
                        uid, DateTime.Now.ToString(),
                        Environment.NewLine));
            }

        }
        public static void LogAd()
        {
        }
    }
}