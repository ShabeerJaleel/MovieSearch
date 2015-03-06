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
   

    public class ThriruttuVCD : MovieDetailsScraperBase
    {
        private Dictionary<string, int> RootLinks = new Dictionary<string,int>();

        public ThriruttuVCD()
        {
            RootLinks.Add("http://www.thiruttuvcd.me/category/tamil-movies-online/watch-2009-movies/", 2009);
            RootLinks.Add("http://www.thiruttuvcd.me/category/tamil-movies-online/watch-2010-movies/", 2010);
            RootLinks.Add("http://www.thiruttuvcd.me/category/tamil-movies-online/watch-2011-movies/", 2011);
            RootLinks.Add("http://www.thiruttuvcd.me/category/tamil-movies-online/2012-movies/", 2012);
            RootLinks.Add("http://www.thiruttuvcd.me/category/tamil-movies-online/watch-new-tamil-movies-online/", 2013);
            RootLinks.Add("http://www.thiruttuvcd.me/category/tamil-movies-online/2014-movies/", 2014);
            
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

                    var elems = SelectItems(dom, ".wp-pagenavi a").Where(x => x.Attributes["class"] == "page larger" || x.Attributes["class"] == "page smaller").ToList();
                    var pageUrls = new List<string>();
                    for (var i = elems.Count - 1; i >= 0; i--)
                        pageUrls.Add(new Uri(new Uri(entry.Key), ReadAttribute(elems[i], "href")).AbsoluteUri);
                    pageUrls.Add(entry.Key);
                    foreach (var elem in pageUrls)
                    {
                        try
                        {
                            dom = GotoUrl(elem);
                            var postBoxes = SelectItems(dom, ".boxentry a");
                            for (var j = postBoxes.Count - 1; j >= 0; j--)
                            {
                                try
                                {
                                    var pb = postBoxes[j];

                                    var movie = new ScrapedMovie(this);
                                    allMovies.Add(movie);
                                    movie.PageUrl = ReadAttribute(pb, "href");
                                    OnNotify(new NotificationEventArgs("Processing " + movie.PageUrl + ". Year: " + year.ToString()));
                                    movie.LangCode = "ta";
                                    movie.ReleasedDate = new DateTime(year, 1, 1);
                                    var title = ReadAttribute(pb, "title");
                                    if (title.ToLower().Contains("dubbed"))
                                        continue;
                                    movie.Name = FixTitle(title);
                                    movie.ImageUrl = pb.FirstElementChild.Attributes["src"];

                                    dom = GotoUrl(movie.PageUrl);
                                    try
                                    {
                                        //movie.Description = SelectItem(dom, "meta[name='description']").InnerText;
                                    }
                                    catch
                                    {
                                        //try
                                        //{
                                        //    movie.Description = SelectItem(dom, ".textsection").InnerText;
                                        //}
                                        //catch { }
                                    }
                                    var embedds = SelectItems(dom, ".videosection embed");
                                    var iframes = SelectItems(dom, ".videosection iframe");
                                    var links = new List<string>();
                                    foreach (var emb in embedds)
                                        links.Add(emb.Attributes["src"]);
                                    foreach (var iframe in iframes)
                                        links.Add(iframe.Attributes["src"]);
                                    if (links.Count == 0)
                                    {
                                        var k = 0;
                                    }
                                    foreach (var l in links)
                                    {
                                        try
                                        {
                                            var linkUrl = l;
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
                                                    movie.Links.Add(new ScrapedMovieLink(linkUrl, host.ID, "Watch Full Movie"));
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
            //File.AppendAllText(@"C:\tvcd.txt", String.Format("Org: {0}, Modified: {1}{2}",org, title.Trim().ToUpper(), Environment.NewLine));
            return title;
        }

        public override string RootUrl
        {
            get { return "http://www.thiruttuvcd.me/"; }
        }

        public override string ID
        {
            get { return "tvcd"; }
        }

        public override string Title
        {
            get { return "TVCD"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get { return  ImagePriorityRank.TVCD; }
        }
    }

   
}
