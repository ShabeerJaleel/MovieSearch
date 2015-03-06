using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Net;
using ComponentFactory.Krypton.Toolkit;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using MovieTube.Viewer.Data;
using System.IO;

namespace MovieTube.Viewer
{
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew;
            var mutex = new Mutex(true, "MovieTubeViewer", out createdNew);
            if (!createdNew)
            {
                NativeMethods.PostMessage((IntPtr)NativeMethods.HWND_BROADCAST,
                              NativeMethods.WM_SHOWME, IntPtr.Zero, IntPtr.Zero);
                //KryptonMessageBox.Show("Another instance of MovieTube is  running. Open it from the System Tray on the bottom right side of the screen", Constants.AppTitle);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //VlcDeployment deployment = VlcDeployment.Default;
            //if (!deployment.CheckVlcLibraryExistence(true, false))
            //    deployment.Install(true, true, false, false);

            ServicePointManager.DefaultConnectionLimit = int.MaxValue;
            ServicePointManager.MaxServicePoints = int.MaxValue;
            ServicePointManager.MaxServicePointIdleTime = 0;

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            try
            {
                DownloadDatabase();
                Application.Run(new MainForm());
            }
            catch(Exception ex){
                KryptonMessageBox.Show(ex.Message);
            }

          
        }
        
        private static void DownloadDatabase()
        {
            if (!File.Exists(Constants.MovieDatabaseFilePath))
            {
                var dForm = new DownloadForm();
                dForm.Download();
            }
        }

      


        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            LogManager.LogError(e.ExceptionObject.ToString(),e.ExceptionObject as Exception);
            Debug.WriteLine("Unhandled Exception: " + e.ExceptionObject.ToString());
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogManager.LogError("Thread Exception", e.Exception);
        }

        public static void SetBusy()
        {
            Application.UseWaitCursor = true;
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void SetIdle()
        {
            Application.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
        }
     
    }
}
