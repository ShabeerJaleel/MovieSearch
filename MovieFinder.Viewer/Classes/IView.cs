using System;
using System.Collections.Generic;
using System.Text;
using Client.Scraper;

namespace BlueTube.Viewer
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
        public ScrapedVideo Video { get; set; }
        public bool IsFavourite { get; set; }
    }

    public class PlayEventArgs : EventArgs
    {
        public ScrapedVideo Current { get; set; }
    }
}
