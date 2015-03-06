using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Xml;
using System.Web;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class FireDrive : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            try
            {
                var item = SelectItem(elem, "#top_external_download");
                url = item.Attributes["href"].Value;

                return url;
            }
            catch { }

            var hash = SelectItem(elem, "[name=confirm]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("confirm", hash);
            elem = Post(url, data);
            {
                var item = SelectItem(elem, "#fd_vid_btm_download");
                url = item.Attributes["href"].Value;
            }

            return url;
        }

        public override string RootUrl
        {
            get { return "http://www.firedrive.com"; }
        }

        public override string ID
        {
            get { return "firedrive.com"; }
        }

        public override string Title
        {
            get { return "FireDrive"; }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("putlocker.com") ||
                url.ToLower().Contains("firedrive.com");
        }
        public override string SanitizeUrl(string url)
        {
            url = url.Replace("https", "http");
            url = url.Replace("www.putlocker.com", "www.firedrive.com");

            if (url.Contains("embed/"))
            {
                url = String.Format("http://www.firedrive.com/file/{0}", SubstringBetween(url, "embed/"));
            }
            return url;
        }

        public override ScraperRank Rank
        {
            get
            {
                return  ScraperRank.FireDrive;
            }
        }
    }
}
