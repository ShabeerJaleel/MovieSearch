using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class BayFiles : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            url = SelectItem(elem, ".highlighted-btn").Attributes["href"].Value;
            return url;
        }

        public override string RootUrl
        {
            get { return "http://bayfiles.net"; }
        }

        public override string ID
        {
            get { return "bayfiles.net"; }
        }

        public override string Title
        {
            get { return "BayFiles"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.BayFiles;
            }
        }


        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("bayfiles");
        }
    }
}
