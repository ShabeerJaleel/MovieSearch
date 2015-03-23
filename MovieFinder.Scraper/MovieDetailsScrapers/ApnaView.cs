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


    public class ApnaView : MovieDetailsScraperBase
    {
        private List<string> HindiRootLinks = new List<string>();
        private List<string> TamilRootLinks = new List<string>();
        private List<string> TeluguRootLinks = new List<string>();


        public ApnaView()
        {
            for(var i = 2; i > 0; i--)
                HindiRootLinks.Add("http://apnaview.com/browse/hindi?page=" + i);
            for (var i = 2; i > 0; i--)
                TamilRootLinks.Add("http://apnaview.com/tamil/tamil?page=" + i);
            for (var i = 2; i > 0; i--)
                TeluguRootLinks.Add("http://apnaview.com/telugu/telugu?page=" + i);

        }
        
        public override List<ScrapedMovie> ScrapeMovies( List<string> skipUrls, List<int> years = null)
        {
            DoScrapeMovies(HindiRootLinks,"hi", skipUrls, years);
            DoScrapeMovies(TamilRootLinks,"ta", skipUrls, years);
            DoScrapeMovies(TeluguRootLinks,"te", skipUrls, years);
            return allMovies;
        }
        
        private List<ScrapedMovie> DoScrapeMovies(List<string> links, string langCode, List<string> skipUrls, List<int> years = null)
        {
            if (years == null)
                years = new List<int>();


            foreach (var entry in links)
            {
                var dom = GotoUrl(entry);
                var movies = SelectItems(dom, ".movie");
                foreach (var m in movies)
                {
                    try
                    {
                        var movie = new ScrapedMovie(this);
                        allMovies.Add(movie);
                        movie.PageUrl = "http://apnaview.com" + m.FirstElementChild.Attributes["href"];
                        var children = m.FirstElementChild.ChildElements.ToList();
                        movie.ReleasedDate = new DateTime(Convert.ToInt32(children[2].InnerText), 1, 1);
                        OnNotify(new NotificationEventArgs("Processing " + movie.PageUrl + ". Year: " + movie.ReleasedDate.Year.ToString()));
                        movie.LangCode = langCode;
                        movie.Name = children[1].InnerText;
                        if(children[0].Attributes["src"].Contains("/img"))
                            movie.ImageUrl = "http://apnaview.com" + children[0].Attributes["src"];

                        dom = GotoUrl(movie.PageUrl);
                        var vids = SelectItems(dom, ".table.table-bordered tbody tr");
                        foreach (var vid in vids)
                        {
                            try
                            {
                                var vidLinks = vid.ChildElements.ToList()[1].ChildElements.ToList();
                                foreach (var vl in vidLinks)
                                {
                                    
                                    var linkUrl = vl.Attributes["href"];
                                    if (GetScrapper(linkUrl) == null)
                                    {
                                        linkUrl = String.Empty;
                                        dom = GotoUrl(vl.Attributes["href"]);
                                        try
                                        {
                                            linkUrl = SelectItem(dom, ".videoplayer iframe").Attributes["src"];
                                        }
                                        catch { }
                                        try
                                        {
                                            if (String.IsNullOrWhiteSpace(linkUrl))
                                                linkUrl = SelectItem(dom, ".videoplayer embed").Attributes["src"];
                                        }
                                        catch { }
                                    }

                                    if (IgnoreLink(linkUrl))
                                        continue;

                                    try
                                    {
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
                                                var name = vl.InnerText.Trim();
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


            return allMovies;
        }

        private string FixTitle(string title)
        {
            var org = title;
            if (title.ToLower().StartsWith("watch"))
                title = title.Replace("watch", "").Replace("Watch", "");
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
            get { return "http://www.apnaview.com"; }
        }

        public override string ID
        {
            get { return "apv"; }
        }

        public override string Title
        {
            get { return "apv"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get { return ImagePriorityRank.ApnaView; }
        }
    }


}
