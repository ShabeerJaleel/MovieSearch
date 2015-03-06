using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using HtmlAgilityPack;

namespace MovieTube.Client.Scraper
{
    public class MuchShare : VideoScraperBase
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

            var rand = SelectItem(elem, "[name=rand]").Attributes["value"].Value;
            data.Clear();
            data.Add("op", "download2");
            data.Add("rand", rand);
            data.Add("id", id);
            data.Add("fname", fname);
            data.Add("method_free", "Proceed to Video");
            data.Add("method_premium", "");
            Thread.Sleep(45000);
            elem = Post(url, data);
            url = new Uri(SelectItem(elem, "#lnk_download").Attributes["href"].Value).AbsoluteUri;

            return url;
        }

        public override ScraperResult IsValid(string url)
        {
            var elem = Get(url);

            try
            {
                var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
                var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
                return  ScraperResult.Success;
            }
            catch (Exception ex)
            {
                return IsVideoRemoved(elem.InnerText) ? ScraperResult.VideoDoesNotExist : ScraperResult.NetworkError;
            }
        }

        public override string RootUrl
        {
            get { return "http://muchshare.net"; }
        }

        public override string ID
        {
            get { return "muchshare.net"; }
        }

        public override string Title
        {
            get { return "MuchShare"; }
        }
    }
}
