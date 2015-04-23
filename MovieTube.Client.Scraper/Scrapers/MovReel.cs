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
    public class MovReel : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var data = new NameValueCollection();
            data.Add("op", SelectItem(elem, "[name=op]").Attributes["value"].Value);
            data.Add("id", SelectItem(elem, "[name=id]").Attributes["value"].Value);
            data.Add("rand", SelectItem(elem, "[name=rand]").Attributes["value"].Value);
            data.Add("method_free", "Proceed to Video");
            elem = Post(url, data);


            url  = SelectItem(elem, ".span12 span a").Attributes["href"].Value;
            return url;
        }

      
        public override string RootUrl
        {
            get { return "http://movreel.com"; }
        }

        public override string ID
        {
            get { return ScrapperId.MovReel; }
        }

        public override string Title
        {
            get { return "MovReel"; }
        }
    }
}
