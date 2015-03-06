using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Dwn : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            url = SubstringBetween(elem.InnerHtml, "un=\"", "\"");
            if (!url.StartsWith("http"))
                url = "http://" + url;
            return new Uri(url).AbsoluteUri;
        }

     
        public override string RootUrl
        {
            get { return "http://dwn.so/"; }
        }

        public override string ID
        {
            get { return "dwn.so"; }
        }

        public override string Title
        {
            get { return "Dwn"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Dwn;
            }
        }

        public override string SanitizeUrl(string url)
        {
            if (url.ToLower().Contains("dwn.so/xml/videolink"))
                return url;
            if (!url.ToLower().Contains("dwn.so/player/embed.php?v="))
                throw new Exception("Unknown format");
            var id =  SubstringBetween(url, "v=","&");
            url = String.Format("http://dwn.so/xml/videolink.php?v={0}", id);
            return url;
        }

        public override string GetFlashUrl(string url)
        {
            //http://dwn.so/v/DS1C4741A5
            if (url.ToLower().Contains("dwn.so/xml/"))
            {
                var id = SubstringBetween(url, "v=");
                url = String.Format("http://dwn.so/v/{0}", id);
            }
            return url;
        }

        //public override bool CanProcess(string url)
        //{
        //    if (base.CanProcess(url))
        //        return true;
        //    return url.ToLower().Contains("veoh");
        //}
    }
}
