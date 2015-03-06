using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using BlueTube.Viewer.Properties;
using System.Collections.Specialized;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using Ionic.Zip;
using System.Diagnostics;

namespace BlueTube.Viewer
{
    public class UpdaterService
    {
        static UpdaterService()
        {
           
        }

        public Version GetLatestAppVersion()
        {
            try
            {
                string result;
                if (GetResource(UrlConstants.AppVersionCheckUrl, out result))
                {
                    var version = new Version(result);
                    return version;
                }
            }

            catch { }
            return Constants.AppVersion;
        }

        public bool DownloadAppUpdate()
        {

            var file = Path.Combine(Application.StartupPath, "_app.zip");
            if (File.Exists(file))
                File.Delete(file);
            try
            {
                var version = GetLatestAppVersion();
                if (version > Constants.AppVersion)
                {
                    if (Download(UrlConstants.AppDownloadUrl, file))
                    {
                        using (ZipFile zip1 = ZipFile.Read(file))
                        {
                            if (Directory.Exists(Constants.UpdaterFolder))
                                Directory.Delete(Constants.UpdaterFolder, true);
                            Directory.CreateDirectory(Constants.UpdaterFolder);
                            zip1.ExtractAll(Constants.UpdaterFolder, ExtractExistingFileAction.OverwriteSilently);
                            var path = Path.Combine(Constants.UpdaterFolder, Constants.AppExecutableName);
                            return File.Exists(path) && Constants.AppVersion < AssemblyName.GetAssemblyName(path).Version;
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

        private bool PostResource(string url, NameValueCollection data, out string result)
        {
            result = null;
            try
            {
                using (var client = new WebClient())
                {
                    data.Add("uid", Constants.UniqueId.ToString());
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
