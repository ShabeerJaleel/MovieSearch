using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace BlueTube.Viewer
{
    public static class AppSettings
    {
        public static bool Log
        {
            get
            {
                return Convert.ToBoolean(ConfigurationManager.AppSettings["Log"]);
            }
        }
    }
}
