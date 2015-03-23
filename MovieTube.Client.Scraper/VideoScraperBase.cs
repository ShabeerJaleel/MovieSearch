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



}
