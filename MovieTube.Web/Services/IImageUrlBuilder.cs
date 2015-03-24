using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTube.Web.Services
{
    public interface IImageUrlBuilder
    {
        string Build(string imgPath);
    }
}