using System;
using System.Collections.Generic;
using System.Text;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer
{
    interface IView
    {
        void ActivateView();
        void DeactivateView();
        void TriggerSearch(SearchParameters param);
    }

    interface IFavouriteEnabledView : IView
    {
        event EventHandler<ToggleFavouriteEventArgs> ToggelFavourite;
    }

    public class ToggleFavouriteEventArgs : EventArgs
    {
        public Movie Video { get; set; }
        public bool IsFavourite { get; set; }
    }

    public class PlayEventArgs : EventArgs
    {
        public Movie Current { get; set; }
    }
}
