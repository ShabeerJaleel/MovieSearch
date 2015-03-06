using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MovieFinder.Client.Properties;
using System.Reflection;

namespace MovieFinder.Client
{

    static class Program
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, UInt32 arg);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNew;
            var mutex = new Mutex(true, "moviefinder", out createdNew);
            if (!createdNew)
            {
                //var thisProcess = Process.GetCurrentProcess();
                //var allProcesses = Process.GetProcessesByName(thisProcess.ProcessName);
                //foreach (Process p in allProcesses)
                //{
                //    if (p != null && (p.Id != thisProcess.Id) &&
                //        p.MainModule != null &&
                //        (p.MainModule.FileName == thisProcess.MainModule.FileName))
                //    {
                //        IntPtr hWnd = p.MainWindowHandle;
                //        ShowWindow(hWnd, 3);
                //        SetForegroundWindow(hWnd);
                //    }
                //}

                MessageBox.Show("An instance of Movie Finder is already running. Open it from the System Tray on the bottom right side of the screen", "Movie Finder");

                return;
            }
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            if (version != Constants.LastRunVersion)
            {
                MessageBox.Show("Movie Finder is updated to the latest version", "Movie Finder");
                Settings.Default.LastRunVersion = version.ToString();
                Settings.Default.Save();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static int MovieDBVersion
        {
            get;
            set;
        }

        
    }
}
