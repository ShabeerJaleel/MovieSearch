using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ComponentFactory.Krypton.Toolkit;
using MovieTube.Client.Scraper;
using MovieTube.Viewer.Data;
using System.Diagnostics;
using System.IO;
using System.Net;


namespace MovieTube.Viewer
{
    public partial class ViewWindow : UserControl, IScraperServiceCallback, IView, IDownloadProgress
    {
        #region Fields

        private int widthLeftRight;
        private MovieLink currentRequestedVideo;
        private MoviePage lastPage;
        private IPlayerWidget playerWidget;
        public static MovieDownloadForm DownloadForm = new MovieDownloadForm();

        #endregion

        #region Constrctor


        public ViewWindow()
        {
            InitializeComponent();
            this.playerWidget = this.wmpVideoPlayerWidget;   
        }

        #endregion

        #region Events


        private void buttonSpecHeaderGroupSearchResult_Click(object sender, EventArgs e)
        {
            // Suspend layout changes until all splitter properties have been updated
            kryptonSplitContainerTop.SuspendLayout();

            // Is the left header group currently expanded?
            if (kryptonSplitContainerTop.FixedPanel == FixedPanel.None)
            {
                // Make the left panel of the splitter fixed in size
                kryptonSplitContainerTop.FixedPanel = FixedPanel.Panel2;
                kryptonSplitContainerTop.IsSplitterFixed = true;

                // Remember the current height of the header group
                widthLeftRight = headerGroupSearchResult.Width;

                // We have not changed the orientation of the header yet, so the width of 
                // the splitter panel is going to be the height of the collapsed header group
                int newWidth = headerGroupSearchResult.PreferredSize.Height;

                // Make the header group fixed just as the new height
                kryptonSplitContainerTop.Panel2MinSize = newWidth;
                kryptonSplitContainerTop.SplitterDistance = kryptonSplitContainerTop.Width- newWidth;

                // Change header to be vertical and button to near edge
                headerGroupSearchResult.HeaderPositionPrimary = VisualOrientation.Right;
                headerGroupSearchResult.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Near;
            }
            else
            {
                // Make the bottom panel not-fixed in size anymore
                kryptonSplitContainerTop.FixedPanel = FixedPanel.None;
                kryptonSplitContainerTop.IsSplitterFixed = false;

                // Put back the minimise size to the original
                kryptonSplitContainerTop.Panel2MinSize = 100;

                // Calculate the correct splitter we want to put back
                kryptonSplitContainerTop.SplitterDistance = kryptonSplitContainerTop.Width - widthLeftRight;

                // Change header to be horizontal and button to far edge
                headerGroupSearchResult.HeaderPositionPrimary = VisualOrientation.Top;
                headerGroupSearchResult.ButtonSpecs[0].Edge = PaletteRelativeEdgeAlign.Far;
            }

            kryptonSplitContainerTop.ResumeLayout();
        }

        private void buttonSpecHeaderGroupCollapse_Click(object sender, EventArgs e)
        {
            this.tableLayoutPanel1.SuspendLayout();
            kryptonHeaderGroup.Height = kryptonHeaderGroup.Height == kryptonHeaderGroup.PreferredSize.Height ?
                this.videoThumbGalleryWidget.MinimumSize.Height +
                kryptonHeaderGroup.PreferredSize.Height
                : kryptonHeaderGroup.PreferredSize.Height;
            this.tableLayoutPanel1.ResumeLayout();
        }

        private void buttonSpecHeaderGroupNext_Click(object sender, EventArgs e)
        {
            this.videoThumbGalleryWidget.ShowNext();
            UpdateNavigation();
        }

        private void buttonSpecHeaderGroupPrevious_Click(object sender, EventArgs e)
        {
            this.videoThumbGalleryWidget.ShowPrevious();
            UpdateNavigation();
        }


        private void verticalSingleColumnGalleryWidget1_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            LoadVideo(e.Data as MovieLink);
        }

        private void videoLinksGalleryWidget_DownloadClicked(object sender, GalleryItemSelectedEventArgs e)
        {
            if (!Directory.Exists(AppSettings.MovieDownloadFolder))
            {
                using (var di = new FolderBrowserDialog() { Description = "Select download folder" })
                {
                    if (di.ShowDialog() == DialogResult.OK)
                        AppSettings.MovieDownloadFolder = di.SelectedPath;
                    else
                        return;
                }
            }
            {
                var link = (MovieLink)e.Data;
                var ds = new DownloaderService();
                var di = new DownloadInfo
                {
                    Link = link,
                    Service = ds
                };
                
                ds.Download(di, this);
                DownloadForm.ShowMe();
                DownloadForm.Add(di, 0);
            }
        }

