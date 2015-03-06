using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Client.Scraper;
using ComponentFactory.Krypton.Toolkit;

namespace BlueTube.Viewer
{
    public partial class BrowseWindow : UserControl, IScraperServiceCallback,IView
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

        public void Initialize(ScrapedPage page)
        {
            this.browseGalleryWidget.ClearItems();
            this.browseGalleryWidget.AddItems(page);
        }

        public void OnScrapVideoCompleted(ScrapedPage page)
        {
            Program.SetIdle();
            this.InvokeEx(() =>
            {
                Initialize(page);
            });
            
        }

        public void OnScrapVideoDetailsCompleted(ScrapedVideo video)
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
            (this.FindForm() as IViewContainer).PlayVideo(e.Video);
        }


        public void TriggerSearch(SearchParameters param)
        {
            Program.SetBusy();
            new ScraperService().ScrapVideosAsync(this, param); 
        }

        private void browseGalleryWidget_PageSelected(object sender, GalleryPageSelectedEventArgs e)
        {
            new ScraperService().ScrapVideosAsync(this, new SearchParameters
            {
                 Url = e.Link.Url
            }); 
        }
    }
}
