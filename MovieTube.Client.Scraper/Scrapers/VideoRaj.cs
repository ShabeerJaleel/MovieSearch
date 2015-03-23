using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class VideoRaj : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var start = elem.InnerHtml.IndexOf("flashvars.file=\"") + 16;
            var end = elem.InnerHtml.IndexOf("\"", start);
            var file = elem.InnerHtml.Substring(start, end - start);
            start = elem.InnerHtml.IndexOf(".filekey=\"") + 10;
            end = elem.InnerHtml.IndexOf("\"", start);
            var fileKey = elem.InnerHtml.Substring(start, end - start);


            url = String.Format("{0}/api/player.api.php?cid=undefined&key={1}&pass=undefined&user=undefined&file={2}&cid3=undefined&numOfErrors=0&cid2=undefined",
                                RootUrl, fileKey, file);
            elem = Get(new Uri(url).AbsoluteUri);
            url = elem.InnerHtml.Substring(4, elem.InnerHtml.IndexOf("&") - 4);
            return new Uri(HttpUtility.UrlDecode(url)).AbsoluteUri;
        }

        public override bool IsWebSupported
        {
            get
            {
                return false;
            }
        }

        public override string RootUrl
        {
            get { return "http://www.videoraj.ch"; }
        }

        public override string ID
        {
            get { return "videoraj.ch"; }
        }

        public override string Title
        {
            get { return "VideoRaj"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.VideoRaj;
            }
        }

        public override string SanitizeUrl(string url)
        {
            if (!url.ToLower().Contains("embed."))
            {
                var start = url.IndexOf("v/") + 2;
                var end = url.IndexOf("&", start);
                if (end == -1)
                    end = url.Length;
                url = String.Format("http://www.videoraj.ch/embed.php?id={0}", url.Substring(start, end - start));
            }

            return url;
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains(".videoraj.");
        }
    }
}
