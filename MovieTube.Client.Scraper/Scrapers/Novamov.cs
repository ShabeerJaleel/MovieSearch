using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Novamov : VideoScraperBase
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

            url = String.Format("{5}api/player.api.php?cid={0}&key={1}&pass=undefined&user=undefined&file={2}&cid3={3}&numOfErrors=0&cid2={4}",
                                cid, fileKey, file, "", cid2, RootUrl);
            elem = Get(new Uri(url).AbsoluteUri);
            url = elem.InnerHtml.Substring(4, elem.InnerHtml.IndexOf("&") - 4);
            return new Uri(url).AbsoluteUri;
        }

        public override string RootUrl
        {
            get { return "http://www.novamov.com/"; }
        }

        public override string ID
        {
            get { return ScrapperId.Novamov; }
        }

        public override string Title
        {
            get { return "Novamov"; }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("embed.novamov.com");
        }

        public override string SanitizeUrl(string url)
        {

            if (url.Contains("embed.novamov.com"))
            {
                var u = new Uri(url);
                url = String.Format("http://www.novamov.com/video/{0}", HttpUtility.ParseQueryString(u.Query).Get("v"));
            }
            else if (url.Contains("embed.php"))
            {
                url = String.Format("http://www.novamov.com/video/{0}", SubstringBetween(url,".php?v="));
            }
            return url;
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Novamov;
            }
        }

        public override string GetFlashUrl(string url)
        {
            //http://embed.novamov.com/embed.php?v=9781cba507d6e
            //http://www.novamov.com/video/p887x8ai5og7t
            if (url.Contains("http://embed.novamov.com"))
            {
                var id = SubstringBetween(url, "v=");
                url = string.Format("http://www.novamov.com/video/{0}", id);
            }
            return base.GetFlashUrl(url);
        }
    }
}
