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
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace  MovieTube.Client.Scraper
{
    public abstract class VideoScraperBase : ScraperBase
    {
        private static List<VideoScraperBase> scrappers = new List<VideoScraperBase>
        {
            new DivxStage(),
            new Einthusan(),
            new FlashX(),
            new MuchShare(),
            new FireDrive(),
            new DivxStream(),
            new Novamov(),
            new NowVideo(),
            new SockShare(),
            new VideoWeed(),
            new Vidto(),
            new Youtube(),
            new NosVideo(),
            new MovReel(),
            new MovShare(),
            new LoboVideo(),
            new Cloudy(),
            new Veoh(),
            new StageVu(),
            new Played(),
            new Dwn(),
            new HostingBulk(),
            new Movzap(),
            new Vk(),
            new GorillaVid(),
           // new BayFiles(),
            new VodLocker(),
            new TheVideo(),
            new VideoRaj()
        };

        private static List<string> VideoDoesNotExistTexts = new List<string>{
              "file not found",
              "file might have been moved",
              "this movie was taken offline",
              "this file no longer exists",
              "file was deleted",
              "this video has been removed from public access",
              "this file doesn't exist",
              "this+video+has+been+removed",
              "this+video+is+",
              "this+video+has+",
              "conn=rtmpe",
              "this+video+available+in+",
              "this+video+does+not",
              "errorcode=",
              "sorry, we couldn't find the video you",
              "this+video+requires+",
              "this video has been deleted",
              "this file has been removed"

        };
        public string Scrape(string url)
        {
            HtmlNode node = null;
            try
            {
                url = SanitizeUrl(url);
                node = Get(url);
                url = OnScrape(url, node);
                return new Uri(url).AbsoluteUri;
            }
            catch (WebException ex)
            {
                var errorResponse = ex.Response as HttpWebResponse;
                if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ScraperException(ScraperResult.VideoDoesNotExist, "Video does not exists", ex);
                }

                throw new ScraperException(ScraperResult.NetworkError, "Unable to connect to server", ex);
            }
            catch (Exception ex)
            {
                ThrowException(node != null ? node.InnerHtml : ex.Message, ex);
            }
            return url;
        }

        protected virtual string OnScrape(string url, HtmlNode node)
        {
            throw new Exception();
        }

        public static string ScrapeUrl(string url)
        {
            var scraper = GetScraper(url);
            if (scraper == null)
                throw new Exception("No suitable processor found for " + url);
            return scraper.Scrape(url);
        }

        public virtual ScraperResult IsValid(string url)
        {
            try
            {
                Scrape(url);
                return ScraperResult.Success;
            }
            catch (ScraperException ex)
            {
                return ex.Type;
            }
        }

        public static ScraperResult ValidateUrl(string url)
        {
            var scraper = GetScraper(url);
            if (scraper == null)
                throw new Exception("No suitable processor found for " + url);
            return scraper.IsValid(url);
        }

        public static VideoScraperBase GetScraper(string url)
        {
            var scrapper = scrappers.FirstOrDefault(x => x.CanProcess(url));
            if (scrapper == null)
                return null;
            return (VideoScraperBase)Activator.CreateInstance(scrapper.GetType());
        }

        protected void ThrowException(string html, Exception ex)
        {
            if (IsVideoRemoved(html))
                throw new ScraperException(ScraperResult.VideoDoesNotExist, "Video is removed", ex);
            else
                throw new ScraperException(ScraperResult.UnknownError, "Unable to play video", ex);
        }

        protected bool IsVideoRemoved(string html)
        {
            return VideoDoesNotExistTexts.Any(x => html.ToLower().Contains(x));
        }

        public virtual ScraperRank Rank { get { return ScraperRank.Default; } }

        public virtual string SanitizeUrl(string url)
        {
            return url;
        }

        public virtual string GetFlashUrl(string url)
        {
            return url;
        }

        private string scriptResult;
        protected string UnpackScript(string text)
        {
            var th = new Thread(() =>
            {
                var br = new WebBrowser(){ ScriptErrorsSuppressed = true};
                br.DocumentCompleted += browser_DocumentCompleted;
                br.DocumentText = text;
                Application.Run();
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            th.Join();
            return scriptResult;
        }
        
      
        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                scriptResult = br.Document.GetElementById("result").GetAttribute("value");
                Application.ExitThread();  
            }
        }

        protected string TryGetUrl(string url, HtmlNode elem)
        {
            if (url.Contains("embed-"))
            {
                try
                {
                    return new Uri(SubstringBetween(elem.InnerHtml, "file: \"", "\"")).AbsoluteUri;
                }
                catch { }
            }
            return null;
        }

       
    }

    public enum ScraperRank
    {
        Einthusan,
        Youtube,
        FireDrive,
        NowVideo,
        SockShare,
        StageVu,
        Played,
        Vidto,
        GorillaVid,
        Veoh,
        Vk,
        Dwn,
        Novamov,
        NosVideo,
        VideoWeed,
        HostingBulk,
        Cloudy,
        VideoRaj,
        VodLocker,
        BayFiles,
        TheVideo,
        Default = 255,
    }

    public class ScraperException : Exception
    {

        public ScraperException(ScraperResult type, string message, Exception ex)
            : base(message, ex)
        {
            Type = type;
        }

        public ScraperResult Type { get; set; }
    }

    public enum ScraperResult
    {
        Success,
        VideoDoesNotExist,
        NetworkError,
        UnknownError
    }



    //public class XVideoScraper : VideoScraperBase
    //{

    //    public  ScrapedPage ScrapeVideos(SearchParameters sparam = null)
    //    {
            

    //        var page = new ScrapedPage();
    //        var elem = Get(RootUrl);
    //        page.Videos =  ScrapThumbnailVideos(elem);
    //        //get next page
    //        var pages = SelectItems(elem, ".pagination.small.top a").ToList();
    //        foreach (var p in pages)
    //        {
    //            bool selected = p.Attributes["class"] != null && p.Attributes["class"].Value == "sel";
    //            page.Links.Add(new PagingLink
    //            {
    //                Url = selected ? RootUrl : new Uri(new Uri(RootUrl), p.Attributes["href"].Value).AbsoluteUri,
    //                IsSelected = selected,
    //                Text = p.InnerText
    //            });
    //        }

            
    //       var showMore = SelectItem(elem, ".showMore a");
    //       if (showMore != null)
    //       {
    //            page.Links.Add(new PagingLink
    //            {
    //                Url = String.Format("{0}{1}", RootUrl, showMore.Attributes["href"].Value),
    //                Text = showMore.InnerText,
    //            });
    //       }
            
    //        return page;
    //    }

    //    public  ScrapedVideo ScrapeVideoDetails(ScrapedVideo video)
    //    {
    //        var root = Get(video.Url);
    //        var flash = SelectItem(root, "#flash-player-embed");
    //        if (flash == null)
    //            return video;
    //        var flashVars = HttpUtility.UrlDecode(ReadAttribute(SelectItem(root, "#flash-player-embed"), "flashvars"));
    //        var beg = flashVars.IndexOf("flv_url=") + 8;
    //        var end = flashVars.IndexOf("&amp;",beg);
    //        video.PlayUrl = HttpUtility.UrlDecode(flashVars.Substring(beg, end - beg));
    //        beg = root.InnerHtml.IndexOf("<script>videoPageWriteRelated(") + 30;
    //        end = root.InnerHtml.IndexOf(");</script>", beg);
    //        var obj = (JArray)JsonConvert.DeserializeObject(root.InnerHtml.Substring(beg, end - beg));
    //        foreach (var item in obj)
    //        {
    //            foreach (var subItem in item)
    //            {
    //                if (subItem["u"] != null)
    //                {
    //                    video.RelatedVideos.Add(new ScrapedVideo
    //                    {
    //                        Duration = TimeSpan.FromMinutes(Convert.ToInt32(Regex.Replace(subItem["d"].ToString(), "[^0-9.]", ""))),
    //                        ImageUrl = subItem["i"].ToString(),
    //                        Url = RootUrl + subItem["u"].ToString(),
    //                        Title = subItem["t"].ToString(),
    //                    });
    //                }
    //            }
    //        }
    //        if (String.IsNullOrEmpty(video.PlayUrl))
    //            throw new Exception("Unable to get video information");
    //        video.FullyLoaded = true;
    //        return video;
    //    }

    //    public override string RootUrl
    //    {
    //        get{ return "http://www.xvideos.com";}
    //    }

    //    public override string ID
    //    {
    //        get { return "xvideos"; }
    //    }

    //    public override string Title
    //    {
    //        get { return "XVideos";  }
    //    }

    //    private List<ScrapedVideo> ScrapThumbnailVideos(HtmlNode root)
    //    {
    //        var videos = new List<ScrapedVideo>();
    //        var items = SelectItems(root, ".thumbBlock").ToList();
    //        foreach (var item in items)
    //        {
    //            var video = new ScrapedVideo();
    //            var elem = SelectItem(item, ".thumb a");
    //            if (elem == null)
    //            {
    //                elem = SelectItem(item, "script");
    //                elem.InnerHtml = elem.InnerHtml.Replace("castDisplayRandomThumb('", "").Replace("');</script>", "");
    //                elem = SelectItem(item, ".thumb a");
                    
    //            }

    //            if (elem == null)
    //                Debug.Assert(true);
    //            if (elem != null)
    //            {
    //                video.Url = RootUrl + ReadAttribute(elem, "href");
    //                video.ImageUrl = ReadAttribute(SelectItem(item, ".thumb a img"), "src");
    //                video.Title = ReadText(SelectItem(item, ".thumbInside p a"));
    //                var duration = ReadText(SelectItem(item, ".duration"));
    //                var mc = Regex.Matches(duration, @"\d+");
    //                var hour = duration.Contains("h") ? TimeSpan.FromHours(Convert.ToInt32(mc[0].Value)) : new TimeSpan();
    //                var min = duration.Contains("min") ? TimeSpan.FromMinutes(Convert.ToInt32(
    //                    duration.Contains("h") ? mc[1].Value : mc[0].Value)) : new TimeSpan();
    //                var sec = duration.Contains("sec") ? TimeSpan.FromSeconds(Convert.ToInt32(mc[mc.Count-1].Value)) : new TimeSpan();


    //                video.Duration = hour + min + sec;
    //                videos.Add(video);
    //            }
    //        }
    //        return videos;
    //    }

    //    public override string Scrape(string url)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
