using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Client.Scraper;
using BlueTube.Viewer.Data;

namespace BlueTube.Viewer.Views
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
            Initialize(new ScrapedPage
            {
                Videos = DataService.Create().GetAllFavourites()
            });
        }

        public void DeactivateView()
        {

        }

        public void Initialize(ScrapedPage page)
        {
            this.browseGalleryWidget.ClearItems();
            this.browseGalleryWidget.AddItems(page);
        }

        private void browseGalleryWidget_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            (this.FindForm() as IViewContainer).PlayVideo(e.Video);
        }


        public void TriggerSearch(SearchParameters param)
        {
            Initialize(new ScrapedPage { Videos = DataService.Create().SearchVideos(param.Query) });
        }

    }
}
