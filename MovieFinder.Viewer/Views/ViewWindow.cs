using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using Client.Scraper;


namespace BlueTube.Viewer
{
    public partial class ViewWindow : UserControl, IScraperServiceCallback, IView
    {
        #region Fields

        private int widthLeftRight;
        private ScrapedVideo currentRequestedVideo;
        private ScrapedPage lastPage;

        #endregion

        #region Constrctor


        public ViewWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Events


        private void buttonSpecHeaderGroupSearchResult_Click(object sender, EventArgs e)
        {
            // Suspend layout changes until all splitter properties have been updated
            kryptonSplitContainerMain.SuspendLayout();

            // Is the left header group currently expanded?
            if (kryptonSplitContainerMain.FixedPanel == FixedPanel.None)
            {
                // Make the left panel of the splitter fixed in size
                kryptonSplitContainerMain.FixedPanel = FixedPanel.Panel1;
                kryptonSplitContainerMain.IsSplitterFixed = true;

                // Remember the current height of the header group
                widthLeftRight = headerGroupSearchResult.Width;

                // We have not changed the orientation of the header yet, so the width of 
                // the splitter panel is going to be the height of the collapsed header group
                int newWidth = headerGroupSearchResult.PreferredSize.Height;

                // Make the header group fixed just as the new height
                kryptonSplitContainerMain.Panel1MinSize = newWidth;
                kryptonSplitContainerMain.SplitterDistance = newWidth;

                // Change header to be vertical and button to near edge
                headerGroupSearchResult.HeaderPositionPrimary = VisualOrientation.Right;
                headerGroupSearchResult.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
            }
            else
            {
                // Make the bottom panel not-fixed in size anymore
                kryptonSplitContainerMain.FixedPanel = FixedPanel.None;
                kryptonSplitContainerMain.IsSplitterFixed = false;

                // Put back the minimise size to the original
                kryptonSplitContainerMain.Panel1MinSize = 100;

                // Calculate the correct splitter we want to put back
                kryptonSplitContainerMain.SplitterDistance = widthLeftRight;

                // Change header to be horizontal and button to far edge
                headerGroupSearchResult.HeaderPositionPrimary = VisualOrientation.Top;
                headerGroupSearchResult.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;
            }

            kryptonSplitContainerMain.ResumeLayout();
        }

        private void buttonSpecHeaderGroupCollapse_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanelRight.SuspendLayout();
            kryptonHeaderGroup.Height = kryptonHeaderGroup.Height == kryptonHeaderGroup.PreferredSize.Height ?
                this.relatedVideoGalleryWidget.MinimumSize.Height + 
                kryptonHeaderGroup.PreferredSize.Height
                : kryptonHeaderGroup.PreferredSize.Height;
             this.tableLayoutPanelRight.ResumeLayout();
        }

        private void verticalSingleColumnGalleryWidget1_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            LoadVideo(e.Video);
        }

        private void relatedVideoGalleryWidget_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            LoadVideo(e.Video);
        }

        private void buttonSpecHeaderGroupMore_Click(object sender, EventArgs e)
        {
            var page = GetNextPage();
            if (page!= null)
            {
                new ScraperService().ScrapVideosAsync(this, new SearchParameters { Url = page.Url });
            }
        }

        private void videoPlayerWidget_ToggelFavourite(object sender, ToggleFavouriteEventArgs e)
        {
            this.verticalSingleColumnGalleryWidget1.RefreshView();
        }

        private void videoPlayerWidget_PlayNext(object sender, PlayEventArgs e)
        {
            var next = this.verticalSingleColumnGalleryWidget1.GetNextVideo(e.Current);
            if (next != null)
                LoadVideo(next);
            
        }

        private void videoPlayerWidget_PlayPrevious(object sender, PlayEventArgs e)
        {
            var pevious = this.verticalSingleColumnGalleryWidget1.GetPreviousVideo(e.Current);
            if (pevious != null)
                LoadVideo(pevious);
        }

        #endregion

        #region Methods

        public void LoadVideo(ScrapedVideo video)
        {
            if (this.currentRequestedVideo == video && !video.FullyLoaded)
                return;
            Program.SetBusy();
            this.videoPlayerWidget.PlayStop();
            
            this.currentRequestedVideo = video;
            if (video.FullyLoaded)
                OnScrapVideoDetailsCompleted(video);
            else
                new ScraperService().ScrapVideosDetailsAsync(video, this);
            
        }

        private PagingLink GetNextPage()
        {
            if (this.lastPage != null && this.lastPage.Links.Count > 0)
            {
                if (this.lastPage.Links.Count == 1 && !this.lastPage.Links[0].IsSelected)
                    return this.lastPage.Links[0];
                var index = this.lastPage.Links.IndexOf(this.lastPage.Links.FirstOrDefault(x => x.IsSelected));
                if ( this.lastPage.Links.Count > index + 1)
                    return this.lastPage.Links[index + 1];
            }
             return null;

        }
        public void Initialize(ScrapedPage page)
        {
            LogManager.Log("Initializing view window");
            this.lastPage = page;
            this.buttonSpecHeaderGroupMore.Visible = GetNextPage() != null;
            this.verticalSingleColumnGalleryWidget1.AddItems(page);
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
            this.InvokeEx(() =>
            {
                if (this.currentRequestedVideo == video)
                {

                    var played = this.videoPlayerWidget.PlayStart(video);

                    if (played)
                    {
                        this.verticalSingleColumnGalleryWidget1.AddItem(video, true);
                        this.relatedVideoGalleryWidget.ClearItems();
                        this.relatedVideoGalleryWidget.AddItems(new ScrapedPage { Videos = video.RelatedVideos });
                    }
                    else
                    {
                        this.currentRequestedVideo = null;
                    }
                }
                Program.SetIdle();
            });

        }


        public void OnScrapError(Exception ex)
        {
            Program.SetIdle();
            KryptonMessageBox.Show("An error occurred: " + ex.Message, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
        }

        public void ActivateView()
        {
            this.videoPlayerWidget.ActivateView();
            this.verticalSingleColumnGalleryWidget1.RefreshView();
            this.BringToFront();
            
        }

        public void DeactivateView()
        {
            this.videoPlayerWidget.DeactivateView();
        }

        public void TriggerSearch(SearchParameters param)
        {
            Program.SetBusy();
            this.verticalSingleColumnGalleryWidget1.ClearItems();
            new ScraperService().ScrapVideosAsync(this, param);
        }


        #endregion

      

       
    }
}
