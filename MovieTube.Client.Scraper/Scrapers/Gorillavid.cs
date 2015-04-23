using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class GorillaVid : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {

            var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("channel", "");
            data.Add("fname", fname);
            data.Add("id", id);
            data.Add("method_free", "Free Download");
            data.Add("op", "download1");
            data.Add("usr_login", "");
            data.Add("referer", "");
            //Thread.Sleep(6000);
            elem = Post(url, data);
            url = SubstringBetween(elem.InnerHtml, "file: \"", "\"");
            return new Uri(url).AbsoluteUri;
        }

        public override string RootUrl
        {
            get { return "http://gorillavid.in"; }
        }

        public override string ID
        {
            get { return ScrapperId.GorillaVid; }
        }

        public override string Title
        {
            get { return "GorillaVid"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.GorillaVid;
            }
        }
    }
}
