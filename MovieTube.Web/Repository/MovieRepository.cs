using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieTube.Web.Models;
using MovieFinder.Data;
using System.Data.Entity;
using MovieTube.Client.Scraper;
using System.IO;
using MovieTube.Web.Services;

namespace MovieTube.Data
{
    public class MovieRepository  : IMovieRepository
    {
        private readonly int ThumbsPerPage = 20;
        private readonly int ThumbsPerLang = 6;
        private readonly IConfigProvider confProvider;
        private readonly IImageUrlBuilder imgUrlBuilder;


        public MovieRepository()
        {
            this.confProvider = new ConfigProvider();
            this.imgUrlBuilder = new ImageUrlBuilder();
        }

        public ThumbNailVm List(string term, string language, int? year, int? page)
        {
            year = !year.HasValue ? 0 : year;
            page = (!page.HasValue || page < 0) ? 0 : page;
            List<MovieThumbnailVm> movies = null;
            if (String.IsNullOrWhiteSpace(term) &&
                String.IsNullOrWhiteSpace(language))
            {
                movies = QueryMovies(term, "ml", year, page, ThumbsPerLang);
                movies.AddRange(QueryMovies(term, "hi", year, page, ThumbsPerLang));
                movies.AddRange(QueryMovies(term, "ta", year, page, ThumbsPerLang));
                movies.AddRange(QueryMovies(term, "te", year, page, ThumbsPerLang));

            }
            else
                movies = QueryMovies(term, language, year.Value, page, ThumbsPerPage);

            return new ThumbNailVm
            {
                Thumbnails = movies,
                NextPage = movies.Count < ThumbsPerPage ? -1 : page.Value + 1
            };
        }

        public List<MovieThumbnailVm> QueryMovies(string term, string langCode, int? year, int? page, int count)
        {
            using (var db = new MovieFinderEntities())
            {
                try
                {
                    IQueryable<Movie> movies = null;
                    if (!String.IsNullOrWhiteSpace(langCode))
                        movies = db.Movies.Where(x => x.LanguageCode == langCode);
                    if (!String.IsNullOrWhiteSpace(term))
                        movies = (movies == null ? db.Movies : movies).Where(x => x.Name.StartsWith(term));
                    if (year != 0)
                        movies = (movies == null ? db.Movies : movies).Where(x => x.ReleaseDate.Year == year);
                    if (movies == null)
                        movies = db.Movies;
                    var m = movies.OrderByDescending(x => x.ReleaseDate)
                           .ThenByDescending(x => x.CreateDate)
                           .Skip(page.Value * count)
                           .Take(count)
                           .ToList()
                           .Select(x => new MovieThumbnailVm
                           {
                               ImageUrl = String.IsNullOrWhiteSpace(x.ImageLocalUrl) ? x.ImageUrl : imgUrlBuilder.Build(x.ImageLocalUrl),
                               PostedBy = "Admin",
                               PostedDate = x.CreateDate.ToShortDateString(),
                               Title = x.Name,
                               Language = GetLanguage(x.LanguageCode),
                               ReleasedYear = x.ReleaseDate.Year,
                               Id = x.UniqueID,
                               Url = String.Format("{0}/Watch/{1}/{2}/{3}/{4}",confProvider.RootUrl,
                               GetLanguage(x.LanguageCode), x.ReleaseDate.Year, x.UniqueID, x.Name),
                               ViewCount = x.ViewCount,
                               LikeCount = x.LikeCount
                           }).ToList();
                    return m;
                }
                catch
                {
                    return null;
                }
            }
        }

        public MovieVm QueryMovie(string id, bool updateStat = false)
        {
            using (var db = new MovieFinderEntities())
            {
                try
                {
                    var movie =  db.Movies.Where(x => x.UniqueID == id)
                              .Include(x => x.MovieLinks)
                              .ToList()
                              .Select(x => new MovieVm{
                                   ImageUrl = String.IsNullOrWhiteSpace(x.ImageLocalUrl) ? x.ImageUrl : imgUrlBuilder.Build(x.ImageLocalUrl),
                                   PostedBy = "Admin",
                                   Description = x.Description,
                                   PostedDate = x.CreateDate.ToShortDateString(),
                                   Title = x.Name,
                                   Language = GetLanguage(x.LanguageCode),
                                   ReleasedYear = x.ReleaseDate.Year,
                                   Id = x.UniqueID,
                                   Url = String.Format("{0}/Watch/{1}/{2}/{3}/{4}", confProvider.RootUrl,
                                   GetLanguage(x.LanguageCode), x.ReleaseDate.Year, x.UniqueID, x.Name),
                                   ViewCount = x.ViewCount,
                                   LikeCount = x.LikeCount,
                                   Links = x.MovieLinks.Where(z => z.IsWebSupported).Select(y => new VideoLinkVm{
                                        HostSite = y.DownloadSiteID,
                                        Title = ShortenLinkTitle(y.LinkTitle),
                                        Url = VideoScraperBase.GetScraper(y.DowloadUrl).GetFlashUrl(y.DowloadUrl),
                                        ID = y.ID,
                                        PartID = y.PartID,
                                        PartIndex = y.PartIndex
                                   }).ToList()
                              }).FirstOrDefault();
                    movie.Links.Sort();
                    
                    if (updateStat)
                    {
                        var m = db.Movies.Single(x => x.UniqueID == id);
                        m.ViewCount++;
                        db.SaveChanges();
                    }
                    
                    return movie;
                }
                catch
                {
                    return null;
                }
            }
        }

        private string ShortenLinkTitle(string title)
        {
            if (String.IsNullOrWhiteSpace(title))
                return title;
            title = title.ToLower();
            var text = ScraperBase.Substring(title, "part");
            if (String.IsNullOrWhiteSpace(text))
                return " Full Movie";
            return "Part - " + text;
        }

        private string GetLanguage(string langCode)
        {
            switch (langCode)
            {
                case "hi":
                    return "Hindi";
                case "ml":
                    return "Malayalam";
                case "te":
                    return "Telugu";
                case "ta":
                    return "Tamil";
                default:
                    return "Unknown";
            }
        }
    }
}