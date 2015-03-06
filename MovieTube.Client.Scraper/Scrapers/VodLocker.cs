using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class VodLocker : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var u = TryGetUrl(url, elem);
            if (!String.IsNullOrEmpty(u))
                return u;
            var hash = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
            var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("op", "download1");
            data.Add("usr_login", "");
            data.Add("hash", hash);
            data.Add("id", id);
            data.Add("referer", "");
            data.Add("imhuman", "Proceed to video");
            var dUrl = String.Empty;
            var count = 0;
            while (count++ < 2)
            {
                elem = Post(url, data);

                dUrl = SubstringBetween(elem.InnerHtml, "file: \"", "\"");
                if (String.IsNullOrEmpty(dUrl))
                    Thread.Sleep(5000);
                else
                    break;
            }

            return new Uri(dUrl).AbsoluteUri;
        }

        public override ScraperResult IsValid(string url)
        {
            var elem = Get(url);
            try
            {
                var u = TryGetUrl(url, elem);
                if (!String.IsNullOrEmpty(u))
                    return  ScraperResult.Success;

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
            get { return "http://vodlocker.com"; }
        }

        public override string ID
        {
            get { return "vodlocker.com"; }
        }

        public override string Title
        {
            get { return "VodLocker"; }
        }

        public override ScraperRank Rank
        {
            get
            {
                return ScraperRank.VodLocker;
            }
        }

        public override string SanitizeUrl(string url)
        {
            if (url.Contains("embed-"))
                return url;

            return String.Format("http://vodlocker.com/embed-{0}.html", SubstringBetween(url, ".com/"));
        }

        public override string GetFlashUrl(string url)
        {
            //http://vodlocker.com/ikmd5u587nt2
            if (url.Contains("/embed"))
            {
                var id = SubstringBetween(url, "embed-","-");
                if(String.IsNullOrEmpty(id))
                    id = SubstringBetween(url, "embed-",".");
                url = String.Format("http://vodlocker.com/{0}", id);
            }
            return url;
        }
    }
}
