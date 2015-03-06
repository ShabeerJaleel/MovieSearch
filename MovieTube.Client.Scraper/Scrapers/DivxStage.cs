using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;


namespace MovieTube.Client.Scraper
{
    public class DivxStage : VideoScraperBase
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
                start = elem.InnerHtml.IndexOf("fkz=\"");
                if (start > -1)
                    start += 5;
                else
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
            get { return "http://www.divxstage.eu/"; }
        }

        public override string ID
        {
            get { return "divxstage.eu"; }
        }

        public override string Title
        {
            get { return "DivxStage"; }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains(".divxstage.");
        }

        public override string SanitizeUrl(string url)
        {
            if (!url.ToLower().Contains("http://embed.divxstage.eu/") &&
                !url.ToLower().Contains("http://www.divxstage.eu/video/") &&
                !url.ToLower().Contains("http://www.divxstage.to/video/") &&
                !url.ToLower().Contains("http://www.divxstage.eu/file/") &&
                 !url.ToLower().Contains("http://www.divxstage.to/file/"))
                throw new Exception("Unknown format");

            return url;
        }

       
    }
}
