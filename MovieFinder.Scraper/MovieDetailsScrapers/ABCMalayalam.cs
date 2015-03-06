using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace MovieFinder.Scraper
{
    public class ABCMalayalam : MovieDetailsScraperBase
    {

        public override string RootUrl
        {
            get { return "http://abcmalayalam.com"; }
        }

        public override List<ScrapedMovie> ScrapeMovies(List<string> skipUrls, List<int> years = null)
        {

            try
            {

                var dom = GotoUrl(RootUrl);
                if (years == null)
                    years = new List<int>();

                //loop through each year
                var elems = SelectItems(dom, "#s5_right_top_wrap li a");
                for (var i = elems.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        var elem = elems[i];
                        int year = 0;
                        try
                        {
                            year = Convert.ToInt32(Regex.Replace(ReadText(elem), "[^0-9.]", ""));
                        }
                        catch { }
                        if (year == 0 ||( years.Count > 0 && !years.Any(x => x == year)))
                            continue;
                        //goto first page
                        var urls = new List<string>();
                        var t = ReadAttribute(elem, "href");
                        if (t.StartsWith("http"))
                            urls.Add(t);
                        else
                            urls.Add(RootUrl + t);
                            
                        try
                        {
                            dom = GotoUrl(urls[0]);
                        }
                        catch { continue; }

                        //select remianign pages for the year
                        var pages = SelectItems(dom, ".k2Pagination a");
                        foreach (var page in pages)
                        {
                            var u = RootUrl + ReadAttribute(page, "href");
                            if (!urls.Contains(u))
                                urls.Add(u);
                        }

                        for (var j = urls.Count - 1; j >= 0; j--)
                        {
                            try
                            {
                                dom = GotoUrl(urls[j]);
                            }
                            catch { continue; }
                            if (!GetMovies(year, dom, skipUrls))
                                return allMovies;
                        }
                    }
                    catch { }

                }
                
            }
            catch (Exception ex)
            {

                //throw;
            }
            return allMovies;
        }


        private bool GetMovies(int year, CQ dom, List<string> skipUrls)
        {
            var items = SelectItems(dom, ".itemList a");
            if (items.Count == 0)
            {
                Debug.Assert(false, "No movies");
            }
            for (var i = items.Count - 1; i >= 0; i--)
            {
                var elem1 = items[i];
                var movieUrl = RootUrl + ReadAttribute(elem1, "href");
                OnNotify(new NotificationEventArgs("Processing " + movieUrl + ". Year: " + year.ToString()));
                if (allMovies.Any(x => x.PageUrl == movieUrl) )
                    continue;
                try
                {
                    dom = GotoUrl(movieUrl);
                }
                catch { continue; }

                var links = SelectItems(dom, ".itemIntroText table a");
                if (links.Count == 0)
                    links = SelectItems(dom, "div.itemFullText a");
                if (links.Count == 0)
                    links = SelectItems(dom, ".avPlayerBlock iframe");

                if (links.Count > 0)
                {
                    var movie = new ScrapedMovie(this);
                    movie.ReleasedDate = new DateTime(year, 1, 1);
                    movie.LangCode = "ml";
                    movie.Language = "Malayalam";
                    movie.Name = ReadText(SelectItems(dom, ".itemTitle")[0]).Replace("\n", "").Replace("\t", "");
                     
                    try
                    {
                        try
                        {
                            movie.Description = ReadText(SelectItems(dom, ".itemIntroText p")[0]);
                        }
                        catch
                        {
                            try
                            {
                                movie.Description = ReadText(SelectItems(dom, ".itemIntroText")[0]);
                            }
                            catch{
                                var spans = SelectItems(dom, ".typeTextfield span");
                                if (spans.Count > 0)
                                {
                                    movie.Description = String.Empty;
                                    foreach (var span in spans)
                                        movie.Description += span.InnerText;
                                }
                            }
                        }
                        if (movie.Description != null)
                            movie.Description = movie.Description.Replace("\n", "").Replace("\t", "");
                    }
                    catch { }
                    try
                    {
                        var a = SelectItems(dom, ".itemIntroText p img").FirstOrDefault();
                        if(a == null)
                            a = SelectItems(dom, ".itemImage a img").FirstOrDefault();
                        if (a == null)
                            a = SelectItems(dom, ".itemIntroText img").FirstOrDefault();
                        if(a == null)
                            a = SelectItems(dom, ".itemIntroText span img").FirstOrDefault();
                        if (a != null)
                            movie.ImageUrl = RootUrl + ReadAttribute(a, "src");
                    }
                    catch { }
                    movie.PageUrl = movieUrl;



                    allMovies.Add(movie);
                    foreach (var link in links)
                    {
                        string linkUrl = null;
                        try
                        {
                            linkUrl = ReadAttribute(link, "href");
                        }
                        catch {
                            try
                            {
                                linkUrl = ReadAttribute(link, "src");
                            }
                            catch { }
                        }
                        if (IgnoreLink(linkUrl))
                            continue;
                        var host = GetScrapper(linkUrl);
                        if (host != null)
                        {
                            if (skipUrls.Any(x => x == linkUrl))
                                continue;
                            try
                            {
                                linkUrl = host.SanitizeUrl(linkUrl);
                            }
                            catch { continue; }
                            if (skipUrls.Any(x => x == linkUrl))
                                continue;

                            MovieTube.Client.Scraper.ScraperResult result = MovieTube.Client.Scraper.ScraperResult.Success;
                            try
                            {
                                result = MovieTube.Client.Scraper.VideoScraperBase.ValidateUrl(linkUrl);
                            }
                            catch { }
                            if (result != MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist)
                                movie.Links.Add(new ScrapedMovieLink(linkUrl, host.ID, link.InnerText));
                        }
                        else
                        {
                            OnScraperNotFound(new ScraperNotFound(linkUrl, movie.PageUrl));
                        }
                    }

                    if (movie.Links.Count > 0)
                    {
                        var args = new MovieFoundEventArgs(movie);
                        OnMovieFound(args);

                    }
                    if (this.stop)
                        return false;
                }
                else
                {
                }
            }
            return true;
        }

        public override string ID
        {
            get { return "abc"; }
        }

        public override string Title
        {
            get { return "ABC"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get { return ImagePriorityRank.ABC; }
        }
    }
}
