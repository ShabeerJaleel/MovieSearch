using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsQuery;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace MovieFinder.Scraper
{
   

    public class TamizhWS : MovieDetailsScraperBase
    {
        

        public override List<ScrapedMovie> ScrapeMovies(List<string> skipUrls, List<int> years = null)
        {
            try
            {

                var dom = GotoUrl(RootUrl);
                if (years == null)
                    years = new List<int>();

                //loop through each year
                var elems = SelectItems(dom, "#HTML5.widget div.widget-content a");
               
                for (var i = elems.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        var elem = elems[i];
                        int year = Convert.ToInt32(elem.InnerText.Trim());
                        if (years.Count > 0 && !years.Any(x => x == year))
                            continue;
                         var urls = new Stack<string>();
                        //goto year page
                        try
                        {
                            dom = GotoUrl(ReadAttribute(elem, "href"));

                            while (true) //grab all links
                            {
                                var mUrls = SelectItems(dom, "div.blog-posts a").Skip(4).ToList();
                                foreach (var mu in mUrls)
                                {
                                    var h = ReadAttribute(mu, "href");
                                    if (urls.Contains(h))
                                        continue;
                                    urls.Push(h);
                                }
                                if (mUrls.Count == 0)
                                    break;

                                dom = GotoUrl(ReadAttribute(SelectItem(dom, "#blog-pager-older-link a"), "href"));
                            }
                        }
                        catch { continue; }


                        while (urls.Count > 0)
                        {
                            string u = null;
                            try
                            {
                                u = urls.Pop();
                                dom = GotoUrl(u);
                                var title = SelectItem(dom, ".post-title.entry-title a").InnerText;
                                ScrapedMovie movie = null;
                                try
                                {
                                    movie = new ScrapedMovie(this)
                                    {
                                        PageUrl = u,
                                        Description = title.Contains("-") ? title.Split('-')[1] : String.Empty,
                                        Name = FixTitle(title.Contains("-") ? title.Split('-')[0] : title),
                                        LangCode = "ta",
                                        ReleasedDate = new DateTime(year, 1, 1),
                                        ImageUrl = ReadAttribute(SelectItem(dom, "div.post-body.entry-content img"), "src")

                                    };
                                }
                                catch
                                {

                                }
                                OnNotify(new NotificationEventArgs("Processing " + movie.PageUrl + ". Year: " + year.ToString()));
                                allMovies.Add(movie);
                                foreach (var item in SelectItems(dom, ".fullpost a"))
                                {
                                    
                                    string linkUrl = null;
                                    try
                                    {
                                        linkUrl = ReadAttribute(item, "href");
                                        if (linkUrl.Contains("links2sites"))
                                        {
                                            dom = GotoUrl(linkUrl);
                                            try
                                            {
                                                linkUrl = ReadAttribute(SelectItem(dom, ".post-body.entry-content embed"), "src");
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    linkUrl = ReadAttribute(SelectItem(dom, ".post-body.entry-content iframe"), "src");
                                                }
                                                catch { }
                                            }
                                            
                                        }


                                        if (IgnoreLink(linkUrl))
                                            continue;

                                        var host = GetScrapper(linkUrl);
                                        if (host != null)
                                        {
                                            if (skipUrls.Any(x => x == linkUrl))
                                                continue;
                                            linkUrl = host.SanitizeUrl(linkUrl);

                                            if (skipUrls.Any(x => x == linkUrl))
                                                continue;
                                            skipUrls.Add(linkUrl);

                                            MovieTube.Client.Scraper.ScraperResult result = MovieTube.Client.Scraper.ScraperResult.Success;
                                            try
                                            {
                                                result = MovieTube.Client.Scraper.VideoScraperBase.ValidateUrl(linkUrl);
                                            }
                                            catch { }

                                            if (result != MovieTube.Client.Scraper.ScraperResult.Success &&
                                                result != MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist)
                                            {
                                                var k = 0;
                                            }
                                            if (result != MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist)
                                                movie.Links.Add(new ScrapedMovieLink(linkUrl, host.ID, "Watch Full Movie"));
                                        }
                                        else
                                        {
                                            OnScraperNotFound(new ScraperNotFound(linkUrl, movie.PageUrl));
                                        }
                                    }
                                    catch (WebException ex)
                                    {
                                    }
                                    catch { }
                                }

                                if (movie.Links.Count > 0)
                                {
                                    var args = new MovieFoundEventArgs(movie);
                                    OnMovieFound(args);

                                }
                                if (this.stop)
                                    return allMovies;
                            }
                            catch { }
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

        private string FixTitle(string title)
        {
            var org = title;
            if (title.ToLower().StartsWith("watch"))
                title = title.Replace("watch","").Replace("Watch", "");
            var index = -1;
            if(title.StartsWith("("))
            {
                title = title.Replace("(" + SubstringBetween(title, "(", ")") + ")", "");
            }
            
            var keywords = new List<string>{
                "dvd",
                "tamil",
                "movie",
                "3d",
                " tc",
                "rip",
                "online",
                "watch",
                "good",
                "quality",
                "lotus"
            };

            var start = 0;
            if (Char.IsDigit(title[0]))
                start = 4;
            for (var i = start; i < title.Length; i++)
            {
                var c = title[i];
                if (!char.IsLetter(c) && c != ' ')
                {
                    if (char.IsDigit(c) && !char.IsDigit(title[i + 1]) && !char.IsDigit(title[i + 2]))
                        continue;
                    else
                    {
                        index = i;
                        break;
                    }
                }
            }
        
            foreach (var kw in keywords)
            {
                var ind = title.ToLower().IndexOf(kw);
                if ((ind > -1 && ind < index) || index == -1)
                    index = ind;
            }
            if (index > -1)
                title = title.Substring(0, index);
            title =  title.Trim();
            if (String.IsNullOrWhiteSpace(title))
                throw new Exception("No title");
            return title;
        }

        public override string RootUrl
        {
            get { return "http://www.tamizh.ws/"; }
        }

        public override string ID
        {
            get { return "tamizh"; }
        }

        public override string Title
        {
            get { return "Tamizh"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get { return  ImagePriorityRank.Tamizh; }
        }
    }

   
}
