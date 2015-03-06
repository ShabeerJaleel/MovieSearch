using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace  Client.Scraper
{
    public class ScrapedPage
    {
        public ScrapedPage()
        {
            Videos = new List<ScrapedVideo>();
            Links = new List<PagingLink>();
        }
        public List<ScrapedVideo> Videos { get; set; }
        public List<PagingLink> Links { get; set; }
    }
    public class ScrapedVideo
    {
        public ScrapedVideo()
        {
            RelatedVideos = new List<ScrapedVideo>();
        }
        public string Url { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public string PlayUrl { get; set; }
        public int Quality { get; set; }
        public List<ScrapedVideo> RelatedVideos { get; set; }
        public bool FullyLoaded { get; set; }
    }

    public class PagingLink
    {
        public string Url { get; set; }
        public string Text { get; set; }
        public bool IsSelected { get; set; }
    }

    public enum VideoSortby
    {
        Relevance,
        Date,
        Rating
    }

    public enum VideoPeriod
    {
        Anytime,
        Today,
        ThisWeek,
        ThisMonth
    }

    public enum VideoDuration
    {
        All,
        Short,
        Medium,
        Long
    }

    public class SearchParameters
    {
        public string Url { get; set; }
        public string Query { get; set; }
        public VideoSortby SortBy { get; set; }
        public VideoPeriod Period { get; set; }
        public VideoDuration Duration { get; set; }
    }
}
