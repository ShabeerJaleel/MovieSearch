using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Youtube : VideoScraperBase
    {
       //private static readonly YouTubeRequestSettings settings = new YouTubeRequestSettings("test", null) { AutoPaging = true, Maximum = 100 };

        protected override string OnScrape(string url, HtmlNode elem)
        {
            try
            {
                var input = SubstringBetween(elem.InnerHtml, "url_encoded_fmt_stream_map=", "&");
                if(String.IsNullOrEmpty(input.Trim()))
                    input = SubstringBetween(elem.InnerHtml, "url_encoded_fmt_stream_map=");
                var expression = HttpUtility.UrlDecode(input);
                var tokens = expression.Split(',').FirstOrDefault(x => x.Contains("quality=medium")).Split('&');
                var newUrl = tokens.FirstOrDefault(x => x.StartsWith("url"));
                if (!String.IsNullOrEmpty(newUrl))
                {
                    newUrl = newUrl.Split('=')[1];
                    return HttpUtility.UrlDecode(newUrl);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            throw new Exception("File Not Found");

            //var version = HttpUtility.ParseQueryString(new Uri(url).Query).Get("video_id");
            //newUrl = String.Format("http://www.youtube.com/watch?v={0}", version);

            //elem = Get(url);
            //var start = elem.InnerHtml.IndexOf("url_encoded_fmt_stream_map");
            //var end = elem.InnerHtml.IndexOf("</script>", start);
            //var script = elem.InnerHtml.Substring(start, end - start);

            //start = script.IndexOf("url=") + 4;
            //end = script.IndexOf("0026", start) - 2;
            //url = script.Substring(start, end - start);
            //return System.Web.HttpUtility.UrlDecode(url);


            //elem = Get(newUrl);
            //var script = SubstringBetween(elem.InnerHtml, "url_encoded_fmt_stream_map", "</script>");
            //newUrl = SubstringBetween(script, "url=", "0026");
            //return System.Web.HttpUtility.UrlDecode(newUrl);
        }

     

        //private string GetVideoById(string id)
        //{
        //    var request = new YouTubeRequest(settings);
        //    var url = new Uri(YouTubeQuery.CreateVideoUri(id));
        //    Feed<Video> videoFeed = request.Get<Video>(url);
        //     var x = videoFeed.Entries.First();

        //    return null;
        //}

        public override string SanitizeUrl(string url)
        {
            if (url.Contains("http://www.youtube.com/get_video_info"))
                return url;
            var version = HttpUtility.ParseQueryString(new Uri(url).Query).Get("v");
            if (version == null && url.Contains("embed"))
                version = SubstringBetween(url, "embed/");
            
            if(version == null && url.Contains("/video/"))
                version = SubstringBetween(url, "video/");
            if (version == null && url.Contains("/v/"))
            {
                version = SubstringBetween(url, "/v/", "?");
                if(version == null)
                    version = SubstringBetween(url, "/v/", "&");
                if(version == null)
                    version = SubstringBetween(url, "/v/");
            }

            if (version == null && url.Contains("youtu.be/"))
                version = SubstringBetween(url, ".be/");
            
            var start = version.IndexOf("?");
            if (start != -1)
                version = version.Substring(0, start);

            url = String.Format("http://www.youtube.com/get_video_info?video_id={0}&el=detailpage", version);

            return url;
        }

      

        public override string RootUrl
        {
            get { return "http://www.youtube.com/"; }
        }

        public override string ID
        {
            get { return "youtube.com"; }
        }

        public override string Title
        {
            get { return "YouTube"; }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("youtube.com") ||
                url.ToLower().Contains("youtu.be");
                
        }

        public override string GetFlashUrl(string url)
        {
            //http://www.youtube.com/watch?v=feIciElwsWM
            if (url.Contains("get_video_info"))
            {
                var id = SubstringBetween(url, "video_id=", "&");
                url = String.Format("http://www.youtube.com/watch?v={0}", id);
            }
            else if (url.Contains("/embed"))
            {
                var id = SubstringBetween(url, "video_id=", "&");
                url = url.Replace("embed/", "watch?v=");
            }

            return url;
        }


        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Youtube;
            }
        }
    }
}
