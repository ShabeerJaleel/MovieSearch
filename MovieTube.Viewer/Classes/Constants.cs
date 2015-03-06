using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MovieTube.Viewer.Properties;
using System.Reflection;
using System.Net;
using System.Configuration;
using System.Globalization;
using System.Threading;
using MovieTube.Client.Scraper;
using MovieTube.Viewer.Data;

namespace MovieTube.Viewer
{
    public static class Constants
    {
        static Constants()
        {
              try
            {
                if (File.Exists(UniqueIdFile))
                    UniqueId = new Guid(File.ReadAllText(UniqueIdFile));
                else
                {
                    UniqueId = Guid.NewGuid();
                    File.WriteAllText(UniqueIdFile, UniqueId.ToString());
                }
            }
            catch { }
            UpdateShowAds();
            UpdateMessage();
          
        }

        public static void UpdateShowAds()
        {
            try
            {
                var client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                var qp = System.Uri.EscapeUriString(String.Format("uid={0}&cn={1}&timeStamp={2}&pcName={3}&version={4}", 
                    UniqueId.ToString(), CountryCode,
                    DateTime.Now.Ticks, SystemInformation.ComputerName, AppVersion.ToString()));
                client.DownloadStringAsync(new Uri(String.Format("{0}?{1}", UrlConstants.ShowAdsUrl, qp)));
            }
            catch { }
        }

        public static void UpdateMessage()
        {
            try
            {
                var client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_Message_DownloadStringCompleted);
                var qp = System.Uri.EscapeUriString(String.Format("uid={0}&cn={1}&timeStamp={2}&pcName={3}&version={4}",
                    UniqueId.ToString(), CountryCode,
                    DateTime.Now.Ticks, SystemInformation.ComputerName, AppVersion.ToString()));
                client.DownloadStringAsync(new Uri(String.Format("{0}?{1}", UrlConstants.ServerMessageUrl, qp)));
            }
            catch { }
        }

        static void client_Message_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
               ServerMessage = e.Result;
        }

        static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                if (!String.IsNullOrEmpty(e.Result))
                    ShowAds = Convert.ToBoolean(e.Result);
            }
        }
        private static bool showAds;


        public static string MovieDatabaseFilePath
        {
            get
            {

                var path = ConfigurationManager.AppSettings["VideoPath"];
                return Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path));
            }
        }

        public static bool ShowEnglish
        {
            get
            {

                return Convert.ToBoolean(ConfigurationManager.AppSettings["ShowEnglish"]);
            }
        }

        public static string TempFolder
        {
            get
            {
                return Path.Combine(Application.StartupPath, "Temp");
            }
        }

        public static string UpdaterFolder
        {
            get
            {
                return Path.Combine(Application.StartupPath,"Update");
            }
        }

        public static string UpdaterProgram
        {
            get
            {
                return Path.Combine(UpdaterFolder, "MovieTube.UpService.exe");
            }
        }

        public static Version AppVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public static string AppExecutableName
        {
            get
            {
                return "MovieTube.exe";
            }
        }

        public static string UniqueIdFile
        {
            get
            {
                return "id.dat";
            }
        }

        private static bool? isDemo;
        public static bool IsDemoMode
        {
            get
            {
                //if (isDemo == null)
                //{
                //    isDemo = File.Exists("mod.lic");
                //}
                //return isDemo.Value;
                return false;
            }
        }

        public static int DemoCount
        {
            get
            {
                return 4;
            }
        }

        public static string AppTitle
        {
            get
            {
                return "MovieTube";
            }
        }

        public static int GetMaxDisplayCount(int max)
        {
            if (IsDemoMode && max > DemoCount)
                return DemoCount;
            return max;
        }

        public static string ServerMessage { get; set; }
        public static bool ShowAds { get; set; }
        public static string VerticalAdId { get { return "V"; } }
        public static string HorizontalAdId { get { return "H"; } }
        public static Guid UniqueId { get; set; }

        public static string CountryCode
        {
            get
            {
                return RegionInfo.CurrentRegion.TwoLetterISORegionName;
            }
        }

        public static bool IsNewLink(MovieLink link)
        {
            return link.Version > (AppSettings.LastDBVersion - 2);
        }

        public static bool IsNewLink(Movie link)
        {
            return link.Version > (AppSettings.LastDBVersion - 2) && link.ModifiedDate != null;
        }
        
        public static bool IsNewMovie(Movie link)
        {
            return link.Version > (AppSettings.LastDBVersion - 2) && link.ModifiedDate == null ;
        }
       
    }
}
