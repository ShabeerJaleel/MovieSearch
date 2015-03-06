using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTube.Viewer
{
    public static class UrlConstants
    {
        public static string WebsiteUrl
        {
            get { return "http://www.filmkhoj.com"; }
           // get { return "  http://localhost:6974"; }
          
        }

        public static string SuggestionUrl
        {
            get { return WebsiteUrl + "/api/suggestion"; }
        }

        public static string AppVersionCheckUrl
        {
            get { return WebsiteUrl + "/api/appversion"; }
        }

        public static string AppUpdateDownloadUrl
        {
            get { return WebsiteUrl + "/api/downloadapp"; }
        }

        public static string AppDownloadUrl
        {
            get { return WebsiteUrl + "/home/Download"; }
        }

        public static string AdUrl
        {
            get { return WebsiteUrl + "/ad"; }
        }

        public static string ShowAdsUrl
        {
            get { return WebsiteUrl + "/ad/showads"; }
        }

        public static string ServerMessageUrl
        {
            get { return WebsiteUrl + "/home/message"; }
        }

        public static string HelpUrl
        {
            get { return WebsiteUrl + "/faq"; }
        }

        public static string ReportIssueUrl
        {
            get { return WebsiteUrl + "/suggestion"; }
        }

        public static string MovieDatabaseVersionCheckUrl
        {
            get { return WebsiteUrl + "/api/dbversion"; }
        }

        public static string MovieDatabaseDownloadUrl
        {
            get { return WebsiteUrl + "/api/downloaddb"; }
        }

        public static string LinkRemovedUrl
        {
            get { return WebsiteUrl + "/api/removelink"; }
        }
    }
}
