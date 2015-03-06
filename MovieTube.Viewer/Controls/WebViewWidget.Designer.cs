namespace MovieTube.Viewer
{
    partial class WebViewWidget
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
            this.components = new System.ComponentModel.Container();
            this.htmlPanel = new HtmlRenderer.HtmlPanel();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToFavouriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFromFavouriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxSubtitle = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewLink = new System.Windows.Forms.PictureBox();
            this.pictureBoxNewMovie = new System.Windows.Forms.PictureBox();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.htmlPanel.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubtitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewLink)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewMovie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // htmlPanel
            // 
            this.htmlPanel.AutoScroll = true;
            this.htmlPanel.AvoidGeometryAntialias = false;
            this.htmlPanel.AvoidImagesLateLoading = false;
            this.htmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel.BaseStylesheet = null;
            this.htmlPanel.ContextMenuStrip = this.contextMenuStrip;
            this.htmlPanel.Controls.Add(this.pictureBoxSubtitle);
            this.htmlPanel.Controls.Add(this.pictureBoxNewLink);
            this.htmlPanel.Controls.Add(this.pictureBoxNewMovie);
            this.htmlPanel.Controls.Add(this.pictureBox);
            this.htmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel.IsContextMenuEnabled = false;
            this.htmlPanel.Location = new System.Drawing.Point(0, 0);
            this.htmlPanel.Name = "htmlPanel";
            this.htmlPanel.Size = new System.Drawing.Size(350, 155);
            this.htmlPanel.TabIndex = 2;
            this.htmlPanel.LinkClicked += new System.EventHandler<HtmlRenderer.Entities.HtmlLinkClickedEventArgs>(this.htmlPanel_LinkClicked);
            this.htmlPanel.Click += new System.EventHandler(this.htmlPanel_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToFavouriteToolStripMenuItem,
            this.deleteFromFavouriteToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(187, 48);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addToFavouriteToolStripMenuItem
            // 
            this.addToFavouriteToolStripMenuItem.Name = "addToFavouriteToolStripMenuItem";
            this.addToFavouriteToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.addToFavouriteToolStripMenuItem.Text = "Add to favourite";
            this.addToFavouriteToolStripMenuItem.Click += new System.EventHandler(this.addToFavouriteToolStripMenuItem_Click);
            // 
            // deleteFromFavouriteToolStripMenuItem
            // 
            this.deleteFromFavouriteToolStripMenuItem.Name = "deleteFromFavouriteToolStripMenuItem";
            this.deleteFromFavouriteToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.deleteFromFavouriteToolStripMenuItem.Text = "Delete from favourite";
            this.deleteFromFavouriteToolStripMenuItem.Click += new System.EventHandler(this.deleteFromFavouriteToolStripMenuItem_Click);
            // 
            // pictureBoxSubtitle
            // 
            this.pictureBoxSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSubtitle.Image = global::MovieTube.Viewer.Properties.Resources.subtitles;
            this.pictureBoxSubtitle.Location = new System.Drawing.Point(0, 139);
            this.pictureBoxSubtitle.Name = "pictureBoxSubtitle";
            this.pictureBoxSubtitle.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxSubtitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxSubtitle.TabIndex = 6;
            this.pictureBoxSubtitle.TabStop = false;
            this.pictureBoxSubtitle.Visible = false;
            // 
            // pictureBoxNewLink
            // 
            this.pictureBoxNewLink.Image = global::MovieTube.Viewer.Properties.Resources.newlink;
            this.pictureBoxNewLink.Location = new System.Drawing.Point(-1, -1);
            this.pictureBoxNewLink.Name = "pictureBoxNewLink";
            this.pictureBoxNewLink.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxNewLink.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxNewLink.TabIndex = 5;
            this.pictureBoxNewLink.TabStop = false;
            // 
            // pictureBoxNewMovie
            // 
            this.pictureBoxNewMovie.Image = global::MovieTube.Viewer.Properties.Resources._new;
            this.pictureBoxNewMovie.Location = new System.Drawing.Point(-1, -2);
            this.pictureBoxNewMovie.Name = "pictureBoxNewMovie";
            this.pictureBoxNewMovie.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxNewMovie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxNewMovie.TabIndex = 4;
            this.pictureBoxNewMovie.TabStop = false;
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox.Image = global::MovieTube.Viewer.Properties.Resources.favourite_16;
            this.pictureBox.Location = new System.Drawing.Point(0, 122);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(16, 16);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // WebViewWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.htmlPanel);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximumSize = new System.Drawing.Size(0, 155);
            this.MinimumSize = new System.Drawing.Size(350, 0);
            this.Name = "WebViewWidget";
            this.Size = new System.Drawing.Size(350, 155);
            this.htmlPanel.ResumeLayout(false);
            this.htmlPanel.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSubtitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewLink)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNewMovie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HtmlRenderer.HtmlPanel htmlPanel;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToFavouriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFromFavouriteToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.PictureBox pictureBoxNewMovie;
        private System.Windows.Forms.PictureBox pictureBoxNewLink;
        private System.Windows.Forms.PictureBox pictureBoxSubtitle;


    }
}
