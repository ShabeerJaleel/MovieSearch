using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using System.Xml;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class NosVideo : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {

            var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("op", "download1");
            data.Add("usr_login", "");
            data.Add("id", id);
            data.Add("fname", fname);
            data.Add("method_free", "Proceed to Video");
            elem = Post(url, data);

            var start = elem.InnerText.IndexOf("|php|") + 5;
            var end = elem.InnerText.IndexOf("|", start + 1);
            var u = elem.InnerText.Substring(start, end - start);
            elem = Get("http://nosvideo.com/xml/" + u + ".xml");

            start = elem.InnerHtml.IndexOf("<file>") + 6;
            end = elem.InnerHtml.IndexOf("<", start + 1);
            u = elem.InnerHtml.Substring(start, end - start);

            return u;

        }

        

        public override string RootUrl
        {
            get { return "http://nosvideo.com"; }
        }

        public override string ID
        {
            get { return ScrapperId.NosVideo; }
        }

        public override string Title
        {
            get { return "NosVideo"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.NosVideo;
            }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("nosvideo.com");
        }

    }
}
