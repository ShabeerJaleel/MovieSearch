using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;
namespace MovieTube.Client.Scraper
{
    public class Movzap : VideoScraperBase
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
            get { return "http://movzap.com/"; }
        }

        public override string ID
        {
            get { return "movzap.com"; }
        }

        public override string Title
        {
            get { return "MovZap"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.HostingBulk;
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
