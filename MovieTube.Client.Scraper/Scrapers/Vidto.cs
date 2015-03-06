using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class Vidto : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {

            var hash = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
            var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("op", "download1");
            data.Add("usr_login", "");
            data.Add("hash", hash);
            data.Add("id", id);
            data.Add("imhuman", "Proceed to video");
            Thread.Sleep(6000);
            elem = Post(url, data);
            //var start = elem.InnerHtml.IndexOf("var file_link = '") + 17;
            //var end = elem.InnerHtml.IndexOf("'", start);

            //url = new Uri(elem.InnerHtml.Substring(start, end - start)).AbsoluteUri;
             
            var  eval = SubstringBetween(elem.InnerHtml, "eval", "</scr");
            eval = "eval" + eval;
            eval = eval.Replace("\"", "\\x22");
            url = SubstringBetween(UnpackScript(Properties.Resources.Unpacker.Replace("X", eval)), "file:\"", "\"");
            return new Uri(url).AbsoluteUri;
        }

        public override ScraperResult IsValid(string url)
        {
            var elem = Get(url);
            try
            {
                var hash = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
                var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
                var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
                return ScraperResult.Success;
            }
            catch (Exception ex)
            {
                return IsVideoRemoved(elem.InnerText) ?  ScraperResult.VideoDoesNotExist : ScraperResult.NetworkError ;
            }
        }

        public override string RootUrl
        {
            get { return "http://vidto.me"; }
        }

        public override string ID
        {
            get { return "vidto.me"; }
        }

        public override string Title
        {
            get { return "VidTo"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.Vidto;
            }
        }

        public override string SanitizeUrl(string url)
        {
            if (url.Contains("embed-"))
            {
                var s = SubstringBetween(url, "embed-", "-");
                if(String.IsNullOrEmpty(s))
                    s = SubstringBetween(url, "embed-", ".");
                url = String.Format("http://vidto.me/{0}", s);
            }
            return url;
        }

    }
}
