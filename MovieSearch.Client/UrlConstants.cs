using System;
using System.Collections.Generic;
using System.Text;

namespace MovieFinder.Client
{
    public static class UrlConstants
    {
        public static string WebsiteUrl
        {
            get { return "http://www.filmkhoj.com"; }
        }

        public static string SuggestionUrl
        {
            get { return WebsiteUrl + "/api/suggestion"; }
        }

        public static string MovieDatabaseVersionCheckUrl
        {
            get { return WebsiteUrl + "/api/dbversion"; }
        }

        public static string MovieDatabaseDownloadUrl
        {
            get { return WebsiteUrl + "/api/downloaddb"; }
        }

        public static string AppVersionCheckUrl
        {
            get { return WebsiteUrl + "/api/appversion"; }
        }

        public static string AppDownloadUrl
        {
            get { return WebsiteUrl + "/api/downloadapp"; }
        }


    }
}
