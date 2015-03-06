using System;
namespace MovieTube.Viewer
{
    partial class LinkViewWidget
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
                data.ScrappingStatusChanged -= new EventHandler<EventArgs>(data_ScrappingStatusChanged);
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
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToFavouriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFromFavouriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.pictureBoxNew = new System.Windows.Forms.PictureBox();
            this.pictureBoxDownload = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNew)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownload)).BeginInit();
            this.SuspendLayout();
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
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(23, 19);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(41, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "label1";
            this.labelTitle.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelHeader_MouseClick);
            // 
            // labelHeader
            // 
            this.labelHeader.AutoSize = true;
            this.labelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHeader.Location = new System.Drawing.Point(3, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(29, 12);
            this.labelHeader.TabIndex = 1;
            this.labelHeader.Text = "label1";
            this.labelHeader.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelHeader_MouseClick);
            // 
            // pictureBoxNew
            // 
            this.pictureBoxNew.Image = global::MovieTube.Viewer.Properties.Resources._new;
            this.pictureBoxNew.Location = new System.Drawing.Point(0, 15);
            this.pictureBoxNew.Name = "pictureBoxNew";
            this.pictureBoxNew.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxNew.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxNew.TabIndex = 3;
            this.pictureBoxNew.TabStop = false;
            // 
            // pictureBoxDownload
            // 
            this.pictureBoxDownload.Location = new System.Drawing.Point(84, -1);
            this.pictureBoxDownload.Name = "pictureBoxDownload";
            this.pictureBoxDownload.Size = new System.Drawing.Size(16, 16);
            this.pictureBoxDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxDownload.TabIndex = 4;
            this.pictureBoxDownload.TabStop = false;
            this.pictureBoxDownload.Click += new System.EventHandler(this.pictureBoxDownload_Click);
            // 
            // LinkViewWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pictureBoxDownload);
            this.Controls.Add(this.pictureBoxNew);
            this.Controls.Add(this.labelHeader);
            this.Controls.Add(this.labelTitle);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximumSize = new System.Drawing.Size(0, 138);
            this.MinimumSize = new System.Drawing.Size(250, 0);
            this.Name = "LinkViewWidget";
            this.Size = new System.Drawing.Size(250, 41);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxNew)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDownload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToFavouriteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFromFavouriteToolStripMenuItem;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.PictureBox pictureBoxNew;
        private System.Windows.Forms.PictureBox pictureBoxDownload;


    }
}
