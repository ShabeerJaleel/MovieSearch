using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTube.Web.Services
{
    public class ImageUrlBuilder : IImageUrlBuilder
    {
        private readonly IConfigProvider confProvider;

        public ImageUrlBuilder()
        {
             this.confProvider = new ConfigProvider();
        }
        public string Build(string imgPath)
        {
            Uri uri;
            if (Uri.TryCreate(new Uri(confProvider.RootUrl), confProvider.ImagePath, out uri))
                Uri.TryCreate(uri, imgPath, out uri);
            return uri.AbsoluteUri;

            //http://localhost:29922/\Contenet\img\poster\/2014\badlapur-2014-hi.jpg
            //return String.Format("{0}/{1}/{2}", confProvider.RootUrl,
            //   confProvider.ImagePath, imgPath);
        }
    }
}