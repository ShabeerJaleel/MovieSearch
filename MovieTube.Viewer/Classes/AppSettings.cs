using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MovieTube.Viewer.Data;
using MovieTube.Viewer.Properties;

namespace MovieTube.Viewer
{
    public static class AppSettings
    {
        private static GlobalSettings settings;
       
        public static bool Log
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["Log"]);
            }

        }

        public static string MovieDownloadFolder
        {
            get
            {
                return Settings.Default.MovieDownloadPath;
            }
            set
            {
                Settings.Default.MovieDownloadPath = value;
                Settings.Default.Save();
            }

        }

        public static int LastDBVersion
        {
            get
            {
                var v = Settings.Default.LastDBVersion;
                if (v == 0)
                    return ClientDataService.Single.MovieDBVersion - 1;
                return v;
            }
            set
            {
                Settings.Default.LastDBVersion = value;
                Settings.Default.Save();
            }

        }

      
    }
}
