﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class HostingBulk : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var tokens = SubstringBetween(elem.InnerHtml, ",'http", "'").Split('|');
            url = String.Format("http://{0}.{1}.{2}.{3}/d/{4}/{5}.{6}?start=0",
                tokens[6], tokens[5], tokens[4], tokens[3], tokens[12], tokens[11], tokens[10]);
            return new Uri(url).AbsoluteUri;
        }


        public override string RootUrl
        {
            get { return "http://hostingbulk.com/"; }
        }

        public override string ID
        {
            get { return ScrapperId.HostingBulk; }
        }

        public override string Title
        {
            get { return "HostingBulk"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.HostingBulk;
            }
        }

        public override string GetFlashUrl(string url)
        {
            //http://hostingbulk.com/88a22d6520566
            if (url.ToLower().Contains("embed-"))
            {
                url = String.Format("http://hostingbulk.com/{0}.html",
                    SubstringBetween(url, "embed-", "-"));

            }

            return url;
        }
    }
}
