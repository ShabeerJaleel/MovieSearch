using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MovieFinder.Client.Properties;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Ionic.Zip;
using System.Diagnostics;

namespace MovieFinder.Client
{
    public class UpdaterService
    {
       

        static UpdaterService()
        {
           
        }

        public int GetLatestMovieDatabaseVersion()
        {
            try
            {
                if (Settings.Default.MovieDBVersionCheckedWhen.AddDays(1) < DateTime.Now)
                {
                    string result;
                    if (GetResource(UrlConstants.MovieDatabaseVersionCheckUrl, out result))
                    {
                        var version = Program.MovieDBVersion;
                        Int32.TryParse(result.Trim(), out version);
                        return version;
                    }
                }
            }
            catch { }
            return Program.MovieDBVersion;
        }

        public Version GetLatestAppVersion()
        {
            try
            {
                if (Settings.Default.AppVersionCheckedWhen.AddDays(1) < DateTime.Now)
                {
                    string result;

                    if (GetResource(UrlConstants.AppVersionCheckUrl, out result))
                    {
                        var version = new Version(result);
                        return version;
                    }
                }
            }
            catch { }
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public bool DownloadMovieDatabase()
        {
            try
            {
                if (Directory.Exists(Constants.TempFolder))
                    Directory.Delete(Constants.TempFolder, true);
                Directory.CreateDirectory(Constants.TempFolder);

            var file = Path.Combine(Constants.TempFolder, "_db.zip");
           
                var version = GetLatestMovieDatabaseVersion();
                if (version > Program.MovieDBVersion)
                {

                    if (Download(UrlConstants.MovieDatabaseDownloadUrl, file))
                    {
                        using (ZipFile zip1 = ZipFile.Read(file))
                        {
                            foreach (ZipEntry e in zip1.Entries)
                            {
                                e.Extract(Constants.TempFolder, ExtractExistingFileAction.OverwriteSilently);
                                File.Copy(Path.Combine(Constants.TempFolder, e.FileName), Constants.MovieDatabaseFilePath, true);
                            }
                            return true;
                        }
                    }

                }
            }
            catch {}
            finally
            {
                try
                {
                    if (Directory.Exists(Constants.TempFolder))
                        Directory.Delete(Constants.TempFolder, true);
                }
                catch { }
            }

            return false;
        }

        public bool DownloadAppUpdate()
        {

            var file = Path.Combine(Application.StartupPath, "_app.zip");
            if (File.Exists(file))
                File.Delete(file);
            try
            {
                var version = GetLatestAppVersion();
                if (version > Constants.LatestDownloadedAppVersion)
                {
                    if (Download(UrlConstants.AppDownloadUrl, file))
                    {
                        using (ZipFile zip1 = ZipFile.Read(file))
                        {
                            if (Directory.Exists(Constants.UpdaterFolder))
                                Directory.Delete(Constants.UpdaterFolder, true);
                            Directory.CreateDirectory(Constants.UpdaterFolder);

                            zip1.ExtractAll(Constants.UpdaterFolder, ExtractExistingFileAction.OverwriteSilently);
                            Settings.Default.LatestDownloadedAppVersion = version.ToString();
                            Settings.Default.Save();
                            return true;
                        }
                    }

                }
            }
            catch { }
            finally
            {
                try
                {
                    if (File.Exists(file))
                        File.Delete(file);
                }
                catch { }
            }
            return false; 
           
        }

        public bool ExecuteUpdater()
        {
            try
            {
                if (Constants.LatestDownloadedAppVersion > Assembly.GetExecutingAssembly().GetName().Version)
                {
                    if (File.Exists(Constants.UpdaterProgram))
                    {
                        var process =  Process.Start(Constants.UpdaterProgram);
                        return true;
                    }
                }
            }
            catch { }
            return false;
        } 

        private bool Download(string url, string filePath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, filePath);
                }
                catch { return false; }
            }
            return true;
        }

        private bool PostResource(string url, NameValueCollection data, out string result)
        {
            result = null;
            try
            {
                using (var client = new WebClient())
                {
                    byte[] responsebytes = client.UploadValues(url, "POST", data);
                    result = Encoding.UTF8.GetString(responsebytes);
                }
            }
            catch { return false; }
            return true;
        }

        private bool GetResource(string url, out string result)
        {
            result = null;
            try
            {
                using (var client = new WebClient())
                {
                    result= client.DownloadString(url);
                }
            }
            catch { return false; }
            return true;
        }
    }
}
