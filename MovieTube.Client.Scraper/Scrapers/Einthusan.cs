using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Xml;
using System.Web;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Einthusan : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var start = elem.InnerText.IndexOf("'file': '") + 9;
            var end = elem.InnerText.IndexOf("'", start + 1);
            url = elem.InnerText.Substring(start, end - start);
            return url; 
        }

        public override string RootUrl
        {
            get { return "http://www.einthusan.com"; }
        }

        public override string ID
        {
            get { return "einthusan.com"; }
        }

        public override string Title
        {
            get { return "Einthusan"; }
        }

        public override bool IsWebSupported
        {
            get
            {
                return false;
            }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Einthusan;
            }
        }
    }
}
