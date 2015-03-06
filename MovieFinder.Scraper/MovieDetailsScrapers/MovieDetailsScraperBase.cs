using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieTube.Client.Scraper;
using MovieFinder.Data;

namespace MovieFinder.Scraper
{
    public abstract class MovieDetailsScraperBase : ScraperBase
    {

        protected bool stop;
        public void Stop()
        {
            this.stop = true;
        }

        public abstract List<ScrapedMovie> ScrapeMovies(List<string> skipUrls, List<int> years = null);
        public static readonly List<ScraperBase> Scrappers = new List<ScraperBase>{
            new ABCMalayalam(),
            new Einthusan(),
            new Hindi4ULink(),
            new ThriruttuVCD(),
            new TamizhWS(),
            new India4movie()
        };

        protected List<ScrapedMovie> allMovies = new List<ScrapedMovie>();
        public event EventHandler<MovieFoundEventArgs> MovieFound;
        public event EventHandler<ScraperNotFound> ScraperNotFound;
        public event EventHandler<NotificationEventArgs> Notify;

        protected void OnMovieFound(MovieFoundEventArgs args)
        {
            if (MovieFound != null)
                MovieFound(this, args);
        }

        protected void OnScraperNotFound(ScraperNotFound args)
        {
            if (ScraperNotFound != null)
                ScraperNotFound(this, args);
        }

        protected void OnNotify(NotificationEventArgs args)
        {
            if (Notify != null)
                Notify(this, args);
        }

        protected VideoScraperBase GetScrapper(string url)
        {
            try
            {
                var scraper = VideoScraperBase.GetScraper(url);
                return scraper;
            }
            catch { return null; }

        }

        protected bool IgnoreLink(string url)
        {
            return String.IsNullOrWhiteSpace(url) ||
                url.ToLower().Contains("royalvid.me") ||
                url.ToLower().Contains("docs.google.com") ||
                url.ToLower().Contains("livestream.com") ||
                url.ToLower().Contains("youku.com") ||
                url.ToLower().Contains("mightyupload.com") ||
                url.ToLower().Contains("abcmalayalam.com") ||
                url.ToLower().Contains("dailymotion.com") ||
                url.ToLower().Contains("embedr.com") ||
                url.ToLower().Contains("megavideo.com") ||
                url.ToLower().Contains("embed.trilulilu.ro") ||
                url.ToLower().Contains("purevid.com") ||
                url.ToLower().Contains("video.tt") ||
                url.ToLower().Contains("videobb.com") ||
                url.ToLower().Contains("videozer.com") ||
                url.ToLower().Contains("streamin.to") ||
                url.ToLower().Contains("google.com") ||
                url.ToLower().Contains("megaupload.com") ||
                url.ToLower().Contains("youtube.com/p/") ||
                url.ToLower().Contains("facebook.com");
        }

      
    }

    public class MovieFoundEventArgs : EventArgs
    {
        public MovieFoundEventArgs(ScrapedMovie movie)
        {
            Movie = movie;
        }

        public ScrapedMovie Movie { get; private set; }
    }

    public class ScraperNotFound : EventArgs
    {
        public ScraperNotFound(string url, string pageUrl)
        {
            Url = url;
            PageUrl = pageUrl;
        }

        public string Url { get; private set; }
        public string PageUrl { get; set; }
    }
    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs(string msg)
        {
            Message = msg;
        }

        public string Message { get; set; }
    }

    public enum ImagePriorityRank
    {
        ABC = 1,
        Tamizh,
        TVCD,
        HL4U,
        I4M,
        EIH

    }

       
}
