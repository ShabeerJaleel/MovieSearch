namespace MovieTube.Viewer
{
    partial class ViewWindow
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
            this.kryptonSplitContainerTop = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.wmpVideoPlayerWidget = new MovieTube.Viewer.WMPVideoPlayerWidget();
            this.headerGroupSearchResult = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroupSearchResult = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroupMore = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.videoLinksGalleryWidget = new MovieTube.Viewer.VerticalSingleColumnGalleryWidget();
            this.kryptonHeaderGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroupPrevious = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroupNext = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroupCollapse = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.videoThumbGalleryWidget = new MovieTube.Viewer.HorizontalSingleRowGalleryWidget();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerTop.Panel1)).BeginInit();
            this.kryptonSplitContainerTop.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerTop.Panel2)).BeginInit();
            this.kryptonSplitContainerTop.Panel2.SuspendLayout();
            this.kryptonSplitContainerTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult.Panel)).BeginInit();
            this.headerGroupSearchResult.Panel.SuspendLayout();
            this.headerGroupSearchResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup.Panel)).BeginInit();
            this.kryptonHeaderGroup.Panel.SuspendLayout();
            this.kryptonHeaderGroup.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonSplitContainerTop
            // 
            this.kryptonSplitContainerTop.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainerTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainerTop.Location = new System.Drawing.Point(3, 3);
            this.kryptonSplitContainerTop.Name = "kryptonSplitContainerTop";
            // 
            // kryptonSplitContainerTop.Panel1
            // 
            this.kryptonSplitContainerTop.Panel1.Controls.Add(this.wmpVideoPlayerWidget);
            // 
            // kryptonSplitContainerTop.Panel2
            // 
            this.kryptonSplitContainerTop.Panel2.Controls.Add(this.headerGroupSearchResult);
            this.kryptonSplitContainerTop.Size = new System.Drawing.Size(929, 271);
            this.kryptonSplitContainerTop.SplitterDistance = 820;
            this.kryptonSplitContainerTop.TabIndex = 5;
            // 
            // wmpVideoPlayerWidget
            // 
            this.wmpVideoPlayerWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wmpVideoPlayerWidget.Location = new System.Drawing.Point(0, 0);
            this.wmpVideoPlayerWidget.Name = "wmpVideoPlayerWidget";
            this.wmpVideoPlayerWidget.Size = new System.Drawing.Size(820, 271);
            this.wmpVideoPlayerWidget.TabIndex = 0;
            // 
            // headerGroupSearchResult
            // 
            this.headerGroupSearchResult.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroupSearchResult,
            this.buttonSpecHeaderGroupMore});
            this.headerGroupSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerGroupSearchResult.HeaderStyleSecondary = ComponentFactory.Krypton.Toolkit.HeaderStyle.Calendar;
            this.headerGroupSearchResult.Location = new System.Drawing.Point(0, 0);
            this.headerGroupSearchResult.Margin = new System.Windows.Forms.Padding(0);
            this.headerGroupSearchResult.Name = "headerGroupSearchResult";
            // 
            // headerGroupSearchResult.Panel
            // 
            this.headerGroupSearchResult.Panel.Controls.Add(this.videoLinksGalleryWidget);
            this.headerGroupSearchResult.Size = new System.Drawing.Size(104, 271);
            this.headerGroupSearchResult.TabIndex = 0;
            this.headerGroupSearchResult.ValuesPrimary.Heading = "Links";
            this.headerGroupSearchResult.ValuesPrimary.Image = global::MovieTube.Viewer.Properties.Resources.search;
            this.headerGroupSearchResult.ValuesSecondary.Heading = global::MovieTube.Viewer.Properties.Resources.DownloadDirectory;
            // 
            // buttonSpecHeaderGroupSearchResult
            // 
            this.buttonSpecHeaderGroupSearchResult.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowRight;
            this.buttonSpecHeaderGroupSearchResult.UniqueName = "90FF6DCCE41046173AA0902A801FBF30";
            this.buttonSpecHeaderGroupSearchResult.Click += new System.EventHandler(this.buttonSpecHeaderGroupSearchResult_Click);
            // 
            // buttonSpecHeaderGroupMore
            // 
            this.buttonSpecHeaderGroupMore.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Far;
            this.buttonSpecHeaderGroupMore.ExtraText = "More";
            this.buttonSpecHeaderGroupMore.HeaderLocation = ComponentFactory.Krypton.Toolkit.HeaderLocation.SecondaryHeader;
            this.buttonSpecHeaderGroupMore.Orientation = ComponentFactory.Krypton.Toolkit.PaletteButtonOrientation.Auto;
            this.buttonSpecHeaderGroupMore.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.NavigatorStack;
            this.buttonSpecHeaderGroupMore.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.RibbonExpand;
            this.buttonSpecHeaderGroupMore.UniqueName = "C2B83C55CBE14DE3F1994DD155091842";
            this.buttonSpecHeaderGroupMore.Click += new System.EventHandler(this.buttonSpecHeaderGroupMore_Click);
            // 
            // videoLinksGalleryWidget
            // 
            this.videoLinksGalleryWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoLinksGalleryWidget.Location = new System.Drawing.Point(0, 0);
            this.videoLinksGalleryWidget.Name = "videoLinksGalleryWidget";
            this.videoLinksGalleryWidget.Size = new System.Drawing.Size(102, 210);
            this.videoLinksGalleryWidget.TabIndex = 2;
            this.videoLinksGalleryWidget.ItemSelected += new System.EventHandler<MovieTube.Viewer.GalleryItemSelectedEventArgs>(this.verticalSingleColumnGalleryWidget1_ItemSelected);
            this.videoLinksGalleryWidget.DownloadClicked += new System.EventHandler<MovieTube.Viewer.GalleryItemSelectedEventArgs>(this.videoLinksGalleryWidget_DownloadClicked);
            // 
            // kryptonHeaderGroup
            // 
            this.kryptonHeaderGroup.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroupPrevious,
            this.buttonSpecHeaderGroupNext,
            this.buttonSpecHeaderGroupCollapse});
            this.kryptonHeaderGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup.Location = new System.Drawing.Point(3, 280);
            this.kryptonHeaderGroup.Name = "kryptonHeaderGroup";
            // 
            // kryptonHeaderGroup.Panel
            // 
            this.kryptonHeaderGroup.Panel.Controls.Add(this.videoThumbGalleryWidget);
            this.kryptonHeaderGroup.Size = new System.Drawing.Size(929, 190);
            this.kryptonHeaderGroup.TabIndex = 3;
            this.kryptonHeaderGroup.ValuesPrimary.Heading = "New Videos";
            this.kryptonHeaderGroup.ValuesPrimary.Image = global::MovieTube.Viewer.Properties.Resources.related;
            // 
            // buttonSpecHeaderGroupPrevious
            // 
            this.buttonSpecHeaderGroupPrevious.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.buttonSpecHeaderGroupPrevious.ToolTipTitle = "Previous";
            this.buttonSpecHeaderGroupPrevious.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Previous;
            this.buttonSpecHeaderGroupPrevious.UniqueName = "125EC497956141067AB736A7C194BD59";
            this.buttonSpecHeaderGroupPrevious.Click += new System.EventHandler(this.buttonSpecHeaderGroupPrevious_Click);
            // 
            // buttonSpecHeaderGroupNext
            // 
            this.buttonSpecHeaderGroupNext.Enabled = ComponentFactory.Krypton.Toolkit.ButtonEnabled.False;
            this.buttonSpecHeaderGroupNext.ToolTipBody = "Next";
            this.buttonSpecHeaderGroupNext.ToolTipStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.SuperTip;
            this.buttonSpecHeaderGroupNext.ToolTipTitle = "Next";
            this.buttonSpecHeaderGroupNext.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.Next;
            this.buttonSpecHeaderGroupNext.UniqueName = "407FCF4E4C7E473202A642A5B86D3790";
            this.buttonSpecHeaderGroupNext.Click += new System.EventHandler(this.buttonSpecHeaderGroupNext_Click);
            // 
            // buttonSpecHeaderGroupCollapse
            // 
            this.buttonSpecHeaderGroupCollapse.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowDown;
            this.buttonSpecHeaderGroupCollapse.UniqueName = "FDD12D6B57164510EF8A4AFC8131F8CA";
            this.buttonSpecHeaderGroupCollapse.Click += new System.EventHandler(this.buttonSpecHeaderGroupCollapse_Click);
            // 
            // videoThumbGalleryWidget
            // 
            this.videoThumbGalleryWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoThumbGalleryWidget.Location = new System.Drawing.Point(0, 0);
            this.videoThumbGalleryWidget.MinimumSize = new System.Drawing.Size(0, 156);
            this.videoThumbGalleryWidget.Name = "videoThumbGalleryWidget";
            this.videoThumbGalleryWidget.Size = new System.Drawing.Size(927, 158);
            this.videoThumbGalleryWidget.TabIndex = 0;
            this.videoThumbGalleryWidget.ItemSelected += new System.EventHandler<MovieTube.Viewer.GalleryItemSelectedEventArgs>(this.relatedVideoGalleryWidget_ItemSelected);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.kryptonHeaderGroup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.kryptonSplitContainerTop, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(935, 473);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // ViewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ViewWindow";
            this.Size = new System.Drawing.Size(935, 473);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerTop.Panel1)).EndInit();
            this.kryptonSplitContainerTop.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerTop.Panel2)).EndInit();
            this.kryptonSplitContainerTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerTop)).EndInit();
            this.kryptonSplitContainerTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult.Panel)).EndInit();
            this.headerGroupSearchResult.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult)).EndInit();
            this.headerGroupSearchResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup.Panel)).EndInit();
            this.kryptonHeaderGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup)).EndInit();
            this.kryptonHeaderGroup.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroupSearchResult;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupSearchResult;
        private VerticalSingleColumnGalleryWidget videoLinksGalleryWidget;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupCollapse;
        private HorizontalSingleRowGalleryWidget videoThumbGalleryWidget;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupMore;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainerTop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupPrevious;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupNext;
        private WMPVideoPlayerWidget wmpVideoPlayerWidget;
    }
}
