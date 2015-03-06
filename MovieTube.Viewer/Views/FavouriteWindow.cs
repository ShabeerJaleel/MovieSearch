using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MovieTube.Viewer.Data;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer.Views
{
    public partial class FavouriteWindow : UserControl,IView
    {
        public FavouriteWindow()
        {
            InitializeComponent();
        }

        public void ActivateView()
        {
            this.BringToFront();
            Initialize(ClientDataService.Single.GetAllFavourites());
        }

        public void DeactivateView()
        {

        }

        public void Initialize(MoviePage page)
        {
            this.browseGalleryWidget.ClearItems();
            this.browseGalleryWidget.AddItems(page);
        }

        private void browseGalleryWidget_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            (this.FindForm() as IViewContainer).PlayVideo(e.Data as Movie);
        }


        public void TriggerSearch(SearchParameters param)
        {
            Initialize(new MoviePage { Videos = ClientDataService.Single.SearchFavourites(param) });
        }

        private void browseGalleryWidget_ItemFavourited(object sender, GalleryItemFavouriteEventArgs e)
        {
            Initialize(ClientDataService.Single.GetAllFavourites());
        }

    }
}
