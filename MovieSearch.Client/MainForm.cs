using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace MovieFinder.Client
{
    public partial class MainForm : Form
    {
        #region Fields
        private Thread downloaderThread;
        private UpdaterService updaterService = new UpdaterService();
               
        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
           
        }

        #endregion

        #region Methods

        private void removeImagesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Movie.ImgDirectory))
            {
                try
                {
                    foreach (var file in Directory.GetFiles(Movie.ImgDirectory))
                        File.Delete(file);

                }
                catch { }
            }
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
            ShowApp();
        }

        private void toolStripMenuItemLaunch_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.WebsiteUrl);
        }

        private void toolStripMenuItemMovieCheck_Click(object sender, EventArgs e)
        {
            CheckMovieDBUpdate(true);
        }

        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.FormClosing -= new FormClosingEventHandler(MainForm_FormClosing);
            Application.Exit();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowApp();
        }
        private void ShowApp()
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
            this.BringToFront();
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void aboutMovieFinderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.movieListWidget.LoadData(false);
            UpdateStatusBar();
            timerDB_Tick(null, null);
        }

        private void DownloadWorker()
        {
            CheckMovieDBUpdate(false);
            if (updaterService.DownloadAppUpdate())
                ReloadApp();
            downloaderThread = null;
        }

        private void timerDB_Tick(object sender, EventArgs e)
        {
            if (downloaderThread == null)
            {
                downloaderThread = new Thread(DownloadWorker);
                downloaderThread.IsBackground = true;
                downloaderThread.Start();
            }
        }

        private void ReloadDB()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.movieListWidget.LoadData(true);
                    UpdateStatusBar();
                });
            }
            else
            {
                this.movieListWidget.LoadData(true);
                UpdateStatusBar();
            }
        }

        private void ReloadApp()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.timerDB.Enabled = false;
                    MessageBox.Show("A new version of the application is downloaded. The application will be closed while installing", "Movie Finder");
                    new UpdaterService().ExecuteUpdater();
                    BusinessCardRenderer.StopThread();
                    Environment.Exit(1);
                });
            }
            else
            {
                MessageBox.Show("A new version of the application is downloaded. The application will be closed while installing", "Movie Finder");
                new UpdaterService().ExecuteUpdater();
                BusinessCardRenderer.StopThread();
                Environment.Exit(1);
            }
        }

        private void checkForNewNewMoviesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckMovieDBUpdate(true);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void onlineHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(UrlConstants.WebsiteUrl);
        }

        private void UpdateStatusBar(string message = "")
        {
            if (String.IsNullOrEmpty(message))
                this.toolStripStatusLabel1.Text = "Total Movies: " + this.movieListWidget.Movies.Count;
            else
                this.toolStripStatusLabel1.Text = message;
        }

        private void CheckMovieDBUpdate(bool showMsg)
        {
            if (updaterService.DownloadMovieDatabase())
                ReloadDB();
            else if(showMsg)
                MessageBox.Show("No new movies found", "Movie Finder");
        }

        private void suggestionBugReportingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SuggestionForm().ShowDialog();
        }
    }
    
}
