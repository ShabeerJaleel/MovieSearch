namespace BlueTube.Viewer
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
            this.kryptonSplitContainerMain = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.headerGroupSearchResult = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroupSearchResult = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.buttonSpecHeaderGroupMore = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.verticalSingleColumnGalleryWidget1 = new BlueTube.Viewer.VerticalSingleColumnGalleryWidget();
            this.tableLayoutPanelRight = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonHeaderGroup = new ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup();
            this.buttonSpecHeaderGroupCollapse = new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup();
            this.relatedVideoGalleryWidget = new BlueTube.Viewer.HorizontalSingleRowGalleryWidget();
            this.videoPlayerWidget = new BlueTube.Viewer.VideoPlayerWidget();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel1)).BeginInit();
            this.kryptonSplitContainerMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel2)).BeginInit();
            this.kryptonSplitContainerMain.Panel2.SuspendLayout();
            this.kryptonSplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult.Panel)).BeginInit();
            this.headerGroupSearchResult.Panel.SuspendLayout();
            this.headerGroupSearchResult.SuspendLayout();
            this.tableLayoutPanelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup.Panel)).BeginInit();
            this.kryptonHeaderGroup.Panel.SuspendLayout();
            this.kryptonHeaderGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonSplitContainerMain
            // 
            this.kryptonSplitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainerMain.Name = "kryptonSplitContainerMain";
            // 
            // kryptonSplitContainerMain.Panel1
            // 
            this.kryptonSplitContainerMain.Panel1.Controls.Add(this.headerGroupSearchResult);
            this.kryptonSplitContainerMain.Panel1MinSize = 200;
            // 
            // kryptonSplitContainerMain.Panel2
            // 
            this.kryptonSplitContainerMain.Panel2.Controls.Add(this.tableLayoutPanelRight);
            this.kryptonSplitContainerMain.Panel2MinSize = 500;
            this.kryptonSplitContainerMain.Size = new System.Drawing.Size(935, 473);
            this.kryptonSplitContainerMain.SplitterDistance = 213;
            this.kryptonSplitContainerMain.TabIndex = 0;
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
            this.headerGroupSearchResult.Panel.Controls.Add(this.verticalSingleColumnGalleryWidget1);
            this.headerGroupSearchResult.Size = new System.Drawing.Size(213, 473);
            this.headerGroupSearchResult.TabIndex = 0;
            this.headerGroupSearchResult.ValuesPrimary.Heading = "Search Results";
            this.headerGroupSearchResult.ValuesPrimary.Image = global::BlueTube.Viewer.Properties.Resources.search;
            this.headerGroupSearchResult.ValuesSecondary.Heading = "";
            // 
            // buttonSpecHeaderGroupSearchResult
            // 
            this.buttonSpecHeaderGroupSearchResult.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
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
            // verticalSingleColumnGalleryWidget1
            // 
            this.verticalSingleColumnGalleryWidget1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verticalSingleColumnGalleryWidget1.Location = new System.Drawing.Point(0, 0);
            this.verticalSingleColumnGalleryWidget1.Name = "verticalSingleColumnGalleryWidget1";
            this.verticalSingleColumnGalleryWidget1.Size = new System.Drawing.Size(211, 412);
            this.verticalSingleColumnGalleryWidget1.TabIndex = 2;
            this.verticalSingleColumnGalleryWidget1.ItemSelected += new System.EventHandler<BlueTube.Viewer.GalleryItemSelectedEventArgs>(this.verticalSingleColumnGalleryWidget1_ItemSelected);
            // 
            // tableLayoutPanelRight
            // 
            this.tableLayoutPanelRight.ColumnCount = 1;
            this.tableLayoutPanelRight.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.Controls.Add(this.kryptonHeaderGroup, 0, 1);
            this.tableLayoutPanelRight.Controls.Add(this.videoPlayerWidget, 0, 0);
            this.tableLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelRight.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelRight.Name = "tableLayoutPanelRight";
            this.tableLayoutPanelRight.RowCount = 2;
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelRight.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelRight.Size = new System.Drawing.Size(717, 473);
            this.tableLayoutPanelRight.TabIndex = 4;
            // 
            // kryptonHeaderGroup
            // 
            this.kryptonHeaderGroup.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup[] {
            this.buttonSpecHeaderGroupCollapse});
            this.kryptonHeaderGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonHeaderGroup.HeaderVisibleSecondary = false;
            this.kryptonHeaderGroup.Location = new System.Drawing.Point(3, 280);
            this.kryptonHeaderGroup.Name = "kryptonHeaderGroup";
            // 
            // kryptonHeaderGroup.Panel
            // 
            this.kryptonHeaderGroup.Panel.Controls.Add(this.relatedVideoGalleryWidget);
            this.kryptonHeaderGroup.Size = new System.Drawing.Size(711, 190);
            this.kryptonHeaderGroup.TabIndex = 3;
            this.kryptonHeaderGroup.ValuesPrimary.Heading = "Related Videos";
            this.kryptonHeaderGroup.ValuesPrimary.Image = global::BlueTube.Viewer.Properties.Resources.related;
            // 
            // buttonSpecHeaderGroupCollapse
            // 
            this.buttonSpecHeaderGroupCollapse.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.ArrowDown;
            this.buttonSpecHeaderGroupCollapse.UniqueName = "FDD12D6B57164510EF8A4AFC8131F8CA";
            this.buttonSpecHeaderGroupCollapse.Click += new System.EventHandler(this.buttonSpecHeaderGroupCollapse_Click);
            // 
            // relatedVideoGalleryWidget
            // 
            this.relatedVideoGalleryWidget.AutoScroll = true;
            this.relatedVideoGalleryWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.relatedVideoGalleryWidget.Location = new System.Drawing.Point(0, 0);
            this.relatedVideoGalleryWidget.MinimumSize = new System.Drawing.Size(0, 156);
            this.relatedVideoGalleryWidget.Name = "relatedVideoGalleryWidget";
            this.relatedVideoGalleryWidget.Size = new System.Drawing.Size(709, 158);
            this.relatedVideoGalleryWidget.TabIndex = 0;
            this.relatedVideoGalleryWidget.ItemSelected += new System.EventHandler<BlueTube.Viewer.GalleryItemSelectedEventArgs>(this.relatedVideoGalleryWidget_ItemSelected);
            // 
            // videoPlayerWidget
            // 
            this.videoPlayerWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.videoPlayerWidget.Location = new System.Drawing.Point(3, 3);
            this.videoPlayerWidget.Name = "videoPlayerWidget";
            this.videoPlayerWidget.Size = new System.Drawing.Size(711, 271);
            this.videoPlayerWidget.TabIndex = 2;
            this.videoPlayerWidget.ToggelFavourite += new System.EventHandler<BlueTube.Viewer.ToggleFavouriteEventArgs>(this.videoPlayerWidget_ToggelFavourite);
            this.videoPlayerWidget.PlayNext += new System.EventHandler<BlueTube.Viewer.PlayEventArgs>(this.videoPlayerWidget_PlayNext);
            this.videoPlayerWidget.PlayPrevious += new System.EventHandler<BlueTube.Viewer.PlayEventArgs>(this.videoPlayerWidget_PlayPrevious);
            // 
            // ViewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonSplitContainerMain);
            this.Name = "ViewWindow";
            this.Size = new System.Drawing.Size(935, 473);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel1)).EndInit();
            this.kryptonSplitContainerMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel2)).EndInit();
            this.kryptonSplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain)).EndInit();
            this.kryptonSplitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult.Panel)).EndInit();
            this.headerGroupSearchResult.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.headerGroupSearchResult)).EndInit();
            this.headerGroupSearchResult.ResumeLayout(false);
            this.tableLayoutPanelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup.Panel)).EndInit();
            this.kryptonHeaderGroup.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonHeaderGroup)).EndInit();
            this.kryptonHeaderGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainerMain;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup headerGroupSearchResult;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupSearchResult;
        private VerticalSingleColumnGalleryWidget verticalSingleColumnGalleryWidget1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelRight;
        private ComponentFactory.Krypton.Toolkit.KryptonHeaderGroup kryptonHeaderGroup;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupCollapse;
        private HorizontalSingleRowGalleryWidget relatedVideoGalleryWidget;
        private VideoPlayerWidget videoPlayerWidget;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecHeaderGroup buttonSpecHeaderGroupMore;
    }
}
