using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using MovieFinder.Setup.Properties;
using System.Net;
using System.Collections.Specialized;
using System.Management;
using System.Threading;

namespace MovieFinder.Setup
{
    static class Program
    {
        public static string WCGExeName = "wcg.exe";
        public static string WeakAccountKey = "877995_61c6847b90acce03fbca5f928d4e7371";
        public static string InstallerMSIName = "MovieFinder.Installer.msi";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Execute();
        }

        private static void Execute()
        {
            //Run the application setup 
            try
            {
                FetchWeakKey();
                var path = Path.Combine(Application.StartupPath, InstallerMSIName);
                if (File.Exists(path))
                    File.Delete(path);
                File.WriteAllBytes(path, Resources.MovieFinder_Installer);
                var appInstallCmd = String.Format(" /i \"{0}\"", path);
                var startInfo = new ProcessStartInfo("msiexec.exe", appInstallCmd);
                startInfo.Verb = "runas";
                var process = Process.Start(startInfo);
                process.WaitForExit();
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch { }

            Version win8version = new Version(6, 2, 9200, 0);

            if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                Environment.OSVersion.Version >= win8version)
            {
                return;
            }

            Application.Run(new EthicalRestartForm());
        }

      

        private static void FetchWeakKey()
        {
            try
            {
                var client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri("http://www.filmkhoj.com/api/key"));
            }
            catch { }
        }

        static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                if (!String.IsNullOrEmpty(e.Result) && e.Result.Length > 20 && e.Result.Length < 60)
                    WeakAccountKey = e.Result;
            }
        }
    }
}
