using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using MovieFinder.Client.Properties;

namespace MovieFinder.Client
{
    public static class Constants
    {
        
        public static string MovieDatabaseFilePath
        {
            get
            {
                return Path.Combine(Application.StartupPath, MovieDatabaseFileName);
            }
        }

        public static string MovieDatabaseFileName
        {
            get
            {
                return "db.dat"; 
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
                return Path.Combine(UpdaterFolder, "MovieFinder.UpService.exe");
            }
        }

        public static Version LatestDownloadedAppVersion
        {
            get
            {
                return new Version(Settings.Default.LatestDownloadedAppVersion);
            }


        }

        public static Version LastRunVersion
        {
            get
            {
                return new Version(Settings.Default.LastRunVersion);
            }


        }

       
    }
}
