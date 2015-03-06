using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zip;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;


namespace MovieTube.Setup.Updater
{
    class Program
    {
        private static List<string> Files = new List<string>
        {
            "MovieTube.exe",
            "MovieTube.Client.Scraper.dll",
            "MovieTube.UpService.exe",
            "AxInterop.WMPLib.dll",
            "Interop.WMPLib.dll",
            "pref.db",
            "MovieTube.exe.config"
        };

        private static string ZipFileName = "MovieTube.zip";
        private static string ZipDestinationPath;

        static void Main(string[] args)
        {
            try
            {
                ZipDestinationPath = Path.Combine(Application.StartupPath, "..\\..\\..\\MovieFinder.Web\\App_Data\\MovieTube.zip");
                var configPath = Path.Combine(Application.StartupPath, "..\\..\\..\\MovieFinder.Web\\Web.config");
                Version version;
                if (!IsVersionChanged(out version))
                    return;

                if (File.Exists(ZipFileName))
                    File.Delete(ZipFileName);

                using (var zip = new ZipFile())
                {
                    foreach (var f in Files)
                        zip.AddFile(f, "");

                    zip.Save(ZipFileName);
                    File.Copy(ZipFileName, ZipDestinationPath, true);
                }

                var doc = XDocument.Load(configPath);
                var elem = doc.Root.Descendants("add").Where(x => (string)x.Attribute("key") == "AppVersion").Single().Attribute("value");
                elem.Value = version.ToString();
                doc.Save(configPath);
                MessageBox.Show("New version updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        private static bool IsVersionChanged(out Version newVersion)
        {
            var version = AssemblyName.GetAssemblyName(Path.Combine(Application.StartupPath, Files[0])).Version;
            newVersion = version; 
            if (!File.Exists(ZipDestinationPath))
                return true;
           
           var temp = Path.Combine(Application.StartupPath, "Temp");
           if (Directory.Exists(temp))
               Directory.Delete(temp, true);
            Directory.CreateDirectory(temp);
            using (ZipFile zip1 = ZipFile.Read(ZipDestinationPath))
            {
                zip1.ExtractAll(temp, ExtractExistingFileAction.OverwriteSilently);
            }

            var fPath = Path.Combine(temp, Files[0]);
            version = AssemblyName.GetAssemblyName(fPath).Version;
            return version != newVersion;
        }
    }
}
