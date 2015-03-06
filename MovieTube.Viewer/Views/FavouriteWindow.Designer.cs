﻿namespace MovieTube.Viewer.Views
{
    partial class FavouriteWindow
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseGalleryWidget = new MovieTube.Viewer.Controls.BrowseGalleryWidget();
            this.SuspendLayout();
            // 
            // browseGalleryWidget
            // 
            this.browseGalleryWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseGalleryWidget.Location = new System.Drawing.Point(0, 0);
            this.browseGalleryWidget.Name = "browseGalleryWidget";
            this.browseGalleryWidget.Size = new System.Drawing.Size(150, 150);
            this.browseGalleryWidget.TabIndex = 1;
            this.browseGalleryWidget.ItemSelected += new System.EventHandler<MovieTube.Viewer.GalleryItemSelectedEventArgs>(this.browseGalleryWidget_ItemSelected);
            this.browseGalleryWidget.ItemFavourited += new System.EventHandler<MovieTube.Viewer.GalleryItemFavouriteEventArgs>(this.browseGalleryWidget_ItemFavourited);
            // 
            // FavouriteWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.browseGalleryWidget);
            this.Name = "FavouriteWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BrowseGalleryWidget browseGalleryWidget;
    }
}
