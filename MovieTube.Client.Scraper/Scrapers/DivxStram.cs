using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;
using System.Web;
using System.Threading;
using System.Xml;
using HtmlAgilityPack;
//using Noesis.Javascript;
using System.IO;


namespace MovieTube.Client.Scraper
{
    public class DivxStream : VideoScraperBase
    {
        protected override string OnScrape(string url, HtmlNode elem)
        {
            var hash = SelectItem(elem, "[name=hash]").Attributes["value"].Value;
            var id = SelectItem(elem, "[name=id]").Attributes["value"].Value;
            var fname = SelectItem(elem, "[name=fname]").Attributes["value"].Value;
            var data = new NameValueCollection();
            data.Add("op", "download1");
            data.Add("usr_login", "");
            data.Add("referer", "");
            data.Add("hash", hash);
            data.Add("id", id);
            data.Add("imhuman", "Continue to video");
            data.Add("fname", fname);
            Thread.Sleep(3000);
            elem = Post(url, data);


           // var tokens = SubstringBetween(elem.InnerHtml, "|http|", "'").Split('|');
           // //var packed = "eval" + SubstringBetween(elem.InnerHtml, "eval", "</script>");
           //// Unpack(packed);
            
           // //url = String.Format("http://{0}.{1}.{2}.{3}/d/{4}/{5}.{6}?start=0",
           // //    tokens[6], tokens[5], tokens[4], tokens[3], tokens[12], tokens[11], tokens[10]);

           // url = SelectItem(elem, "#vplayer img").Attributes["src"].Value;
            var eval = SubstringBetween(elem.InnerHtml, "eval", "</scr");
            eval = "eval" + eval;
            eval = eval.Replace("\"", "\\x22");
            url = SubstringBetween(UnpackScript(Properties.Resources.Unpacker.Replace("X", eval)), "file:\"", "\"");
            return new Uri(url).AbsoluteUri;
        }

       

        public override string RootUrl
        {
            get { return "http://divxstream.net"; }
        }

        public override string ID
        {
            get { return "divxstream.net"; }
        }

        public override string Title
        {
            get { return "DivxStream"; }
        }

        //private string Unpack(string packed)
        //{
        //    using (var context = new JavascriptContext())
        //    {
        //        context.SetParameter("packed", packed);

        //        // Running the script
        //        context.Run(File.ReadAllText("unpacker.js"));

        //        // Getting a parameter
        //        return context.GetParameter("unpacked").ToString();
        //    }
        //}

    }
}
