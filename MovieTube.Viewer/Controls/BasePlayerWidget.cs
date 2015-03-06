using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MovieTube.Client.Scraper;
using MovieTube.Viewer.Data;

namespace MovieTube.Viewer
{
    public partial class BasePlayerWidget : UserControl
    {
        protected MovieLink currentPlayingVideo;
        protected ClientDataService dataService;
        public event EventHandler<ToggleFavouriteEventArgs> ToggelFavourite;

        public BasePlayerWidget()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                dataService = ClientDataService.Single;
            }
        }

        protected bool ToggleFavourite()
        {
            if (this.currentPlayingVideo != null)
            {
                var fav = dataService.IsFavourite(this.currentPlayingVideo.Parent);
                if (!fav)
                    dataService.AddToFavourite(this.currentPlayingVideo.Parent);
                else
                    dataService.DeleteFromFavourite(this.currentPlayingVideo.Parent);

                if (this.ToggelFavourite != null)
                    this.ToggelFavourite(this, new ToggleFavouriteEventArgs
                    {
                        IsFavourite = !fav,
                        Video = this.currentPlayingVideo.Parent
                    });
                return !fav;
            }
            return false;

        }
    }
}
