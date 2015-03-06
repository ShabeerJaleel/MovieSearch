using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MovieTube.Client.Scraper;
using System.Linq;
using System.ComponentModel;
using System.Net;
using System.Collections.Specialized;

namespace MovieTube.Viewer
{
    class ScraperService
    {
        private static List<ScrapRequest> requests = new List<ScrapRequest>();

        public string ScrapUrl(string url)
        {
            return VideoScraperBase.ScrapeUrl(url);

        }

        public void ScrapVideosDetailsAsync(IScraperServiceCallback callback, Movie movie)
        {
            foreach (var m in movie.Links)
            {
                if (m.ScrapState == LinkScrapState.Idle)
                    ScrapVideosDetailsAsync(callback, m);
            }
        }
        
        public void ScrapVideosDetailsAsync(IScraperServiceCallback callback, MovieLink link)
        {
            if (link.ScrapState == LinkScrapState.FullyLoaded)
            {
                callback.OnScrapVideoDetailsCompleted(link);
                return;
            }
            else if (link.ScrapState == LinkScrapState.VideoDoesNotExists)
            {
                callback.OnScrapError(link, "Video is removed");
                return;
            }

            lock (requests)
            {
                var req = requests.FirstOrDefault(x => x.Link == link);
                if (req == null)
                    requests.Add(new ScrapRequest(link, callback));
                else if (!req.Clients.Any(x => x == callback))
                    req.Clients.Add(callback);

                if (link.ScrapState == LinkScrapState.Scraping)
                    return;
            }
           
            link.ScrapState = LinkScrapState.Scraping;
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    link.PlayUrl = ScrapUrl(link.DowloadUrl);
                    link.ScrapState = LinkScrapState.FullyLoaded;
                    link.RetrievedTime = DateTime.Now;
                    Tracer.WriteLine(String.Format("Provider: {0} , Download Link: {1}, View Link: {2}", link.Provider.ID, link.PlayUrl, link.DowloadUrl));
                    var request = PopRequest(link);
                    foreach(var r in request.Clients)
                        r.OnScrapVideoDetailsCompleted(link);
                }
                catch (Exception ex)
                {
                    var se = ex as ScraperException;
                    link.ScrapState = se != null && se.Type == ScraperResult.VideoDoesNotExist ? LinkScrapState.VideoDoesNotExists : LinkScrapState.Idle;
                    Tracer.WriteLine(String.Format("Error: Provider: {0} , View Link: {1}, Msg: {2}", link.Provider.ID, 
                        link.DowloadUrl, ex.Message));
                    var request = PopRequest(link);
                    foreach (var r in request.Clients)
                        r.OnScrapError(link, ex.Message);

                    if (link.ScrapState == LinkScrapState.VideoDoesNotExists)
                    {
                        try
                        {
                            var data = new NameValueCollection();
                            data.Add("link", link.DowloadUrl);
                            UpdaterService.PostResource(UrlConstants.LinkRemovedUrl, data);
                        }
                        catch { }
                    }
                   
                }
            });
        }

        private ScrapRequest PopRequest(MovieLink link)
        {
            lock (requests)
            {
                var r = requests.FirstOrDefault(x => x.Link == link);
                requests.Remove(r);
                return r;
            }
        }

      
    }

    

    public interface IScraperServiceCallback
    {
        void OnScrapVideoDetailsCompleted(MovieLink link);
        void OnScrapError(MovieLink link, string message);
    }

    public interface IDownloadProgress
    {
        void OnProgress(DownloadInfo di, DownloadProgressChangedEventArgs e);
        void OnCompletion(DownloadInfo di, AsyncCompletedEventArgs e);
    }

    public class DownloadInfo
    {
        private Guid id = Guid.NewGuid();
        public Guid Id { get { return id; } }
        public MovieLink Link { get; set; }
        public string FilePath { get; set; }
        public DownloaderService Service { get; set; }
    }

    class ScrapRequest
    {
        public ScrapRequest(MovieLink link, IScraperServiceCallback client)
        {
            Link = link;
            Clients = new List<IScraperServiceCallback>();
            Clients.Add(client);
        }
        public MovieLink Link { get; set; }
        public List<IScraperServiceCallback> Clients { get; private set; }
    }
}
