using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Played : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            return new Uri(SubstringBetween(elem.InnerHtml, "file: \"", "\"")).AbsoluteUri;
        }

      
        public override string RootUrl
        {
            get { return "http://played.to/"; }
        }

        public override string ID
        {
            get { return "played.to"; }
        }

        public override string Title
        {
            get { return "Played"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Played;
            }
        }

        public override string SanitizeUrl(string url)
        {
            if (!url.ToLower().Contains("http://played.to/"))
                throw new Exception("Unknown format");
           
            return url;
        }

        public override bool CanProcess(string url)
        {
            return base.CanProcess(url) || url.ToLower().Contains("played.");
        }
    }
}
