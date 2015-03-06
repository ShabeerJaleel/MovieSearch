using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using System.Xml;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class SockShare : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var hash = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("hash", hash);
            data.Add("confirm", "Continue as Free User");
            
            elem = Post(url, data);
            return RootUrl + SelectItem(elem, ".download_file_link").Attributes["href"].Value;

        }

       
        public override string RootUrl
        {
            get { return "http://www.sockshare.com"; }
        }

        public override string ID
        {
            get { return "sockshare.com"; }
        }

        public override string Title
        {
            get { return "SockShare"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.SockShare;
            }
        }

        public override string SanitizeUrl(string url)
        {
            url = url.Replace("/embed/", "/file/");
            return url;
        }
    }
}
