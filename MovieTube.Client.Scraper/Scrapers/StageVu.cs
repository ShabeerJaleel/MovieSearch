using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class StageVu : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            url = SelectItem(elem, "embed").Attributes["src"].Value;
            return new Uri(url).AbsoluteUri;
        }

      

        public override string RootUrl
        {
            get { return "http://www.stagevu.com/"; }
        }

        public override string ID
        {
            get { return "stagevu.com"; }
        }

        public override string Title
        {
            get { return "StageVu"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.StageVu;
            }
        }

        public override string SanitizeUrl(string url)
        {
            if (!url.ToLower().Contains("http://stagevu.com/video/"))
                throw new Exception("Unknown format");

            return url;
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("stagevu.");
        }
    }
}
