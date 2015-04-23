using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Vk : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            url = new Uri(SubstringBetween(elem.InnerHtml, "url360=", "&")).AbsoluteUri;
            return url;
        }

      

        public override string RootUrl
        {
            get { return "http://vk.com/"; }
        }

        public override string ID
        {
            get { return ScrapperId.Vk; }
        }

        public override string Title
        {
            get { return "Vk"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Vk;
            }
        }

        //public override string SanitizeUrl(string url)
        //{
        //    if (!url.ToLower().Contains("http://www.veoh.com") && !url.Contains("/watch/") &&
        //        !url.Contains("http://veoh.com/videos/"))
        //        throw new Exception("Unknown format");
        //    var id =  SubstringBetween(url, url.ToLower().Contains("/videos/") ? "videos/" : "watch/");
        //    url = String.Format("http://www.veoh.com/watch/{0}", id);

        //    return url;
        //}

        //public override bool CanProcess(string url)
        //{
        //    if (base.CanProcess(url))
        //        return true;
        //    return url.ToLower().Contains("veoh");
        //}
    }
}
