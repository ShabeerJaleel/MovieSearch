using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Diagnostics;
using System.Threading;
using MovieTube.Client.Scraper;
using MovieTube.Viewer.Data;
using System.Linq;

namespace MovieTube.Viewer
{
    partial class MainForm : KryptonForm,IViewContainer
    {
        #region Fields

        private IView currentView;
        private Thread downloaderThread;
        private UpdaterService updaterService = new UpdaterService();
        private NotepadForm fakeView;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            this.currentView = this.viewerWindow;
            this.Text = Constants.AppTitle;
            System.Net.ServicePointManager.Expect100Continue = false;
            // this.fakeView = new NotepadForm(this);
        }

        #endregion

        #region Methods

       
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
                view.StateNormal.Content.ShortText.Color1 = Color.White;
                view.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }


        public void PlayVideo(Movie video)
        {
            ChangeView(this.viewerWindow);
            this.viewerWindow.LoadVideo(video);
        }

        public IView ActiveView
        {
            get { throw new NotImplementedException(); }
            private set { }
        }
        
        

        private void ReloadApp(Version version)
        {
            this.InvokeEx(() =>
            {
                KryptonMessageBox.Show(this, String.Format("A new version ({0}) of the application is downloaded. The application will be closed while installing", version),
                    Constants.AppTitle);
                if(new UpdaterService().ExecuteUpdater())
                    Environment.Exit(0);
            });
        }

        private void CheckMovieDBUpdate(bool showMsg)
        {
            if (updaterService.DownloadMovieDatabase(true))
                ReloadDB();
            else if (showMsg)
                KryptonMessageBox.Show(this, "No new movies found", Constants.AppTitle);
        }

        private void ReloadDB()
        {
            this.InvokeEx(() =>
            {
                var movies = ClientDataService.Single.GetLatestMovies();
                if (movies.Count > 0)
                {
                    new DBUpdatedForm().ShowMe(movies);
                    if (this.currentView == this.viewerWindow)
                    {
                        var lang = this.searchPanelWidget.GetLanguage();
                        if (movies.Any(x => x.ModifiedDate == null && x.LanguageCode == lang))
                        {
                            this.currentView.TriggerSearch(new SearchParameters
                            {
                                Language = this.searchPanelWidget.GetLanguage(),
                                Year = 0
                            });
                        }
                    }
                }
               
                UpdateStatusBar();
            });
        }

        private void UpdateStatusBar(string message = "")
        {
            //if (String.IsNullOrEmpty(message))
            //    this.toolStripStatusLabel1.Text = "Total Movies: " + this.movieListWidget.Movies.Count;
            //else
            //    this.toolStripStatusLabel1.Text = message;
        }

        private void ShowApp()
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront();
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        private void StartUpdateCheckThread(bool forcefully)
        {
            this.toolStripStatusLabelMessage.Text = Constants.ServerMessage;
            if (downloaderThread == null)
            {
                downloaderThread = new Thread(DownloadWorker);
                downloaderThread.IsBackground = true;
                downloaderThread.Start(forcefully);
            }
        }

        private void DownloadWorker(object data)
        {

            var forcefull = (bool)data;
            CheckMovieDBUpdate(false);
            var v = updaterService.DownloadAppUpdate(forcefull);
            if(v > Constants.AppVersion)
                ReloadApp(v);
            downloaderThread = null;
            if (!forcefull)
            {
                Constants.UpdateShowAds();
                Constants.UpdateMessage();
            }
        }

        #endregion

        #region Events


        private void openToolStripMenuItemOpenNStream_Click(object sender, EventArgs e)
        {
            var frm = new NetworkStreamForm();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var scraper = VideoScraperBase.GetScraper(frm.Url);
                    var movie = new Movie
                    {
                        Description = frm.Url,
                        Url = frm.Url,
                        Name = frm.Url,
                        ReleaseDate  =DateTime.Now
                    };
                    movie.Links.Add(new MovieLink
                    {
                        DowloadUrl = frm.Url,
                        Parent = movie,
                        LinkTitle = frm.Url,
                        PageSiteID = scraper.Title,
                        DownloadSiteID = scraper.Title,
                    });
                    this.viewerWindow.LoadVideo(movie, true);
                }
                catch(Exception ex) {
                    KryptonMessageBox.Show(this, ex.Message,
                        Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

      
        private void toolStripButtonShowNew_Click(object sender, EventArgs e)
        {
            var lang = ((ToolStripItem)sender).Tag != null ?  ((ToolStripItem)sender).Tag.ToString() : null;
            this.currentView.TriggerSearch(new SearchParameters { Query = "_new", Language = lang });
        }

       
        private void downloadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewWindow.DownloadForm.ShowMe();
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

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuitApp();
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
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                LogManager.Log("Starting application");
                var movies = ClientDataService.Single.GetAllMovies(new SearchParameters() {
                    Language = ClientDataService.Single.Settings.DefaultLanguage
                });
                this.viewerWindow.Initialize(movies);
                //this.browseWindow.Initialize(movies);
            }
            this.viewerWindow.Focus();
            StartUpdateCheckThread(true);
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

        public void OnScrapVideoDetailsCompleted(MovieLink link)
        {
            throw new NotImplementedException();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Options().ShowDialog(this);
        }

        private void timerDB_Tick(object sender, EventArgs e)
        {
            StartUpdateCheckThread(false);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            CheckMovieDBUpdate(true);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApp();
        }

        private void gotoWebsiteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.WebsiteUrl);
        }

        private void checkForNewMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckMovieDBUpdate(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuitApp();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.viewerWindow.StopPlay();
            this.Hide();
            e.Cancel = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                ShowApp();

        }
        TraceForm traceForm;
        private void traceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (traceForm == null || traceForm.IsDisposed)
                traceForm = new TraceForm();
            traceForm.Show();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
                ShowApp();
            base.WndProc(ref m);
        }

        private void QuitApp()
        {
            this.FormClosing -= new FormClosingEventHandler(MainForm_FormClosing);
            ViewWindow.DownloadForm.Dispose();
            Application.Exit();
        }

        private void toolStripButtonShare_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fwww.filmkhoj.com");
        }

        #endregion

    
     
    }
}
