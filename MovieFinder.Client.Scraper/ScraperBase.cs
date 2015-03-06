using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using Fizzler.Systems.HtmlAgilityPack;
using System.Linq;

namespace  Client.Scraper
{
    public abstract class ScraperBase
    {
        #region Fields

      
        #endregion


        protected HtmlNode GotoUrl(string url)
        {
            //HtmlDocument doc = new HtmlDocument();
            //using (var client = new WebClient())
            //{
            //    //doc.Load(client.OpenRead(url));
            //    byte[] myDataBuffer = client.DownloadData(url);
            //    string download = Encoding.ASCII.GetString(myDataBuffer);
            //    doc.LoadHtml2(download);
            //    return doc.DocumentNode;
            //}
            
            
            return new HtmlWeb().Load(url).DocumentNode;
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
    }
  
}
