using System;
using System.Collections.Generic;
using System.Text;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer
{
    interface IViewContainer
    {
        void PlayVideo(Movie video);
        IView ActiveView { get; }

    }
}
