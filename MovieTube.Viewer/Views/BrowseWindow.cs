using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MovieTube.Client.Scraper;
using MovieTube.Viewer.Data;

namespace MovieTube.Viewer
{
    public partial class BrowseWindow : UserControl,IView
    {
        public BrowseWindow()
        {
            InitializeComponent();
        }

        public void ActivateView()
        {
            this.browseGalleryWidget.RefreshView();
            this.BringToFront();
            
        }

        public void DeactivateView()
        {
            
        }

        public void Initialize(MoviePage page)
        {
            this.browseGalleryWidget.ClearItems();
            this.browseGalleryWidget.AddItems(page);
        }

        public void OnScrapVideoCompleted(MoviePage page)
        {
            Program.SetIdle();
            this.InvokeEx(() =>
            {
                Initialize(page);
            });
            
        }

        public void OnScrapVideoDetailsCompleted(Movie video)
        {
            
        }
        public void OnScrapError(Exception ex)
        {
            Program.SetIdle();
            KryptonMessageBox.Show("An error occurred: " + ex.Message, Constants.AppTitle, 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void browseGalleryWidget_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            //ugly.. should have a IViewContainer to route
            (this.FindForm() as IViewContainer).PlayVideo(e.Data as Movie);
        }


        public void TriggerSearch(SearchParameters param)
        {
            Program.SetBusy();
            Initialize(ClientDataService.Single.GetAllMovies(param));
            Program.SetIdle();
        }

        private void browseGalleryWidget_PageSelected(object sender, GalleryPageSelectedEventArgs e)
        {
            //new ScraperService().ScrapVideosAsync(this, new SearchParameters
            //{
            //     Url = e.Link.Url
            //}); 
        }

        public void OnScrapVideoDetailsCompleted(MovieLink link)
        {
            throw new NotImplementedException();
        }
    }
}
