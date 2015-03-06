namespace MovieTube.Viewer
{
    partial class WMPVideoPlayerWidget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WMPVideoPlayerWidget));
            this.axWindowsMediaPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.headerGroupVideoPlayer = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderFavourite = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroupOriginal = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroupFullscreen = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupVideoPlayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupVideoPlayer.Panel)).BeginInit();
            this.headerGroupVideoPlayer.Panel.SuspendLayout();
            this.headerGroupVideoPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer
            // 
            this.axWindowsMediaPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWindowsMediaPlayer.Enabled = true;
            this.axWindowsMediaPlayer.Location = new System.Drawing.Point(0, 0);
            this.axWindowsMediaPlayer.Name = "axWindowsMediaPlayer";
            this.axWindowsMediaPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer.OcxState")));
            this.axWindowsMediaPlayer.Size = new System.Drawing.Size(766, 352);
            this.axWindowsMediaPlayer.TabIndex = 4;
            this.axWindowsMediaPlayer.DoubleClickEvent += new AxWMPLib._WMPOCXEvents_DoubleClickEventHandler(this.axWindowsMediaPlayer_DoubleClickEvent);
            this.axWindowsMediaPlayer.OpenStateChange += new AxWMPLib._WMPOCXEvents_OpenStateChangeEventHandler(this.axWindowsMediaPlayer_OpenStateChange);
            this.axWindowsMediaPlayer.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer_PlayStateChange);
            this.axWindowsMediaPlayer.ErrorEvent += new System.EventHandler(this.axWindowsMediaPlayer_ErrorEvent);
            this.axWindowsMediaPlayer.PositionChange += new AxWMPLib._WMPOCXEvents_PositionChangeEventHandler(this.axWindowsMediaPlayer_PositionChange);
            // 
            // headerGroupVideoPlayer
            // 
            this.headerGroupVideoPlayer.AllowButtonSpecToolTips = true;
            this.headerGroupVideoPlayer.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderFavourite,
            this.buttonSpecHeaderGroupOriginal,
            this.buttonSpecHeaderGroupFullscreen});
            this.headerGroupVideoPlayer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.headerGroupVideoPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerGroupVideoPlayer.HeaderVisibleSecondary = false;
            this.headerGroupVideoPlayer.Location = new System.Drawing.Point(0, 0);
            this.headerGroupVideoPlayer.Name = "headerGroupVideoPlayer";
            // 
            // headerGroupVideoPlayer.Panel
            // 
            this.headerGroupVideoPlayer.Panel.Controls.Add(this.axWindowsMediaPlayer);
            this.headerGroupVideoPlayer.Size = new System.Drawing.Size(768, 379);
            this.headerGroupVideoPlayer.StateCommon.Back.Color1 = System.Drawing.Color.Transparent;
            this.headerGroupVideoPlayer.StateCommon.HeaderPrimary.Content.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerGroupVideoPlayer.StateCommon.HeaderPrimary.Content.ShortText.Trim = ComponentFactory.Krypton.Toolkit.PaletteTextTrim.Word;
            this.headerGroupVideoPlayer.TabIndex = 6;
            this.headerGroupVideoPlayer.ValuesPrimary.Heading = global::MovieTube.Viewer.Properties.Resources.DownloadDirectory;
            this.headerGroupVideoPlayer.ValuesPrimary.Image = ((System.Drawing.Image)(resources.GetObject("headerGroupVideoPlayer.ValuesPrimary.Image")));
            // 
            // buttonSpecHeaderFavourite
            // 
            this.buttonSpecHeaderFavourite.Checked = ComponentFactory.Krypton.Toolkit.ButtonCheckState.Unchecked;
            this.buttonSpecHeaderFavourite.Image = global::MovieTube.Viewer.Properties.Resources.favourite_16;
            this.buttonSpecHeaderFavourite.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.buttonSpecHeaderFavourite.ToolTipTitle = "Toggle Favourite";
            this.buttonSpecHeaderFavourite.UniqueName = "D2EF56A993524F6C70A3AB3B4A15DE6E";
            this.buttonSpecHeaderFavourite.Click += new System.EventHandler(this.buttonSpecHeaderFavourite_Click);
            // 
            // buttonSpecHeaderGroupOriginal
            // 
            this.buttonSpecHeaderGroupOriginal.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.buttonSpecHeaderGroupOriginal.Image = global::MovieTube.Viewer.Properties.Resources.original;
            this.buttonSpecHeaderGroupOriginal.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.buttonSpecHeaderGroupOriginal.ToolTipTitle = "Toggle Video Size";
            this.buttonSpecHeaderGroupOriginal.UniqueName = "3A85FF8BF82D4E0ECDA7F0A4F24B5C6B";
            this.buttonSpecHeaderGroupOriginal.Click += new System.EventHandler(this.buttonSpecHeaderGroupOriginal_Click);
            // 
            // buttonSpecHeaderGroupFullscreen
            // 
            this.buttonSpecHeaderGroupFullscreen.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.buttonSpecHeaderGroupFullscreen.Image = global::MovieTube.Viewer.Properties.Resources.fullscreen1;
            this.buttonSpecHeaderGroupFullscreen.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.buttonSpecHeaderGroupFullscreen.ToolTipTitle = "Fullscreen";
            this.buttonSpecHeaderGroupFullscreen.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.WorkspaceMaximize;
            this.buttonSpecHeaderGroupFullscreen.UniqueName = "BAE773AF597940F4AFB2C6203F81C862";
            this.buttonSpecHeaderGroupFullscreen.Click += new System.EventHandler(this.buttonSpecHeaderGroupFullscreen_Click);
            // 
            // WMPVideoPlayerWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.headerGroupVideoPlayer);
            this.Name = "WMPVideoPlayerWidget";
            this.Size = new System.Drawing.Size(768, 379);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupVideoPlayer.Panel)).EndInit();
            this.headerGroupVideoPlayer.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupVideoPlayer)).EndInit();
            this.headerGroupVideoPlayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroupVideoPlayer;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderFavourite;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupFullscreen;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupOriginal;
    }
}
