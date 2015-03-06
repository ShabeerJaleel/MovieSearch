using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Cloudy : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var start = elem.InnerHtml.IndexOf("flashvars.file=\"") + 16;
            var end = elem.InnerHtml.IndexOf("\"", start);
            var file = elem.InnerHtml.Substring(start, end - start);
            start = elem.InnerHtml.IndexOf("fkzd=\"");
            if (start > -1)
                start += 6;
            else
            {
                start = elem.InnerHtml.IndexOf(".filekey=\"") + 10;
            }
            end = elem.InnerHtml.IndexOf("\"", start);
            var fileKey = elem.InnerHtml.Substring(start, end - start);

            start = elem.InnerHtml.IndexOf(".cid=\"") + 6;
            end = elem.InnerHtml.IndexOf("\"", start);
            var cid = elem.InnerHtml.Substring(start, end - start);

            start = elem.InnerHtml.IndexOf(".cid2=\"") + 7;
            end = elem.InnerHtml.IndexOf("\"", start);
            var cid2 = elem.InnerHtml.Substring(start, end - start);

            url = String.Format("{5}api/player.api.php?cid=undefined&key={1}&pass=undefined&user=undefined&file={2}&cid3=cloudy.ec&numOfErrors=0&cid2=undefined",
                                cid, fileKey, file, "", cid2, RootUrl);
            elem = Get(new Uri(url).AbsoluteUri);
            url = elem.InnerHtml.Substring(4, elem.InnerHtml.IndexOf("&") - 4);
            return new Uri(HttpUtility.UrlDecode(url)).AbsoluteUri;
        }

      

        public override string RootUrl
        {
            get { return "http://www.cloudy.ec/"; }
        }

        public override string ID
        {
            get { return "cloudy.ec"; }
        }

        public override string Title
        {
            get { return "Cloudy"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Cloudy;
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
                url = String.Format("http://www.cloudy.ec/embed.php?id={0}", url.Substring(start, end - start));
            }

            return url;
        }

        public override string GetFlashUrl(string url)
        {
            //http://www.cloudy.ec/v/88a22d6520566
            if (url.ToLower().Contains("embed."))
            {
                var start = url.IndexOf("id=") + 3;
                var end = url.IndexOf("&", start);
                if (end == -1)
                    end = url.Length;
                url = String.Format("http://www.cloudy.ec/v/{0}", url.Substring(start, end - start));
            }

            return url;
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains(".cloudy.");
        }
    }
}
