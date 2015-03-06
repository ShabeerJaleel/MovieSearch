using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MovieTube.Viewer.Properties;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Ionic.Zip;
using System.Diagnostics;
using MovieTube.Viewer.Data;

namespace MovieTube.Viewer
{
    public class UpdaterService
    {
        static UpdaterService()
        {
           
        }


        public int GetLatestMovieDatabaseVersion(bool forcefully )
        {
            var service = ClientDataService.Single;
            try
            {
                if (forcefully || service.Settings.DBVersionCheckedTime.AddHours(1) < DateTime.Now)
                {
                    string result;
                    if (GetResource(UrlConstants.MovieDatabaseVersionCheckUrl, out result))
                    {
                        var version = service.MovieDBVersion;
                        Int32.TryParse(result.Trim(), out version);
                        var gs = service.Settings;
                        gs.DBVersionCheckedTime = DateTime.Now;
                        ClientDataService.Single.SaveGlobalSettings(gs);
                        return version;
                    }
                }
            }
            catch { }
            return service.MovieDBVersion;
        }

        public bool DownloadMovieDatabase(bool forcefully = false)
        {
            try
            {
                if (Directory.Exists(Constants.TempFolder))
                    Directory.Delete(Constants.TempFolder, true);
                Directory.CreateDirectory(Constants.TempFolder);

                var file = Path.Combine(Constants.TempFolder, "_db.zip");

                var version = GetLatestMovieDatabaseVersion(forcefully);
                if (version > ClientDataService.Single.MovieDBVersion || !File.Exists(Constants.MovieDatabaseFilePath))
                {

                    if (Download(UrlConstants.MovieDatabaseDownloadUrl, file))
                    {
                        AppSettings.LastDBVersion = ClientDataService.Single.MovieDBVersion;
                        ClientDataService.Single.Close();
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
            catch { }
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

        public Version GetLatestAppVersion(bool forcefully = false)
        {
            try
            {
                if (forcefully || ClientDataService.Single.Settings.AppVersionCheckedTime.AddDays(1) < DateTime.Now)
                {
                    string result;
                    if (GetResource(UrlConstants.AppVersionCheckUrl, out result))
                    {
                        var version = new Version(result);
                        var gs = ClientDataService.Single.Settings;
                        gs.AppVersionCheckedTime = DateTime.Now;
                        ClientDataService.Single.SaveGlobalSettings(gs);
                        return version;
                    }
                }
            }

            catch { }
            return Constants.AppVersion;
        }

        public Version DownloadAppUpdate(bool forcefully = false)
        {
            var file = Path.Combine(Application.StartupPath, "_app.zip");
            if (File.Exists(file))
                File.Delete(file);
            try
            {
                var version = GetLatestAppVersion(forcefully);
                if (version > Constants.AppVersion)
                {
                    var success = IsNewVersionDownloaded();
                    if (!success && Download(UrlConstants.AppUpdateDownloadUrl, file))
                    {
                        using (ZipFile zip1 = ZipFile.Read(file))
                        {
                            if (Directory.Exists(Constants.UpdaterFolder))
                                Directory.Delete(Constants.UpdaterFolder, true);
                            Directory.CreateDirectory(Constants.UpdaterFolder);
                            zip1.ExtractAll(Constants.UpdaterFolder, ExtractExistingFileAction.OverwriteSilently);
                            success = IsNewVersionDownloaded();
                        }
                    }

                    return version;
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
            return Constants.AppVersion; 
           
        }

        public bool IsNewVersionDownloaded()
        {
            var path = Path.Combine(Constants.UpdaterFolder, Constants.AppExecutableName);
            if (File.Exists(path))
            {
                path = Path.Combine(Constants.UpdaterFolder, Constants.UpdaterProgram);
                if (File.Exists(path))
                    return Constants.AppVersion < GetDownloadedAppVersion();
            }
            return false;
        }

        public Version GetDownloadedAppVersion()
        {
            var path = Path.Combine(Constants.UpdaterFolder, Constants.AppExecutableName);
            return AssemblyName.GetAssemblyName(path).Version;
        }


        public bool ExecuteUpdater()
        {
            try
            {
                var path = Path.Combine(Constants.UpdaterFolder, Constants.AppExecutableName);
                if(File.Exists(path) && Constants.AppVersion < AssemblyName.GetAssemblyName(path).Version)
                {
                    var process = Process.Start(Constants.UpdaterProgram);
                    return true;
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
                    url = url + "?uid=" + Constants.UniqueId.ToString();
                    client.DownloadFile(url, filePath);
                }
                catch { return false; }
            }
            return true;
        }

        public static bool PostResource(string url, NameValueCollection data)
        {
            //result = null;
            try
            {
                using (var client = new WebClient())
                {
                    data.Add("uid", Constants.UniqueId.ToString());
                    byte[] responsebytes = client.UploadValues(url, "POST", data);
                    //result = Encoding.UTF8.GetString(responsebytes);
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
                url = url + "?uid=" + Constants.UniqueId.ToString();
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
