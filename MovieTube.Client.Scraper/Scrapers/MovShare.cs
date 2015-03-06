using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class MovShare : VideoScraperBase
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

            //start = elem.InnerHtml.IndexOf(".cid2=\"") + 7;
            //end = elem.InnerHtml.IndexOf("\"", start);
            //var cid2 = elem.InnerHtml.Substring(start, end - start);

            url = String.Format("{4}api/player.api.php?cid={0}&key={1}&pass=undefined&user=undefined&file={2}&cid3={3}&numOfErrors=0",
                                cid, fileKey, file, "", RootUrl);
            elem = Get(new Uri(url).AbsoluteUri);
            url = elem.InnerHtml.Substring(4, elem.InnerHtml.IndexOf("&") - 4);
            return new Uri(url).AbsoluteUri;
        }

       

        public override string RootUrl
        {
            get { return "http://www.movshare.net/"; }
        }

        public override string ID
        {
            get { return "movshare.net"; }
        }

        public override string Title
        {
            get { return "MovShare"; }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains(".movshare.");
        }

        public override string SanitizeUrl(string url)
        {
            url = url.Replace("http://embed.movshare.ch", "http://embed.movshare.net");
            if (!url.ToLower().Contains("http://www.movshare.net/video/") &&
                !url.ToLower().Contains("http://embed.movshare.net/embed.php?") &&
                !url.ToLower().Contains("http://www.movshare.net/embed/"))
                throw new Exception("Unknown format");

            return url;
        }

        public override string GetFlashUrl(string url)
        {
            //http://embed.movshare.net/embed.php?v=8628be306c70b

            //http://www.movshare.net/video/c9365242e8a26

            if (url.Contains("http://embed.movshare.net/"))
            {
                var id = SubstringBetween(url, "v=");
                url = String.Format("http://www.movshare.net/video/{0}", id);
            }

            return url;
        }
    }
}
