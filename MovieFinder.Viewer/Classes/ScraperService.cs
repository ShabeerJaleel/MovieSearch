using System;
using System.Collections.Generic;
using System.Text;
using Client.Scraper;
using System.Threading;

namespace BlueTube.Viewer
{
    class ScraperService
    {
        
        public ScrapedPage ScrapVideos(SearchParameters sparam = null)
        {
            var scraper = new XVideoScraper();
            return scraper.ScrapeVideos(sparam);
        }

        public ScrapedVideo ScrapVideoDetails(ScrapedVideo video)
        {
            var scraper = new XVideoScraper();
            return scraper.ScrapeVideoDetails(video);
        }

        public void ScrapVideosAsync(IScraperServiceCallback callback, SearchParameters sparam = null)
        {
            ThreadPool.QueueUserWorkItem(delegate 
            {
                try
                {
                    callback.OnScrapVideoCompleted(new XVideoScraper().ScrapeVideos(sparam)); 
                }
                catch (Exception ex)
                {
                    callback.OnScrapError(ex);
                }
                
            
            });
        }

        public void ScrapVideosDetailsAsync(ScrapedVideo video, IScraperServiceCallback callback)
        {
            ThreadPool.QueueUserWorkItem(delegate {
                try
                {
                    callback.OnScrapVideoDetailsCompleted(new XVideoScraper().ScrapeVideoDetails(video));
                }
                catch (Exception ex)
                {
                    callback.OnScrapError(ex);
                }
            });
        }

      
    }

    public interface IScraperServiceCallback
    {
        void OnScrapVideoCompleted(ScrapedPage page);
        void OnScrapVideoDetailsCompleted(ScrapedVideo video);
        void OnScrapError(Exception ex);
    }
}
