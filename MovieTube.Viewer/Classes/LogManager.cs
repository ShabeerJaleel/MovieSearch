using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MovieTube.Viewer
{
    public static class LogManager
    {
        private static string ErrorFile = "error.txt";
        private static string LogFile = "log.txt";

        static LogManager()
        {

        }

        public static  void Log(string message)
        {
            if(AppSettings.Log)
                File.AppendAllText(LogFile,  DateTime.Now.ToString() + ": " + message  +Environment.NewLine);
            
        }

        public static void LogError(string message, Exception ex)
        {
            if (AppSettings.Log)
                File.AppendAllText(ErrorFile, DateTime.Now.ToString() + ": " + message + ": Exception => " + ex != null ? ex.ToString() : String.Empty + Environment.NewLine);
        }
    }
}
