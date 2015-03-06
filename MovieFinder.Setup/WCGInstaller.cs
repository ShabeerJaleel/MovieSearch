using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using System.Diagnostics;
using MovieFinder.Setup.Properties;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MovieFinder.Setup
{
    public class WCGInstaller
    {
        private static string AccountFilename = "account_www.worldcommunitygrid.org.xml";
        #region Fields
        [DllImport("shell32.dll")]
        static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, StringBuilder lpszPath, CSIDL nFolder, bool fCreate);

        enum CSIDL
        {
            COMMON_STARTMENU = 0x0016,
            COMMON_PROGRAMS = 0x0017
        }
        #endregion
        public bool Execute(string weakKey)
        {
            var path = BoincPath();
            path = Path.Combine(path, "boinc.exe");
            if (File.Exists(path))
                return false;
            path = GetBoincDatafolder();
            if (File.Exists(path))
                return false;
            path = Path.Combine(Application.StartupPath, Program.WCGExeName);
            if (!File.Exists(path))
                File.WriteAllBytes(path, Resources.wcg_boinc_6_10_58_windows_intelx86);
            if (!File.Exists(path))
                throw new Exception("Unable to find the wcg file");
            Install(weakKey);
            DeleteEntries();

            ProcessRegistryEntries(weakKey, SaveAccount(weakKey));
            RenameUtils();
            File.Delete(Program.WCGExeName);
            return true;
        }

        private void Install(string weakKey)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                Arguments = String.Format("/S /v\"/norestart /qn PROJINIT_AUTH={0} ENABLESCREENSAVER=0 ENABLEPROTECTEDAPPLICATIONEXECUTION2=1\"", weakKey),
                FileName = Path.Combine(Application.StartupPath, Program.WCGExeName),
                Verb = "runas",
                UseShellExecute = false
            };
            var p = Process.Start(info);
            p.WaitForExit();
        }

        private void DeleteEntries()
        {
            try
            {
                string path = String.Empty;
                try
                {
                    StringBuilder allUsersStartMenu = new StringBuilder(255);

                    if (SHGetSpecialFolderPath(IntPtr.Zero, allUsersStartMenu, CSIDL.COMMON_PROGRAMS, false))
                        path = Path.Combine(allUsersStartMenu.ToString(), "World Community Grid");
                }
                catch { }
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                else
                {
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu),
                        "Programs\\World Community Grid");
                    if (Directory.Exists(path))
                        Directory.Delete(path, true);
                }
            }
            catch { }
        }

        private bool SaveAccount(string weakKey)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(Resources.account_www_worldcommunitygrid_org);
                var keyNode = doc.SelectSingleNode("account/authenticator");
                keyNode.InnerText = weakKey;
                var path = Path.Combine(GetBoincDatafolder(), AccountFilename);
                doc.Save(path);
                try
                {
                    path = Path.Combine(GetBoincDatafolder(), "project_init.xml");
                    //if (File.Exists(path))
                    //    File.Delete(path);
                }
                catch { }
                return true;
            }
            catch { }
            return false;
        }

        private void ProcessRegistryEntries(string weakKey, bool skipRunOnce)
        {

            var boincFolder = BoincPath();
            if (String.IsNullOrEmpty(boincFolder))
                throw new Exception("BOINC Folder does not exists");

            var boincCmd = String.Format("\"{0}\\boinccmd.exe\" --project_attach www.worldcommunitygrid.org {1}",
                boincFolder, weakKey);

            RegistryKey run = null;
            RegistryKey runOnce = null;
            try
            {
                run = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                runOnce = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true);

            }
            catch { }

            try
            {
                if (run == null)
                    run = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (runOnce == null)
                    run = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\RunOnce", true);
            }
            catch { }
            if (run != null)
            {
                if (run.GetValue("boinctray") != null)
                    run.DeleteValue("boinctray");
                if (run.GetValue("boincmgr") != null)
                    run.DeleteValue("boincmgr");
            }

             if(!skipRunOnce)
             {
                  if (runOnce != null)
                       runOnce.SetValue("boincmgr", boincCmd);
                  else if(run != null)
                      run.SetValue("boincmgr", boincCmd);
             }
        }

        private void RenameUtils()
        {
            var boincFolder = BoincPath();
            try
            {
               File.Move( Path.Combine(boincFolder, "boinctray.exe"), Path.Combine(boincFolder, "boinctray1.exe"));
               File.Move(Path.Combine(boincFolder, "boincmgr.exe"), Path.Combine(boincFolder, "boincmgr1.exe"));
               File.Move(Path.Combine(boincFolder, "boincscr.exe"), Path.Combine(boincFolder, "boincscr1.exe"));
            }
            catch { }
        }

        private string BoincPath()
        {
            try
            {
                var boincFolder = Path.Combine(ProgramFilesx86(), "BOINC");
                if (Directory.Exists(boincFolder))
                    return boincFolder;
                boincFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "BOINC");
                if (Directory.Exists(boincFolder))
                    return boincFolder;
            }
            catch { }
            return String.Empty;
        }

        private string GetBoincDatafolder()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "BOINC");
            if (Directory.Exists(path))
                return path;
            
            path = @"C:\ProgramData\BOINC";
            if (Directory.Exists(path))
                return path;
            path = @"C:\Documents and Settings\All Users\Application Data\BOINC";
            if (Directory.Exists(path))
                return path;

            return null;
            
        }

        private string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }
    }
}
