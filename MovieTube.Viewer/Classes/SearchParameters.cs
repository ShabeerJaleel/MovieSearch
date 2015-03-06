using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MovieTube.Client.Scraper
{
  
    public class MoviePage
    {
        public MoviePage()
        {
            Videos = new List<Movie>();
            Links = new List<PagingLink>();
        }
        public List<Movie> Videos { get; set; }
        public List<PagingLink> Links { get; set; }
    }
    public class Movie 
    {
        private List<MovieLink> links;
        public Movie()
        {
            RelatedVideos = new List<Movie>();
        }
        public string Url { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public string PlayUrl { get; set; }
        public int Quality { get; set; }
        public List<Movie> RelatedVideos { get; set; }
        public List<MovieLink> Links { 
         get
            {
                if(links == null)
                    links = MovieTube.Viewer.Data.ClientDataService.Single.ReadMovieLinks(this);
                return links;
            }
            
        }
        public bool FullyLoaded { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public int ReleaseYear { get { return ReleaseDate.Year; } }
        public string LanguageCode { get; set; }
        public string LanguageText
        {
            get
            {
                try
                {
                    return Language.Languages.Find(x => x.Id == LanguageCode).Name;
                }
                catch { }
                return "Unknown";
            }

        }
        public string Description { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int Version { get; set; }
       // public int VersionChange { get; set; }
        public string UniqueID { get; set; }
        
    }

    public class MovieLink : IComparable<MovieLink>
    {
        public event EventHandler<EventArgs> ScrappingStatusChanged;
        private LinkScrapState scrapState;

        private string downloadUrl;
        public int ID { get; set; }
        public int MovieID { get; set; }
        public string SiteTitle { get; set; }
        public string DownloadSiteID { get; set; }
        public string PageUrl { get; set; }
        public string PageSiteID { get; set; }
        public string DowloadUrl
        {
            get { return this.downloadUrl; }
            set
            {
                Provider = VideoScraperBase.GetScraper(value);
                if (Provider == null)
                    throw new Exception("Provider is not available");
                this.downloadUrl = value;
            }
        }
        public string LinkTitle { get; set; }
        public string PlayUrl { get; set; }
        public virtual Movie Parent { get; set; }
        public LinkScrapState ScrapState 
        {
            get
            {
                if (scrapState == LinkScrapState.FullyLoaded && RetrievedTime.AddHours(2) < DateTime.Now)
                    scrapState = LinkScrapState.Idle;
                return scrapState;
            }
            set
            {
                if (this.scrapState != value)
                {
                    this.scrapState = value;
                    if (ScrappingStatusChanged != null)
                        ScrappingStatusChanged(this, new EventArgs());
                }
            }
        }
        public double LastPosition { get; set; }
        public VideoScraperBase Provider { get; private set; }

        public int CompareTo(MovieLink other)
        {
            var r = 0;
            if ((int)this.Provider.Rank > 1 && (int)other.Provider.Rank > 1)
            {
                r = other.Version.CompareTo(this.Version);
                if (r != 0)
                    return r;
            }
            r = this.Provider.Rank.CompareTo(other.Provider.Rank);
            if (r != 0)
                return r;
            return this.ID.CompareTo(other.ID);
        }
        public int Version { get; set; }
        public DateTime RetrievedTime { get; set; }

    }

   

    public enum LinkScrapState
    {
        Idle,
        FullyLoaded,
        Scraping,
        VideoDoesNotExists
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
    public interface IScraperServiceCallback
    {
        void OnScrapVideoCompleted(MoviePage page);
        void OnScrapVideoDetailsCompleted(Movie video);
        void OnScrapError(Exception ex);
    }

    public class SearchParameters
    {
        public string Url { get; set; }
        public string Query { get; set; }
        public VideoSortby SortBy { get; set; }
        public VideoPeriod Period { get; set; }
        public VideoDuration Duration { get; set; }

        public int Year { get; set; }
        public string Language { get; set; }
    }
}
