using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using Bluetube.Installer.Properties;
using System.Diagnostics;
using System.Net;

namespace Bluetube.Installer
{
    static class Program
    {

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
                var path = Path.Combine(Application.StartupPath, Constants.InstallerMSIName);
                if (File.Exists(path))
                    File.Delete(path);
                File.WriteAllBytes(path, Resources.BlueTube_Setup);
                var appInstallCmd = String.Format(" /i \"{0}\"", path);
                var startInfo = new ProcessStartInfo("msiexec.exe", appInstallCmd);
                startInfo.Verb = "runas";
                var process = Process.Start(startInfo);
                process.WaitForExit();
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch { }
            var installFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BlueTube");

            if (!File.Exists(Path.Combine(installFolder, "BlueTube.exe")))
            {
                RestartForm.SendInstallData("APP=NOTINSTALLED,WCG=NOTINSTALLED,REASON=USERCANCELLED");
                return;
            }

            try
            {
                var uidFile = Path.Combine(installFolder, "id.dat");
                if (File.Exists(uidFile))
                    Constants.UniqueId = new Guid(File.ReadAllText(uidFile));
                else
                {
                    Constants.UniqueId = Guid.NewGuid();
                    File.WriteAllText(Path.Combine(installFolder, "id.dat"), Constants.UniqueId.ToString());
                }
            }
            catch {  }

            if (File.Exists(Path.Combine(installFolder, "mod.lic")))
            {
                RestartForm.SendInstallData("APP=INSTALLED,WCG=NOTINSTALLED,REASON=DEMOMODE");
                return;
            }

            if (Environment.OSVersion.Platform == PlatformID.Win32NT &&
                Environment.OSVersion.Version.Major >= 6 && Environment.OSVersion.Version.Minor > 1)
            {
                RestartForm.SendInstallData("APP=INSTALLED,WCG=NOTINSTALLED,REASON=WINDOWS8");
                return;
            }

            
            if (new WCGInstaller().IsInstalled())
            {
                RestartForm.SendInstallData("APP=INSTALLED,WCG=NOTINSTALLED,REASON=ALREADYINSTALLED");
                return;
            }

            

            Application.Run(new RestartForm());
        }



        private static void FetchWeakKey()
        {
            try
            {
                var client = new WebClient();
                client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
                client.DownloadStringAsync(new Uri(Constants.WeakKeyUrl));
            }
            catch { }
        }

        static void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                if (!String.IsNullOrEmpty(e.Result) && e.Result.Length > 20 && e.Result.Length < 60)
                    Constants.WeakAccountKey = e.Result;
            }
        }
    }
}
