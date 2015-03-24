using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieTube.Web.Services
{
    public interface IConfigProvider
    {
        string ImagePath { get; }
        string RootUrl { get; }
    }
}