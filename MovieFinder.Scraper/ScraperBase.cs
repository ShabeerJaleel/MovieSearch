using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsQuery;
using System.Net;
using CsQuery.Web;

namespace MovieFinder.Scraper
{
    public abstract class ScraperBase
    {
        #region Fields

        private static readonly GotoUrlStep StepGotoUrl = new GotoUrlStep();
        private static readonly SelectStep StepSelect = new SelectStep();
        private static readonly ReadAttributeStep StepReadAttribute = new ReadAttributeStep();
        private static readonly ReadTextStep StepReadText = new ReadTextStep();

      
        
        #endregion

        protected string SubstringBetween(string text, string startText, string endText = "")
        {
            var start = text.IndexOf(startText) + startText.Length;
            var end = text.IndexOf(endText, start);
            return text.Substring(start,
                String.IsNullOrEmpty(endText) ?
                text.Length - start :
                end - start);
        }


        protected CQ GotoUrl(string url, int attempt = 1)
        {
            while (attempt-- > 0)
            {
                try
                {
                    var r = (CQ)StepGotoUrl.Process(url);
                    return r;
                }
                catch { if (attempt == 0) throw; }
            }
            throw new Exception("Network error");
        }

        protected List<IDomElement> SelectItems(CQ cq, string css)
        {
            return (List<IDomElement>)StepSelect.Process(cq, css);
        }

        protected IDomElement SelectItem(CQ cq, string css)
        {
            try
            {
                 var e = ((List<IDomElement>)StepSelect.Process(cq, css));
                 if (e.Count > 0)
                     return e[0];
                 return null;
            }
            catch
            {
                return null;
            }
        }


        //protected List<IDomElement> SelectItems(IDomElement elem, string css)
        //{
        //    return SelectItems(elem.Cq(), css);
        //}

        //protected IDomElement SelectItem(IDomElement elem, string css)
        //{
        //    return SelectItem(elem.Cq(), css);
        //}


        protected string ReadAttribute(IDomElement element, string attribute)
        {
            return StepReadAttribute.Process(element, attribute).ToString();
        }

        protected string ReadText(IDomElement element)
        {
            return WebUtility.HtmlDecode(  StepReadText.Process(element).ToString());
        }

        public abstract string RootUrl { get;}
        public abstract string ID { get; }
        public abstract string Title { get; }
        public abstract ImagePriorityRank ImagePriority { get; }
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

    public interface IScraperStep
    {
        object Process(params object[] args);
    }

    public class GotoUrlStep : IScraperStep
    {
        private ServerConfig serverConfig = new ServerConfig
        {
            TimeoutSeconds = 30,
            UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11"
        };

        public object Process(params object[] args)
        {
            if (args == null || args.Length < 1)
                throw new ArgumentException("args");
            return CQ.CreateFromUrl(args[0].ToString(), serverConfig);
        }
    }

    public class SelectStep : IScraperStep
    {
        public object Process(params object[] args)
        {
            if (args == null || args.Length < 2)
                throw new ArgumentException("args");
            return  ((CQ)args[0]).Select(args[1].ToString()).Elements.ToList();
        }
    }

    public class ReadAttributeStep : IScraperStep
    {
        public object Process(params object[] args)
        {

            if (args == null || args.Length < 2)
                throw new ArgumentException("args");
            return ((IDomElement)args[0]).Attributes[args[1].ToString()];
        }
    }

    public class ReadTextStep : IScraperStep
    {
        public object Process(params object[] args)
        {
            if (args == null || args.Length < 1)
                throw new ArgumentException("args");
            return ((IDomElement)args[0]).InnerText;
        }
    }

  


  
}
