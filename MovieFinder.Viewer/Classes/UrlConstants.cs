using System;
using System.Collections.Generic;
using System.Text;

namespace BlueTube.Viewer
{
    public static class UrlConstants
    {
        public static string WebsiteUrl
        {
            get { return "http://www.bluetube.me"; }
        }

        public static string SuggestionUrl
        {
            get { return WebsiteUrl + "/api/suggestion"; }
        }

        public static string AppVersionCheckUrl
        {
            get { return WebsiteUrl + "/api/appversion"; }
        }

        public static string AppDownloadUrl
        {
            get { return WebsiteUrl + "/api/downloadapp"; }
        }

        public static string AdUrl
        {
            get { return WebsiteUrl + "/ad"; }
        }

        public static string ShowAdsUrl
        {
            get { return WebsiteUrl + "/ad/showads"; }
        }

        public static string HelpUrl
        {
            get { return WebsiteUrl + "/faq"; }
        }

        public static string ReportIssueUrl
        {
            get { return WebsiteUrl + "/suggestion"; }
        }


    }
}
