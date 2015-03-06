using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HtmlRenderer.Entities;
using MovieTube.Viewer.Data;
using MovieTube.Client.Scraper;
using MovieTube.Viewer.Properties;


namespace MovieTube.Viewer
{
    public partial class LinkViewWidget : UserControl, IWidget
    {
        public event EventHandler<GalleryItemSelectedEventArgs> ViewSelected;
        public event EventHandler<GalleryItemSelectedEventArgs> DownloadClicked;
        private MovieLink data;
        private ClientDataService dataService = ClientDataService.Single;
        #region Constructor

        public LinkViewWidget()
        {
            InitializeComponent();
            this.MouseClick += new MouseEventHandler(groupBox1_MouseClick);
            
        }

      

        public LinkViewWidget(MovieLink data)
            :this()
        {
            data.ScrappingStatusChanged -= new EventHandler<EventArgs>(data_ScrappingStatusChanged);
            data.ScrappingStatusChanged += new EventHandler<EventArgs>(data_ScrappingStatusChanged);
            
            this.SuspendLayout();
            this.data = data;
            this.labelHeader.Text = data.DownloadSiteID;
            this.labelTitle.Text = data.LinkTitle;
            this.pictureBoxNew.Visible = Constants.IsNewLink(data);
            data_ScrappingStatusChanged(this, null);
            this.ResumeLayout();
           
        }

        void data_ScrappingStatusChanged(object sender, EventArgs e)
        {
            this.InvokeEx(() =>
            {
                switch(this.data.ScrapState)
                {
                    case LinkScrapState.FullyLoaded:
                        this.pictureBoxDownload.Image = Resources.download_icon;
                        break;
                    case LinkScrapState.Idle:
                        this.pictureBoxDownload.Image = Resources.question_y;
                        break;
                    case LinkScrapState.Scraping:
                        this.pictureBoxDownload.Image = Resources.ProgressIndicator;
                        break;
                    case LinkScrapState.VideoDoesNotExists:
                        this.pictureBoxDownload.Image = Resources.error_icon;
                        break;
                }
            });
        }

        #endregion

        #region Events
       
        private void pictureBoxDownload_Click(object sender, EventArgs e)
        {
            if (DownloadClicked != null && this.data.ScrapState == LinkScrapState.FullyLoaded)
                DownloadClicked(this, new GalleryItemSelectedEventArgs(data));
        }


        private void labelHeader_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && ViewSelected != null)
                ViewSelected(this, new GalleryItemSelectedEventArgs(data));
        }

    

        void groupBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && ViewSelected != null)
                ViewSelected(this, new GalleryItemSelectedEventArgs(data));
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            UpdateFavourite();
        }

        private void addToFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //dataService.AddToFavourite(this.data);
            UpdateFavourite();
        }

        private void deleteFromFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // dataService.DeleteFromFavourite(this.data);
            UpdateFavourite();
        }

        private void UpdateFavourite()
        {
            //var fav = this.dataService.IsFavourite(this.data);
           //this.addToFavouriteToolStripMenuItem.Visible = !fav;
           //this.deleteFromFavouriteToolStripMenuItem.Visible = fav;
        }

        public void RefreshView()
        {
            UpdateFavourite();
        }

    
        #endregion

        #region Methods

        public void SelectView()
        {
            this.BackColor = this.labelHeader.BackColor = SystemColors.ActiveCaption;
        }

        public void DeselectView()
        {
            this.BackColor = this.labelHeader.BackColor = SystemColors.Window;
        }

        public MovieLink Video
        {
            get
            {
                return this.data;
            }
        }

        #endregion
       
    }

  
}
