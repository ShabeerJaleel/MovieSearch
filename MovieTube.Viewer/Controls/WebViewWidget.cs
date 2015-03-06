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


namespace MovieTube.Viewer
{
    public partial class WebViewWidget : UserControl, IWidget
    {
        public event EventHandler<GalleryItemSelectedEventArgs> ViewSelected;
        public event EventHandler<GalleryItemFavouriteEventArgs> Favourited;
        private Movie data;
        private ClientDataService dataService = ClientDataService.Single;
        #region Constructor

        public WebViewWidget()
        {
            InitializeComponent();
            
        }

        public WebViewWidget(Movie data, string html)
            :this()
        {
            this.SuspendLayout();
            this.pictureBox.Visible = ClientDataService.Single.IsFavourite(data);
            this.data = data;
            this.htmlPanel.Text = html;
            this.htmlPanel.AutoScroll = false;
            this.AutoScroll = false;
            this.htmlPanel.VerticalScroll.Enabled = false;
            this.htmlPanel.HorizontalScroll.Enabled = false;
            this.htmlPanel.VerticalScroll.Visible = false;
            this.htmlPanel.HorizontalScroll.Visible = false;
            this.pictureBoxNewMovie.Visible = Constants.IsNewMovie(data);
            this.pictureBoxNewLink.Visible =  Constants.IsNewLink(data);
            this.ResumeLayout();
           
        }

        #endregion

        #region Events

        private void htmlPanel_LinkClicked(object sender, HtmlLinkClickedEventArgs e)
        {
            e.Handled = true;
        }

        private void htmlPanel_Click(object sender, EventArgs e)
        {
            if (ViewSelected != null)
                ViewSelected(this, new GalleryItemSelectedEventArgs( data));
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            UpdateFavourite();
        }

        private void addToFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataService.AddToFavourite(this.data);
            UpdateFavourite();
            if (Favourited != null)
                Favourited(this, new GalleryItemFavouriteEventArgs(this.data, true));
        }

        private void deleteFromFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataService.DeleteFromFavourite(this.data);
            UpdateFavourite();
            if (Favourited != null)
                Favourited(this, new GalleryItemFavouriteEventArgs(this.data, false));
        }

        private void UpdateFavourite()
        {
            var fav = this.dataService.IsFavourite(this.data);
            this.addToFavouriteToolStripMenuItem.Visible = !fav;
            this.deleteFromFavouriteToolStripMenuItem.Visible = fav;
            this.pictureBox.Visible = fav;
        }

        public void RefreshView()
        {
            UpdateFavourite();
        }

        #endregion

        public void SelectView()
        {
            this.htmlPanel.BackColor = SystemColors.ActiveCaption;
        }

        public void DeselectView()
        {
            this.htmlPanel.BackColor = SystemColors.Window;
        }

        public Movie Video
        {
            get
            {
                return this.data;
            }
        }
    }

    interface IWidget
    {
        void SelectView();
        void DeselectView();
    }

  
}