        private void relatedVideoGalleryWidget_ItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            LoadVideo(e.Data as Movie);
        }

        private void buttonSpecHeaderGroupMore_Click(object sender, EventArgs e)
        {
            var page = GetNextPage();
            if (page!= null)
            {
                //new ScraperService().ScrapVideosAsync(this, new SearchParameters { Url = page.Url });
            }
        }

        private void videoPlayerWidget_ToggelFavourite(object sender, ToggleFavouriteEventArgs e)
        {
            this.videoThumbGalleryWidget.RefreshView();
        }

        private void videoPlayerWidget_PlayNext(object sender, PlayEventArgs e)
        {
            var next = this.videoThumbGalleryWidget.GetNextVideo(e.Current);
            if (next != null)
                this.videoThumbGalleryWidget.SelecteItem(next);
        }

        private void videoPlayerWidget_PlayPrevious(object sender, PlayEventArgs e)
        {
            var pevious = this.videoThumbGalleryWidget.GetPreviousVideo(e.Current);
            if (pevious != null)
                this.videoThumbGalleryWidget.SelecteItem(pevious);
        }

        #endregion

        #region Methods


        public void LoadVideo(MovieLink video)
        {
            this.playerWidget.PlayStop();
            this.playerWidget.ShowBuffering();
            this.currentRequestedVideo = video;
            if (video.ScrapState == LinkScrapState.FullyLoaded)
                OnScrapVideoDetailsCompleted(video);
            else if (video.ScrapState == LinkScrapState.VideoDoesNotExists)
                OnScrapError(video, "Video is removed");
            else
                new ScraperService().ScrapVideosDetailsAsync(this, video);
        }

        public void LoadVideo(Movie movie, bool playFirstLink = false)
        {
            var s = ClientDataService.Single.Settings;
            s.PlayFirstLink = playFirstLink ? true : s.PlayFirstLink;
            this.videoThumbGalleryWidget.AddItem(movie, true);
            this.videoLinksGalleryWidget.ClearItems();
            this.videoLinksGalleryWidget.AddItem(movie, s.PlayFirstLink);
            new ScraperService().ScrapVideosDetailsAsync(this, movie);

            this.playerWidget.PlayStop();

            if (s.PlayFirstLink)
                LoadVideo(movie.Links[0]);
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
       
        public void Initialize(MoviePage page)
        {
            LogManager.Log("Initializing view window");
            this.lastPage = page;
            this.buttonSpecHeaderGroupMore.Visible = GetNextPage() != null;
            this.videoThumbGalleryWidget.ClearItems();
            this.videoThumbGalleryWidget.AddItems(page);
            UpdateNavigation();
        }

        public void ActivateView()
        {
            this.playerWidget.ActivateView();
            this.videoLinksGalleryWidget.RefreshView();
            this.videoThumbGalleryWidget.RefreshView();
            this.BringToFront();
            
        }

        public void DeactivateView()
        {
            this.playerWidget.DeactivateView();
        }

        public void StopPlay()
        {
            this.playerWidget.PlayStop();
        }

        public void TriggerSearch(SearchParameters param)
        {
            Program.SetBusy();
            Initialize(ClientDataService.Single.GetAllMovies(param));
            Program.SetIdle();
        }

        private void UpdateNavigation()
        {
            this.buttonSpecHeaderGroupNext.Enabled = !this.videoThumbGalleryWidget.IsEnd ? ButtonEnabled.True : ButtonEnabled.False;
            this.buttonSpecHeaderGroupPrevious.Enabled = !this.videoThumbGalleryWidget.IsBeginning ? ButtonEnabled.True : ButtonEnabled.False;
        }


        #endregion

        #region IScraperServiceCallback

        public void OnScrapVideoDetailsCompleted(MovieLink link)
        {
            this.InvokeEx(() =>
            {
                if (this.currentRequestedVideo == link)
                {
                    Tracer.WriteLine(String.Format("Playing {0}, Link: {1}",  link.PlayUrl, link.DowloadUrl));
                    this.playerWidget.PlayStart(link);
                }
                this.playerWidget.HideBuffering();
            });

        }
      

        public void OnScrapError(MovieLink link, string message)
        {
            this.InvokeEx(() =>
            {
                this.playerWidget.HideBuffering();
                if (this.currentRequestedVideo == link)
                    KryptonMessageBox.Show(this, message, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            });

        }

        #endregion

        #region IDownloadProgress

        public void OnProgress(DownloadInfo di, DownloadProgressChangedEventArgs e)
        {
            DownloadForm.UpdateProgress(di, e);
        }

        public void OnCompletion(DownloadInfo di,AsyncCompletedEventArgs e)
        {
            DownloadForm.UpdateCompletion(di,e);
        }

      
        #endregion

       
    }
}
