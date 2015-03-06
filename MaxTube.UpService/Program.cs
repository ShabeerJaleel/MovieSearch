using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace MaxTube.UpService
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
            var mutex = new Mutex(true, "bluetubeupdate", out createdNew);
            if (!createdNew)
                return;

            Thread.Sleep(2000);
            Execute();
        }

        static void Execute()
        {
            try
            {
                var allProcesses = Process.GetProcessesByName("BlueTube");
                foreach (var process in allProcesses)
                {
                    if (process.MainModule != null && "BlueTube.exe".Equals(process.MainModule.ModuleName, StringComparison.InvariantCultureIgnoreCase))
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
                    if (!file.Contains("BlueTube.UpService.exe"))
                    {
                        File.Copy(file, Path.Combine(Directory.GetParent(Application.StartupPath).FullName,
                            Path.GetFileName(file)), true);
                    }
                }

                var app = Path.Combine(Directory.GetParent(Application.StartupPath).FullName, "BlueTube.exe");
                Process.Start(app);
            }
            catch { }
        }
    }
}
