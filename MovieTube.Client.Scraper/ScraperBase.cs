using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;
using System.Collections.Specialized;

namespace  MovieTube.Client.Scraper
{
    public abstract class ScraperBase
    {
        #region Fields

        private WebClientEx client = new WebClientEx();
        #endregion

        public static string Substring(string text, string startText, string endText = null)
        {
            try
            {
                var start = text.IndexOf(startText) + startText.Length;
                if (start < startText.Length)
                    return null;
                if (endText == null)
                    return text.Substring(start, text.Length - start);

                var end = text.IndexOf(endText, start);
                if (end == -1)
                    return null;
                return text.Substring(start, end - start);
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected string SubstringBetween(string text, string startText, string endText = null)
        {
            return ScraperBase.Substring(text, startText, endText);
        }

        protected HtmlNode Get(string url)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(client.DownloadString(url));
            return doc.DocumentNode;
        }

        protected HtmlNode GetFromHtml(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode;
        }

        protected HtmlNode Post(string url, NameValueCollection data, CookieCollection cookies = null, NameValueCollection headers = null)
        {
            System.Net.ServicePointManager.Expect100Continue = false;
            if (cookies != null)
                client.AddCookies(cookies);
            if (headers != null)
                client.AddHeaders(headers);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(Encoding.ASCII.GetString(client.UploadValues(url, data)));
            return doc.DocumentNode;
        }

        protected IEnumerable<HtmlNode> SelectItems(HtmlNode cq, string css)
        {
            return cq.QuerySelectorAll(css);
        }

        protected HtmlNode SelectItem(HtmlNode cq, string css)
        {
            return cq.QuerySelector(css);
        }

        protected string ReadAttribute(HtmlNode element, string attribute)
        {
            return element.Attributes[attribute] != null ? element.Attributes[attribute].Value : String.Empty;
        }

        protected string ReadText(HtmlNode element)
        {
            return element.InnerText;// WebUtility.HtmlDecode(StepReadText.Process(element).ToString());
        }

        public abstract string RootUrl { get;}
        public abstract string ID { get; }
        public abstract string Title { get; }
        public virtual bool CanProcess(string url)
        {
            return url.ToLower().Contains(RootUrl.ToLower());
        }

        public virtual bool IsWebSupported
        {
            get
            {
                return true;
            }
        }

        public virtual bool IsDesktopSupported
        {
            get
            {
                return true;
            }
        }
    }

    public class WebClientEx : WebClient
    {
        

        private readonly CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            var request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = container;
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            var response = r as HttpWebResponse;
            if (response != null)
            {
                CookieCollection cookies = response.Cookies;
                container.Add(cookies);
            }
        }

        public void AddCookies(CookieCollection cookies)
        {
            foreach(Cookie c in cookies)
                container.Add(c);
        }

        public void AddHeaders(NameValueCollection headers)
        {
            foreach (string key in headers)
                this.Headers.Add(key,headers[key]);
        }
    }
  
}
