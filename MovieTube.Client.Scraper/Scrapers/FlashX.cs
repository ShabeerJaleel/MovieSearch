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
    public class FlashX : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var start = elem.InnerHtml.IndexOf("vid=") + 4;
            var end = elem.InnerHtml.IndexOf("&", start);
            url = elem.InnerHtml.Substring(start, end - start);
            elem = Get(String.Format("http://play.flashx.tv/player/embed.php?vid={0}&width=620&height=400",url));


            try
            {
                var yes = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
                var sec = SelectItem(elem, "[name=sechash]").Attributes["value"].Value;
                var data = new NameValueCollection();
                data.Add("hash", yes);
                data.Add("sechash", sec);
                elem = Post("http://play.flashx.tv/player/player.php", data);

            }
            catch
            {
                var yes = SelectItem(elem, "[name=yes]").Attributes["value"].Value;
                var sec = SelectItem(elem, "[name=sec]").Attributes["value"].Value;
                var data = new NameValueCollection();
                data.Add("yes", yes);
                data.Add("sec", sec);
                elem = Post("http://play.flashx.tv/player/player.php", data);
            }

            start = elem.InnerHtml.IndexOf("config=") +7;
            end = elem.InnerHtml.IndexOf("\"", start);
            url = elem.InnerHtml.Substring(start, end - start);
            //elem = Post(new Uri(url).AbsoluteUri, data);
            var doc = new XmlDocument();
            doc.Load(new Uri(url).AbsoluteUri);
            var n = doc.SelectSingleNode("//file");
            return new Uri(n.InnerText).AbsoluteUri;
        }
     

        public override string RootUrl
        {
            get { return "http://www.flashx.tv/"; }
        }

        public override string ID
        {
            get { return "flashx.tv"; }
        }

        public override string Title
        {
            get { return "FlashX"; }
        }
        public override bool CanProcess(string url)
        {
            return base.CanProcess(url) || url.Contains("flashx.tv");
        }

        public override string SanitizeUrl(string url)
        {
            url = url.Replace("video/","");
            return url;
        }
    }
}
