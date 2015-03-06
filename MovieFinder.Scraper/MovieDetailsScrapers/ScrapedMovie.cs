using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MovieFinder.Scraper
{
    public class ScrapedMovie
    {
        private string name;
        private string description;

        public ScrapedMovie(ScraperBase scraper)
        {
            Links = new List<ScrapedMovieLink>();
            Scraper = scraper;
            description = "No description available";
        }
        public string ImageUrl { get; set; }
        public DateTime ReleasedDate { get; set; }
        public string Language { get; set; }
        public string PageUrl { get; set; }
        public string LangCode { get; set; }
        public List<ScrapedMovieLink> Links { get; set; }
        public ScraperBase Scraper { get; set; }

        public string Description 
        {
            get { return description; }
            set { description = value.Substring(0, Math.Min(value.Length, 1000)); }
        }
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = Regex.Replace(value, @"\s*?(?:\(.*?\)|\[.*?\]|\{.*?\})", String.Empty).Trim();
            }
        }

        public string UniqueId
        {
            get
            {
                var rgx = new Regex("[^a-zA-Z0-9]");
                return String.Format("{0}-{1}-{2}", rgx.Replace(name, ""), ReleasedDate.Year, LangCode).ToLower();
            }
        }
     
    }

    public class ScrapedMovieLink
    {
        public ScrapedMovieLink(string url, string site, string title = null)
        {
            DownloadUrl = url;
            DownloadSiteID = site;
            Title = title;

        }
        public string DownloadUrl { get; private set; }
        public string DownloadSiteID { get; private set; }
        public string Title { get; private set; }
    }
}
