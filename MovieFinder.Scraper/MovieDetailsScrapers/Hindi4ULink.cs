using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Noesis.Javascript;
using System.IO;
using CsQuery;
using System.Threading.Tasks;

namespace MovieFinder.Scraper
{
    public class Hindi4ULink : MovieDetailsScraperBase
    {

        public override List<ScrapedMovie> ScrapeMovies(List<string> skipUrls, List<int> years = null)
        {
            var tasks = new List<Task>();
            try
            {
                var dom = GotoUrl(RootUrl);
                if (years == null)
                    years = new List<int>();

                foreach (var elem in SelectItems(dom, ".tagcloud a"))
                {
                    try
                    {
                        var year = Convert.ToInt32(Regex.Replace(ReadText(elem), "[^0-9.]", ""));
                        if (years.Count > 0 && !years.Any(x => x == year))
                            continue;
                        var u = ReadAttribute(elem, "href");
                        var d = GotoUrl(u);
                        tasks.Add(Task.Factory.StartNew(() => ScrapThread(d, year, u,skipUrls)));
                    }
                    catch { }
                }
            }
            catch { }

            Task.WaitAll(tasks.ToArray());
            return allMovies;
        }

        private void ScrapThread(CQ dom, int year,string startUrl, List<string> skipUrls)
        {
            var last = SelectItem(dom, ".wp-pagenavi > .last");
            var lasthRef = ReadAttribute(last, "href");
            var pos = lasthRef.LastIndexOf('/');
            var lastIndex = Int32.Parse( lasthRef.Substring(pos + 1, lasthRef.Length - pos -1) );
            var urls = new List<string>();
            
            var urlTemplate = lasthRef.Substring(0, pos);
            for (var i = lastIndex; i > 1; i--)
                urls.Add(String.Format("{0}/{1}", urlTemplate, i));
            urls.Add(startUrl);
            
            
            foreach(var url in urls)
            {
               
                try
                {
                   
                    dom = GotoUrl(url);
                    var elems = SelectItems(dom, "a.clip-link");
                    for (var i = elems.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            var subElem = elems[i];
                            var movie = new ScrapedMovie(this);
                            movie.PageUrl = ReadAttribute(subElem, "href");
                            OnNotify(new NotificationEventArgs("Processing " + movie.PageUrl + ". Year: " + year.ToString()));
                            if (movie.PageUrl.ToLower().Contains("-in-hindi") ||
                               movie.PageUrl.ToLower().Contains("-hindi."))
                                continue;

                            dom = GotoUrl(movie.PageUrl);


                            movie.ReleasedDate = new DateTime(year, 1, 1);
                            movie.LangCode = "hi";
                            movie.Language = "Hindi";
                            movie.Description = String.Empty;
                            movie.Name = ReadText(SelectItem(dom, ".entry-title")).Replace("\n", "").Replace("\t", "");
                            try
                            {
                                var descElems = SelectItems(dom, ".entry-content p");
                                //var descs = descElems.Count > 5 ? descElems.Skip(3) : descElems.Skip(2);
                                foreach (var p in descElems)
                                {
                                    if (!p.InnerHTML.Contains("<strong>"))
                                    {
                                        var t = ReadText(p);
                                        movie.Description += ReadText(p) + Environment.NewLine;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (String.IsNullOrWhiteSpace(movie.Description))
                                    movie.Description = String.Empty;
                            }
                            catch { }

                            var imgElems = SelectItems(dom, "#thumb img");
                            
                            if (imgElems.Count == 0)
                            {
                                Debug.WriteLine("No Image: " + movie.PageUrl);
                            }
                            else
                                movie.ImageUrl = ReadAttribute(imgElems[0], "src");
                            allMovies.Add(movie);

                            //links
                            var linkPages = SelectItems(dom, ".entry-content p a.external");

                            foreach (var l in linkPages)
                            {
                                var pageUrl = ReadAttribute(l, "href");
                                string linkUrl = "";
                                if (pageUrl.Contains("filmshowonline.net"))
                                {
                                    continue;
                                    dom = GotoUrl(pageUrl);

                                    IDomElement item = null;
                                    var attrib = "src";
                                    if (dom.Document.Body.InnerHTML.Contains("id=\"cipher\""))
                                    {
                                        var html = DecryptLink(ReadAttribute(SelectItem(dom, "#key"), "value"),
                                            ReadAttribute(SelectItem(dom, "#cipher"), "value"));
                                        var doc = CsQuery.CQ.CreateDocument(html);
                                        item = SelectItem(doc, "iframe");
                                        if (item == null)
                                            item = SelectItem(doc, "embed");
                                        if (item == null)
                                        {
                                            item = SelectItems(doc, "object param").FirstOrDefault(x => x.Attributes["name"] == "movie");
                                            if (item != null)
                                                attrib = "value";
                                        }
                                        if (html.Contains("flashvars"))
                                        {
                                            html = System.Web.HttpUtility.UrlDecode(html);
                                            linkUrl = System.Web.HttpUtility.UrlDecode(SubstringBetween(html, "&url=", "&"));
                                        }
                                    }

                                    if (item == null)
                                        item = SelectItems(dom, "center embed").FirstOrDefault(x => x.HasAttribute("allowfullscreen"));
                                    if (item == null)
                                        item = SelectItems(dom, "center iframe").FirstOrDefault(x => x.HasAttribute("allowfullscreen"));


                                    if (item == null)
                                    {
                                        OnScraperNotFound(new ScraperNotFound("No Link", pageUrl));
                                        continue;
                                    }
                                    if (String.IsNullOrWhiteSpace(linkUrl))
                                        linkUrl = ReadAttribute(item, attrib);
                                }
                                else if (pageUrl.Contains("www.veoh.com/download"))
                                {
                                    continue;
                                }
                                else
                                    linkUrl = pageUrl;

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
                                        if (!movie.Links.Any(x => x.DownloadUrl.ToLower() == linkUrl.ToLower()))
                                        {
                                            MovieTube.Client.Scraper.ScraperResult result = MovieTube.Client.Scraper.ScraperResult.Success;
                                            try
                                            {
                                                result = MovieTube.Client.Scraper.VideoScraperBase.ValidateUrl(linkUrl);
                                            }
                                            catch { }
                                            if (result != MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist)
                                                movie.Links.Add(new ScrapedMovieLink(linkUrl, host.ID, l.InnerText));
                                        }
                                    }
                                    else
                                    {
                                        OnScraperNotFound(new ScraperNotFound(linkUrl, pageUrl));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    OnScraperNotFound(new ScraperNotFound("Exception", ex.Message));
                                }
                            }
                            if (movie.Links.Count > 0)
                            {
                                var args = new MovieFoundEventArgs(movie);
                                OnMovieFound(args);

                            }
                            if (this.stop)
                                return;
                        }
                        catch (Exception ex)
                        {
                            OnScraperNotFound(new ScraperNotFound("Exception", ex.Message));
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnScraperNotFound(new ScraperNotFound("Exception", ex.Message));
                }
            }

        }


        public string DecryptLink(string key, string cipher)
        {
            using (var context = new JavascriptContext())
            {
               // context.SetParameter("Console", new SystemConsole());
                context.SetParameter("key", key);
                context.SetParameter("cipher", cipher);
               

                // Running the script
                context.Run(File.ReadAllText("Hindi4ULink.js"));
                

                // Getting a parameter
                return context.GetParameter("decryptedText").ToString();
            }
        }

        public override string RootUrl
        {
            get { return "http://www.hindilinks4u.to/"; }
        }

        public override string ID
        {
            get { return "hl4u"; }
        }

        public override string Title
        {
            get { return "HLFU"; }
        }

        public override ImagePriorityRank ImagePriority
        {
            get
            {
                return ImagePriorityRank.HL4U;
            }
        }

    }
}
