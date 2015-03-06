using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using HtmlRenderer.Entities;
using Client.Scraper;
using BlueTube.Viewer.Data;


namespace BlueTube.Viewer
{
    public partial class WebViewWidget : UserControl
    {
        public event EventHandler<GalleryItemSelectedEventArgs> ViewSelected;
        private ScrapedVideo video;
        private DataService dataService = DataService.Create();
        #region Constructor

        public WebViewWidget()
        {
            InitializeComponent();
            
        }

        public WebViewWidget(ScrapedVideo video, string html)
            :this()
        {
            this.SuspendLayout();
            this.pictureBox.Visible = DataService.Create().IsFavourite(video);
            this.video = video;
            this.htmlPanel.Text = html;
            this.htmlPanel.AutoScroll = false;
            this.AutoScroll = false;
            this.htmlPanel.VerticalScroll.Enabled = false;
            this.htmlPanel.HorizontalScroll.Enabled = false;
            this.htmlPanel.VerticalScroll.Visible = false;
            this.htmlPanel.HorizontalScroll.Visible = false;
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
                ViewSelected(this, new GalleryItemSelectedEventArgs( video));
        }

        private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            UpdateFavourite();
        }

        private void addToFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataService.AddToFavourite(this.video);
            UpdateFavourite();
        }

        private void deleteFromFavouriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataService.DeleteFromFavourite(this.video);
            UpdateFavourite();
        }

        private void UpdateFavourite()
        {
            var fav = this.dataService.IsFavourite(this.video);
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

        public ScrapedVideo Video
        {
            get
            {
                return this.video;
            }
        }
    }

  
}
