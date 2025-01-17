﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace MovieTube.UpService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            var mutex = new Mutex(true, "movietubeupdate", out createdNew);
            if (!createdNew)
                return;

            Thread.Sleep(2000);
            Execute();
        }

        static void Execute()
        {
            try
            {
                var allProcesses = Process.GetProcessesByName("MovieTube");
                foreach (var process in allProcesses)
                {
                    if (process.MainModule != null && "MovieTube.exe".Equals(process.MainModule.ModuleName, StringComparison.InvariantCultureIgnoreCase))
                    {

                        process.Kill();
                        Thread.Sleep(2000);
                        if (!process.HasExited)
                            Thread.Sleep(5000);
                        if (!process.HasExited)
                            return;
                        break;
                    }
                }

                var files = Directory.GetFiles(Application.StartupPath);
                foreach (var file in files)
                {
                    if (!file.Contains("MovieTube.UpService.exe"))
                    {
                        File.Copy(file, Path.Combine(Directory.GetParent(Application.StartupPath).FullName,
                            Path.GetFileName(file)), true);
                    }
                }

                var app = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "MovieTube.exe");
                Process.Start(app);
            }
            catch { }
        }
    }
}
