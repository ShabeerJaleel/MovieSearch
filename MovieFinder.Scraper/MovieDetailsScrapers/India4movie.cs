using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsQuery;
using System.Diagnostics;
using System.IO;

namespace MovieFinder.Scraper
{
   

    public class India4movie : MovieDetailsScraperBase
    {
        private Dictionary<string, int> RootLinks = new Dictionary<string,int>();

        public India4movie()
        {
            RootLinks.Add("http://www.india4movie.com/category/telugu-movie-2013/", 2013);
            RootLinks.Add("http://www.india4movie.com/category/telugu-movie-2014/", 2014);
            
        }

        public override List<ScrapedMovie> ScrapeMovies(List<string> skipUrls, List<int> years = null)
        {
            if (years == null)
                years = new List<int>();

            foreach (var entry in RootLinks)
            {
                try
                {
                    if (years.Count > 0 && !years.Any(x => x == entry.Value))
                        continue;
                    int year = entry.Value;
                    var dom = GotoUrl(entry.Key);
                    var lastPage = SelectItem(dom, "a.last").Attributes["href"];
                    int ix1 = lastPage.LastIndexOf('/');
                    int ix2 = ix1 > 0 ? lastPage.LastIndexOf('/', ix1 - 1) : -1;
                    var count = Convert.ToInt32(lastPage.Substring(ix2 + 1, lastPage.Length - ix2 - 2));
                    lastPage = lastPage.Substring(0, ix2 + 1);
                    var pageUrls = new List<string>();
                    for (var i = count; i >= 2; i--)
                        pageUrls.Add(lastPage + i.ToString() + "/");
                    pageUrls.Add(entry.Key);
                    foreach (var elem in pageUrls)
                    {
                        try
                        {
                            dom = GotoUrl(elem);
                            var postBoxes = SelectItems(dom, "h2.title a");
                            for (var j = postBoxes.Count - 1; j >= 0; j--)
                            {
                                try
                                {
                                    var movie = new ScrapedMovie(this);
                                    allMovies.Add(movie);
                                    movie.PageUrl = ReadAttribute(postBoxes[j], "href");
                                    OnNotify(new NotificationEventArgs("Processing " + movie.PageUrl + ". Year: " + year.ToString()));
                                    movie.LangCode = "te";
                                    movie.ReleasedDate = new DateTime(year, 1, 1);
                                    dom = GotoUrl(movie.PageUrl);
                                    movie.Name = FixTitle( SelectItem(dom, "h2.title").InnerText);
                                    movie.ImageUrl = SelectItem(dom, "img.wp-post-image").Attributes["src"];
                                    try
                                    {
                                        movie.Description = SelectItems(dom, "div.entry p span")[1].InnerText;
                                    }
                                    catch
                                    {
                                        //try
                                        //{
                                        //    movie.Description = SelectItem(dom, ".textsection").InnerText;
                                        //}
                                        //catch { }
                                    }

                                    var links = new Dictionary<string, string>();
                                    var anchors = SelectItems(dom, "a");
                                    foreach (var anchor in anchors)
                                    {
                                        try
                                        {
                                            var url = anchor.Attributes["href"];
                                            if (GetScrapper(url) != null)
                                            {
                                                if(!links.ContainsKey(url))
                                                    links.Add(url, anchor.InnerText.Replace("&nbsp;", ""));
                                            }
                                            if(url.Contains("http://www.power4link.us"))
                                            {
                                                dom = GotoUrl(url);
                                                var frame = SelectItem(dom, "div.entry-content  iframe");
                                                if (!links.ContainsKey(url))
                                                    links.Add(frame.Attributes["src"], "Watch Online");
                                            }
                                        }
                                        catch { }
                                    }

                                    var iframes = SelectItems(dom, "div.entry  iframe");
                                    foreach (var iframe in iframes)
                                        links.Add(iframe.Attributes["src"], "Watch Online");
                                    if (links.Count == 0)
                                    {
                                        var k = 0;
                                    }
                                    foreach (var l in links)
                                    {
                                        try
                                        {
                                            var linkUrl = l.Key;
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
                                                {
                                                    var name = l.Value;
                                                    if (name.ToLower().Contains("part"))
                                                    {
                                                        var ind = name.IndexOf("part", StringComparison.InvariantCultureIgnoreCase);
                                                        name = "Watch " + name.Substring(ind, name.Length - ind);
                                                    }
                                                    else
                                                        name = "Watch Online";

                                                    movie.Links.Add(new ScrapedMovieLink(linkUrl, host.ID, name));
                                                }
                                            }
                                            else
                                            {
                                                OnScraperNotFound(new ScraperNotFound(linkUrl, movie.PageUrl));
                                            }
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
                catch { }
            }
            return allMovies;
        }

        private string FixTitle(string title)
        {
            var org = title;
            if (title.ToLower().StartsWith("watch"))
                title = title.Replace("watch","").Replace("Watch", "");
            var index = -1;
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
            
            for (var i = 0; i < title.Length; i++)
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
            return title.Trim();
        }

        public override string RootUrl
        {
            get { return "http://www.india4movie.com"; }
        }

        public override string ID
        {
            get { return "i4m"; }
        }

        public override string Title
        {
            get { return "I4M"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get { return  ImagePriorityRank.I4M; }
        }
    }

   
}
