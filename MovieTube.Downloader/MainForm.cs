using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace MovieTube.Downloader
{
    public partial class MainForm : Form
    {
        private Thread thread;
        private static string Folder = @"_MovieTube\MovieTube.Installer.exe";
        private static string SetupPath = @"_MovieTube\MovieTube.Setup.msi";
        private static string VersionUrl = "http://www.filmkhoj.com/api/appversion";
        private static string DownloadUrl = "http://www.filmkhoj.com/Home/Download2";
        private WebClient client = new WebClient();
        private int progress;
        public MainForm()
        {
            InitializeComponent();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.thread = new Thread(DownloadAndLaunch);
            thread.Start();
        }

        private string FilePath
        {
            get
            {
                return Path.Combine(Application.StartupPath, Folder);
            }
        }

        private string DirectoryPath
        {
            get
            {
               return  Path.GetDirectoryName(FilePath);
            }
        }
       
        private void DownloadAndLaunch()
        {
            try
            {
                var path = FilePath;
                
                //string v;
                //Version verion = null;
                //Version dVersion = null;
                //if (GetResource(VersionUrl, out v))
                //    verion = new Version(v);

                //if (File.Exists(path))
                //{
                //    try
                //    {
                //        dVersion = AssemblyName.GetAssemblyName(Path.Combine(Application.StartupPath,)).Version;
                //    }
                //    catch { }
                //}

                //if (dVersion == null || dVersion < verion)
                {
                    var dir = DirectoryPath;
                    if (Directory.Exists(dir))
                        Directory.Delete(dir, true);
                    Directory.CreateDirectory(dir);

                    Download(DownloadUrl, path);
                }
                //else
                //{
                //    this.progressBar1.Value = 100;
                //    LaunchInstaller();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Downloader");
            }

        }

        private void Download(string url, string filePath)
        {
           client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
           client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
           client.DownloadFileAsync(new Uri(url), filePath);
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (File.Exists(FilePath))
            {
                LaunchInstaller();
            }
            else
            {
                MessageBox.Show(e.Error.Message, "Downloader");
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progress = e.ProgressPercentage;
        }

        private bool GetResource(string url, out string result)
        {
            result = null;
            try
            {
                using (var client = new WebClient())
                {
                    result = client.DownloadString(url);
                }
            }
            catch { return false; }
            return true;
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value = this.progress;
        }

        private void LaunchInstaller()
        {
            var pi = new ProcessStartInfo()
            {
                WorkingDirectory = DirectoryPath,
                FileName = FilePath
            };
            Process.Start(pi);
            Application.Exit();
        }

    }
  
}
