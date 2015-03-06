using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using Client.Scraper;
using System.Diagnostics;
using System.Threading;

namespace BlueTube.Viewer
{
    partial class MainForm : KryptonForm, IScraperServiceCallback, IViewContainer
    {
        private IView currentView;
        private Thread downloaderThread;
        private UpdaterService updaterService = new UpdaterService();
        private NotepadForm fakeView;
        public MainForm()
        {
            InitializeComponent();
            this.currentView = this.viewerWindow;
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                LogManager.Log("Downloading site async");
                new ScraperService().ScrapVideosAsync(this, new SearchParameters {  Url= "http://www.xvideos.com/new/"});
                this.Text = Constants.AppTitle;
            }
            this.fakeView = new NotepadForm(this);
        }

        private void custom2_Click(object sender, EventArgs e)
        {
            IView futureView = null;
            if (sender == this.headerView)
                futureView = this.viewerWindow;
            else if (sender == this.headerBrowse)
                futureView = this.browseWindow;
            else if (sender == this.headerFavourite)
                futureView = this.favouriteWindow;

            if (currentView == futureView)
                return;

            ChangeView(futureView);
            
        }

        private void searchPanelWidget_Search(object sender, SearchEventArgs e)
        {
           this.currentView.TriggerSearch(e.Params);
        }

        private void ChangeView(IView futureView)
        {
            
            ChangeViewSingle(this.headerView, futureView == this.viewerWindow);
            ChangeViewSingle(this.headerBrowse, futureView == this.browseWindow);
            ChangeViewSingle(this.headerFavourite, futureView == this.favouriteWindow);
           
            currentView.DeactivateView();
            futureView.ActivateView();
            currentView = futureView;
        }

        private void ChangeViewSingle(KryptonHeader view, bool selected)
        {
            if (!selected)
            {
                view.StateNormal.Back.Color1 = System.Drawing.SystemColors.GrayText;
                view.StateNormal.Back.Color2 = System.Drawing.SystemColors.InactiveCaption;
                view.StateNormal.Content.ShortText.Color1 = System.Drawing.SystemColors.GradientInactiveCaption;
                view.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                view.StateNormal.Back.Color1 = System.Drawing.SystemColors.InactiveCaptionText;
                view.StateNormal.Back.Color2 = System.Drawing.SystemColors.InactiveCaption;
                view.StateNormal.Content.ShortText.Color1 = System.Drawing.SystemColors.Window;
                view.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        public void OnScrapVideoCompleted(ScrapedPage page)
        {
            LogManager.Log("Download complete. Total videos: " + page.Videos.Count);
            this.InvokeEx(() =>
            {
                this.labelLoading.Dispose();
                this.viewerWindow.Initialize(page);
                this.browseWindow.Initialize(page);
            });
            
        }

        public void OnScrapVideoDetailsCompleted(ScrapedVideo video)
        {
           
        }

        public void OnScrapError(Exception ex)
        {
            KryptonMessageBox.Show("An error occurred: " + ex.Message, Constants.AppTitle, 
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        public void PlayVideo(ScrapedVideo video)
        {
            ChangeView(this.viewerWindow);
            this.viewerWindow.LoadVideo(video);
        }


        public IView ActiveView
        {
            get { throw new NotImplementedException(); }
            private set { }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.currentView.DeactivateView();
            this.Close();
        }

        private void playToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
           // var activeView = this.currentView == this.viewerWindow;

        }

        private void gotoWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.WebsiteUrl);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           downloaderThread = new Thread(DownloadWorker);
           downloaderThread.IsBackground = true;
           downloaderThread.Start();
        }

        private void DownloadWorker()
        {
            if (updaterService.DownloadAppUpdate())
                ReloadApp();
            downloaderThread = null;
        }

        private void ReloadApp()
        {
            this.InvokeEx(() =>
            {
                KryptonMessageBox.Show("A new version of the application is downloaded. The application will be closed while installing", Constants.AppTitle);
                if(new UpdaterService().ExecuteUpdater())
                    Environment.Exit(0);
            });
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                if (!this.fakeView.Visible)
                {
                    this.Hide();
                    this.fakeView.ShowDialog();
                }
                else
                {
                    this.Show();
                    this.fakeView.Hide();
                }
                e.SuppressKeyPress = true;  
            }
        }

        private void reportIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.ReportIssueUrl);
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.HelpUrl);
        }
    }
}
