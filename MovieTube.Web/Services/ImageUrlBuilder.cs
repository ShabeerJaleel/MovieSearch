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
            return String.Format("{0}/{1}/{2}", confProvider.RootUrl,
               confProvider.ImagePath, imgPath);
        }
    }
}