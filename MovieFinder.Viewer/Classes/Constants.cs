using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using BlueTube.Viewer.Properties;
using System.Reflection;
using System.Net;

namespace BlueTube.Viewer
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
            try
            {
                var client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri(UrlConstants.ShowAdsUrl + "?uid=" + UniqueId.ToString()));
            }
            catch { }
          
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
                return Path.Combine(UpdaterFolder, "BlueTube.UpService.exe");
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
                return "BlueTube.exe";
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
                if (isDemo == null)
                {
                    isDemo = File.Exists("mod.lic");
                }
                return isDemo.Value;
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
                return IsDemoMode ? "BlueTube - DEMO" : "BlueTube";
            }
        }

        public static int GetMaxDisplayCount(int max)
        {
            if (IsDemoMode && max > DemoCount)
                return DemoCount;
            return max;
        }

        public static bool ShowAds { get; set; }
        public static string VerticalAdId { get { return "V"; } }
        public static string HorizontalAdId { get { return "H"; } }
        public static Guid UniqueId { get; set; }
       
    }
}
