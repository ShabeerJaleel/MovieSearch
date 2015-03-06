using System;
using System.Collections.Generic;
using System.Text;
using Client.Scraper;

namespace BlueTube.Viewer
{
    interface IViewContainer
    {
        void PlayVideo(ScrapedVideo video);
        IView ActiveView { get; }

    }
}
