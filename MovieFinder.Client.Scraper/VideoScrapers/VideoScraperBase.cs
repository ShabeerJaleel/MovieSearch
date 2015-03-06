using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace  Client.Scraper
{
    public abstract class VideoScraperBase : ScraperBase
    {
        public abstract ScrapedPage ScrapeVideos(SearchParameters sparam);
        public abstract ScrapedVideo ScrapeVideoDetails(ScrapedVideo video);
    }

    public class XVideoScraper : VideoScraperBase
    {

        public override ScrapedPage ScrapeVideos(SearchParameters sparam = null)
        {
            var rootUrl = RootUrl;
            if (sparam != null )
            {
                if (!String.IsNullOrEmpty(sparam.Url))
                    rootUrl = sparam.Url;
                var ub = new Uri(rootUrl);
                if (!String.IsNullOrEmpty(sparam.Query))
                {
                    ub = ub.AddQuery("k", sparam.Query);
                    ub = ub.AddQuery("sort", sparam.SortBy == VideoSortby.Relevance ? "relevance" 
                        : (sparam.SortBy == VideoSortby.Rating ? "rating" : "uploaddate"));
                    ub = ub.AddQuery("durf", sparam.Duration == VideoDuration.All ? "allduration" : (sparam.Duration == VideoDuration.Long ? "10min_more" :
                        (sparam.Duration == VideoDuration.Short ? "1-3min" : "3-10min")));
                    ub = ub.AddQuery("datef", sparam.Period == VideoPeriod.Anytime ? "all" : (sparam.Period == VideoPeriod.Today ? "today" : 
                        ((sparam.Period == VideoPeriod.ThisWeek ? "week" : "month"))));
                    rootUrl = ub.AbsoluteUri;
                }
            }

            var page = new ScrapedPage();
            var elem = GotoUrl(rootUrl);
            page.Videos =  ScrapThumbnailVideos(elem);
            //get next page
            var pages = SelectItems(elem, ".pagination.small.top a").ToList();
            foreach (var p in pages)
            {
                bool selected = p.Attributes["class"] != null && p.Attributes["class"].Value == "sel";
                page.Links.Add(new PagingLink
                {
                    Url = selected? rootUrl : new Uri(new Uri(RootUrl), p.Attributes["href"].Value).AbsoluteUri,
                    IsSelected = selected,
                    Text = p.InnerText
                });
            }

            
           var showMore = SelectItem(elem, ".showMore a");
           if (showMore != null)
           {
                page.Links.Add(new PagingLink
                {
                    Url = String.Format("{0}{1}", RootUrl, showMore.Attributes["href"].Value),
                    Text = showMore.InnerText,
                });
           }
            
            return page;
        }

        public override ScrapedVideo ScrapeVideoDetails(ScrapedVideo video)
        {
            var root = GotoUrl(video.Url);
            var flash = SelectItem(root, "#flash-player-embed");
            if (flash == null)
                return video;
            var flashVars = HttpUtility.UrlDecode(ReadAttribute(SelectItem(root, "#flash-player-embed"), "flashvars"));
            var beg = flashVars.IndexOf("flv_url=") + 8;
            var end = flashVars.IndexOf("&amp;",beg);
            video.PlayUrl = HttpUtility.UrlDecode(flashVars.Substring(beg, end - beg));
            beg = root.InnerHtml.IndexOf("<script>videoPageWriteRelated(") + 30;
            end = root.InnerHtml.IndexOf(");</script>", beg);
            var obj = (JArray)JsonConvert.DeserializeObject(root.InnerHtml.Substring(beg, end - beg));
            foreach (var item in obj)
            {
                foreach (var subItem in item)
                {
                    if (subItem["u"] != null)
                    {
                        video.RelatedVideos.Add(new ScrapedVideo
                        {
                            Duration = TimeSpan.FromMinutes(Convert.ToInt32(Regex.Replace(subItem["d"].ToString(), "[^0-9.]", ""))),
                            ImageUrl = subItem["i"].ToString(),
                            Url = RootUrl + subItem["u"].ToString(),
                            Title = subItem["t"].ToString(),
                        });
                    }
                }
            }
            if (String.IsNullOrEmpty(video.PlayUrl))
                throw new Exception("Unable to get video information");
            video.FullyLoaded = true;
            return video;
        }

        public override string RootUrl
        {
            get{ return "http://www.xvideos.com";}
        }

        public override string ID
        {
            get { return "xvideos"; }
        }

        public override string Title
        {
            get { return "XVideos";  }
        }

        private List<ScrapedVideo> ScrapThumbnailVideos(HtmlNode root)
        {
            var videos = new List<ScrapedVideo>();
            var items = SelectItems(root, ".thumbBlock").ToList();
            foreach (var item in items)
            {
                var video = new ScrapedVideo();
                var elem = SelectItem(item, ".thumb a");
                if (elem == null)
                {
                    elem = SelectItem(item, "script");
                    elem.InnerHtml = elem.InnerHtml.Replace("castDisplayRandomThumb('", "").Replace("');</script>", "");
                    elem = SelectItem(item, ".thumb a");
                    
                }

                if (elem == null)
                    Debug.Assert(true);
                if (elem != null)
                {
                    video.Url = RootUrl + ReadAttribute(elem, "href");
                    video.ImageUrl = ReadAttribute(SelectItem(item, ".thumb a img"), "src");
                    video.Title = ReadText(SelectItem(item, ".thumbInside p a"));
                    var duration = ReadText(SelectItem(item, ".duration"));
                    var mc = Regex.Matches(duration, @"\d+");
                    var hour = duration.Contains("h") ? TimeSpan.FromHours(Convert.ToInt32(mc[0].Value)) : new TimeSpan();
                    var min = duration.Contains("min") ? TimeSpan.FromMinutes(Convert.ToInt32(
                        duration.Contains("h") ? mc[1].Value : mc[0].Value)) : new TimeSpan();
                    var sec = duration.Contains("sec") ? TimeSpan.FromSeconds(Convert.ToInt32(mc[mc.Count-1].Value)) : new TimeSpan();


                    video.Duration = hour + min + sec;
                    videos.Add(video);
                }
            }
            return videos;
        }
    }
}
