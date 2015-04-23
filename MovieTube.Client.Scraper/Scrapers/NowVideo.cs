using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class NowVideo : VideoScraperBase
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
            url = new Uri(url).AbsoluteUri;

            return url;
        }

       

        public override string RootUrl
        {
            get { return "http://www.nowvideo.sx/"; }
        }

        public override string ID
        {
            get { return ScrapperId.NowVideo; }
        }

        public override string Title
        {
            get { return "NowVideo"; }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("nowvideo.eu") || url.ToLower().Contains("nowvideo.ch") ||
                url.ToLower().Contains("nowvideo.sx");
        }

        public override string SanitizeUrl(string url)
        {
            url = url.Replace(".ch/", ".sx/").Replace(".eu/",".sx/");
            if(url.Contains("embed"))
            {
               var start = url.IndexOf("v=") + 2;
               var end = url.IndexOf("&", start);
               if (end == -1)
                    end = url.Length;
                url = String.Format("http://www.nowvideo.sx/video/{0}", url.Substring(start, end - start));
            }
            if (!url.Contains("http://www.nowvideo.sx/video/"))
                throw new Exception("Unknown format");
            return url;
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.NowVideo;
            }
        }
    }
}
