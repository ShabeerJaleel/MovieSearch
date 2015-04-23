using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsQuery;
using System.Diagnostics;

namespace MovieFinder.Scraper
{
   

    

    public class Einthusan : MovieDetailsScraperBase
    {
        private Dictionary<string, string> RootLinks = new Dictionary<string,string>();

        public Einthusan()
        {
            RootLinks.Add("http://www.einthusan.com/movies/index.php?lang=hindi&organize=Year", "hi");
            RootLinks.Add("http://www.einthusan.com/movies/index.php?lang=malayalam&organize=Year", "ml");
            RootLinks.Add("http://www.einthusan.com/movies/index.php?lang=tamil&organize=Year", "ta");
            RootLinks.Add(" http://www.einthusan.com/movies/index.php?lang=telugu&organize=Year", "te");
        }

        public override List<ScrapedMovie> ScrapeMovies(List<string> skipUrls, List<int> years = null)
        {
            if (years == null)
                years = new List<int>();
            try
            {
                foreach (var entry in RootLinks)
                {
                    var dom = GotoUrl(entry.Key, 3);

                    var elems = SelectItems(dom, ".video-organizer-element-wrapper a");
                    for (var i = elems.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            var elem = elems[i];
                            int year;
                            Int32.TryParse(elem.InnerText, out year);

                            if (years.Count > 0 && !years.Any(x => x == year))
                                continue;

                            dom = GotoUrl(new Uri(new Uri(entry.Key), ReadAttribute(elem, "href")).AbsoluteUri, 3);

                            if (!Int32.TryParse((Regex.Replace(ReadText(SelectItems(dom, ".filter-selected").First()), "[^0-9.]", "")), out year))
                                continue;
                            if (year.ToString().Length != 4)
                                continue;

                            if (years.Count > 0 && !years.Any(x => x == year))
                                continue;

                            foreach (var el in SelectItems(dom, ".numerical-nav a"))
                            {
                                try
                                {
                                    dom = GotoUrl(new Uri(new Uri(entry.Key), ReadAttribute(el, "href")).AbsoluteUri, 3);
                                    foreach (var subElem in SelectItems(dom, ".video-object-wrapper"))
                                    {
                                        try
                                        {
                                            dom = CQ.Create(subElem);
                                            var movie = new ScrapedMovie(this);
                                            var e = dom.Select(".movie-title").Elements.First();
                                            movie.PageUrl = new Uri(new Uri(RootUrl), ReadAttribute(e, "href").Replace("..", "")).AbsoluteUri;
                                            OnNotify(new NotificationEventArgs("Processing " + movie.PageUrl + ". Year: " + year.ToString()));
                                            if (skipUrls.Any(x => x == movie.PageUrl))
                                                continue;
                                            movie.ImageUrl = ReadAttribute(dom.Select(".video-object-thumb img").Elements.First(), "src");
                                            movie.ReleasedDate = new DateTime(year, 1, 1);
                                            movie.LangCode = entry.Value;
                                            movie.Description = ReadText(dom.Select(".desc_body").Elements.First()).Replace("-", "");
                                            movie.Name = ReadText(e).Replace("\n", "").Replace("\t", "");
                                            movie.Name = Regex.Replace(movie.Name, @"\s*?(?:\(.*?\)|\[.*?\]|\{.*?\})", String.Empty);
                                            movie.Links.Add(new ScrapedMovieLink(movie.PageUrl, "einthusan.com", "With Subtitles"));
                                            allMovies.Add(movie);
                                            var args = new MovieFoundEventArgs(movie);
                                            OnMovieFound(args);
                                            if (this.stop)
                                                return allMovies;
                                        }
                                        catch { }
                                    }
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }
                }
            }
            catch { }
            return allMovies;
        }
        public override string RootUrl
        {
            get { return "http://www.einthusan.com/"; }
        }

        public override string ID
        {
            get { return "eih"; }
        }

        public override string Title
        {
            get { return "EIH"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get { return  ImagePriorityRank.EIH; }
        }
    }

   
}
