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
    public class LoboVideo : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            //var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            //var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            //var data = new NameValueCollection();
            //data.Add("op", "download1");
            //data.Add("usr_login", "");
            //data.Add("id", id);
            //data.Add("fname", fname);
            //data.Add("method_free", "Proceed to Video");
            //elem = Post(url, data);

            var start = elem.InnerText.IndexOf("|image|") + 7;
            var end = elem.InnerText.IndexOf("|", start + 1);
            var extension = elem.InnerText.Substring(start, end - start);

            start = elem.InnerText.IndexOf("|"+ extension + "|") + extension.Length + 2;
            end = elem.InnerText.IndexOf("|", start + 1);
            var id = elem.InnerText.Substring(start, end - start);

            url = String.Format("http://lobovideo.com/cgi-bin/dl.cgi/{0}/video.{1}", id, extension);
            return url;

        }

     

        public override string RootUrl
        {
            get { return "http://lobovideo.com/"; }
        }

        public override string ID
        {
            get { return ScrapperId.LoboVideo; }
        }

        public override string Title
        {
            get { return "LoboVideo"; }
        }
    }
}
