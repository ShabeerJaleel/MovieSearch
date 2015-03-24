using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MovieTube.Web.Services
{
    public class ConfigProvider  : IConfigProvider
    {
        public string ImagePath
        {
            get { return ConfigurationManager.AppSettings["ImagePath"]; }
        }

        public string RootUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
            }
        }
    }
}