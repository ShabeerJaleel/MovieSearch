using System;
using System.Collections.Generic;
using System.Text;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer
{
    interface IPlayerWidget : IView
    {
        bool PlayStart(MovieLink link, bool start = true);
        void PlayStop();
        void ShowBuffering();
        void HideBuffering();
        PlayerType PlayerType { get; }

    }
}
