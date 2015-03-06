using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;
using System.Net;
using System.Linq;
using System.IO;

namespace MovieTube.Client.Scraper
{
    public class TheVideo : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {

            var hash = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
            var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            var inhu = SelectItem(elem, "[name=inhu]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("_vhash", SubstringBetween(elem.InnerHtml, "name: '_vhash', value: '", "'"));
            data.Add("fname", fname);
            data.Add("gfk", SubstringBetween(elem.InnerHtml, "name: 'gfk', value: '", "'"));
            data.Add("hash", hash);
            data.Add("id", id);
            data.Add("imhuman", "Proceed to video");
            data.Add("inhu", inhu);
            data.Add("op", "download1");
            data.Add("referer", "");
            data.Add("usr_login", "");

            var cookies = new CookieCollection();
            cookies.Add(new Cookie("file_id", SubstringBetween(elem.InnerHtml, "'file_id', '", "'")) { Domain = "www.thevideo.me" });
            cookies.Add(new Cookie("aff", SubstringBetween(elem.InnerHtml, "'aff', '", "'")) { Domain = "www.thevideo.me" });
            cookies.Add(new Cookie("lang", "1") { Domain = "www.thevideo.me" });
            //cookies.Add(new Cookie("ref_url", url) { Domain = "www.thevideo.me" });
            //cookies.Add(new Cookie("mlUserID", "dtwVzu8bAWsc") { Domain = "www.thevideo.me" });
            //cookies.Add(new Cookie("__cfduid", "d629171dd342b852f8ddabc37c85b978e1406816048462") { Domain = ".thevideo.me" });
            var nv = new NameValueCollection();
            nv.Add("Referer", url);
            var c = 0;
            var eval = String.Empty;
            var text = Properties.Resources.Unpacker;
            while (c++ < 6)
            {
                elem = Post(url, data, cookies, nv);
                eval = SubstringBetween(elem.InnerHtml, "eval", "</");
                if (!String.IsNullOrEmpty(eval))
                    break;
                Thread.Sleep(1000);
            }
            
            eval = "eval" + eval;
            eval = eval.Replace("\"", "\\x22");
            url = SubstringBetween(UnpackScript(text.Replace("X", eval)), "file:'", "'");
            return new Uri(url).AbsoluteUri;
        }

       

        public override string RootUrl
        {
            get { return "http://www.thevideo.me"; }
        }

        public override string ID
        {
            get { return "thevideo.me"; }
        }

        public override string Title
        {
            get { return "TheVideo"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.TheVideo;
            }
        }

        public override bool CanProcess(string url)
        {
            if (base.CanProcess(url))
                return true;
            return url.ToLower().Contains("thevideo.");
        }

        //public override string SanitizeUrl(string url)
        //{
        //    if (url.Contains("embed-"))
        //        return url;

        //    return String.Format("http://www.thevideo.me/embed-{0}.html?play=1&confirm=Close+Ad+and+Watch+as+Free+User", SubstringBetween(url, ".com/"));
        
        //}
    }
}
