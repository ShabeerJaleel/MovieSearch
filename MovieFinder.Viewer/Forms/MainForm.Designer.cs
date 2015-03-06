namespace BlueTube.Viewer
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainerMain = new System.Windows.Forms.ToolStripContainer();
            this.kryptonSplitContainerMain = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.kryptonSplitContainerSubMain = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.headerFavourite = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.headerBrowse = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.headerView = new ComponentFactory.Krypton.Toolkit.KryptonHeader();
            this.panelViews = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.labelLoading = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.applicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.nextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.previousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.fullscreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoWebsiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonManager1 = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.searchPanelWidget = new BlueTube.Viewer.SearchPanelWidget();
            this.viewerWindow = new BlueTube.Viewer.ViewWindow();
            this.browseWindow = new BlueTube.Viewer.BrowseWindow();
            this.favouriteWindow = new BlueTube.Viewer.Views.FavouriteWindow();
            this.toolStripContainerMain.ContentPanel.SuspendLayout();
            this.toolStripContainerMain.TopToolStripPanel.SuspendLayout();
            this.toolStripContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel2)).BeginInit();
            this.kryptonSplitContainerMain.Panel2.SuspendLayout();
            this.kryptonSplitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerSubMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerSubMain.Panel1)).BeginInit();
            this.kryptonSplitContainerSubMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerSubMain.Panel2)).BeginInit();
            this.kryptonSplitContainerSubMain.Panel2.SuspendLayout();
            this.kryptonSplitContainerSubMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelViews)).BeginInit();
            this.panelViews.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainerMain
            // 
            // 
            // toolStripContainerMain.ContentPanel
            // 
            this.toolStripContainerMain.ContentPanel.Controls.Add(this.kryptonSplitContainerMain);
            this.toolStripContainerMain.ContentPanel.Size = new System.Drawing.Size(1097, 591);
            this.toolStripContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainerMain.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainerMain.Name = "toolStripContainerMain";
            this.toolStripContainerMain.Size = new System.Drawing.Size(1097, 615);
            this.toolStripContainerMain.TabIndex = 0;
            this.toolStripContainerMain.Text = "toolStripContainer1";
            // 
            // toolStripContainerMain.TopToolStripPanel
            // 
            this.toolStripContainerMain.TopToolStripPanel.Controls.Add(this.menuStrip1);
            // 
            // kryptonSplitContainerMain
            // 
            this.kryptonSplitContainerMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainerMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.kryptonSplitContainerMain.IsSplitterFixed = true;
            this.kryptonSplitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainerMain.Name = "kryptonSplitContainerMain";
            this.kryptonSplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.kryptonSplitContainerMain.Panel1Collapsed = true;
            // 
            // kryptonSplitContainerMain.Panel2
            // 
            this.kryptonSplitContainerMain.Panel2.Controls.Add(this.kryptonSplitContainerSubMain);
            this.kryptonSplitContainerMain.Size = new System.Drawing.Size(1097, 591);
            this.kryptonSplitContainerMain.SplitterDistance = 394;
            this.kryptonSplitContainerMain.SplitterWidth = 0;
            this.kryptonSplitContainerMain.TabIndex = 0;
            // 
            // kryptonSplitContainerSubMain
            // 
            this.kryptonSplitContainerSubMain.Cursor = System.Windows.Forms.Cursors.Default;
            this.kryptonSplitContainerSubMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonSplitContainerSubMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.kryptonSplitContainerSubMain.IsSplitterFixed = true;
            this.kryptonSplitContainerSubMain.Location = new System.Drawing.Point(0, 0);
            this.kryptonSplitContainerSubMain.Name = "kryptonSplitContainerSubMain";
            this.kryptonSplitContainerSubMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // kryptonSplitContainerSubMain.Panel1
            // 
            this.kryptonSplitContainerSubMain.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // kryptonSplitContainerSubMain.Panel2
            // 
            this.kryptonSplitContainerSubMain.Panel2.Controls.Add(this.panelViews);
            this.kryptonSplitContainerSubMain.Panel2.Controls.Add(this.statusStrip1);
            this.kryptonSplitContainerSubMain.Size = new System.Drawing.Size(1097, 591);
            this.kryptonSplitContainerSubMain.SplitterDistance = 27;
            this.kryptonSplitContainerSubMain.SplitterWidth = 0;
            this.kryptonSplitContainerSubMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 524F));
            this.tableLayoutPanel1.Controls.Add(this.headerFavourite, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.headerBrowse, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.headerView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.searchPanelWidget, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1097, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // headerFavourite
            // 
            this.headerFavourite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.headerFavourite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerFavourite.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Calendar;
            this.headerFavourite.Location = new System.Drawing.Point(381, 0);
            this.headerFavourite.Margin = new System.Windows.Forms.Padding(0);
            this.headerFavourite.Name = "headerFavourite";
            this.headerFavourite.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.headerFavourite.Size = new System.Drawing.Size(191, 27);
            this.headerFavourite.StateNormal.Back.Color1 = System.Drawing.SystemColors.GrayText;
            this.headerFavourite.StateNormal.Back.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.headerFavourite.StateNormal.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerFavourite.StateNormal.Border.Color1 = System.Drawing.SystemColors.GradientActiveCaption;
            this.headerFavourite.StateNormal.Border.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.headerFavourite.StateNormal.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.headerFavourite.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerFavourite.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerFavourite.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerFavourite.StateNormal.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerFavourite.StateNormal.Content.ShortText.Color1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.headerFavourite.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerFavourite.TabIndex = 10;
            this.headerFavourite.Values.Description = "";
            this.headerFavourite.Values.Heading = "Favourite";
            this.headerFavourite.Values.Image = ((System.Drawing.Image)(resources.GetObject("headerFavourite.Values.Image")));
            this.headerFavourite.Click += new System.EventHandler(this.custom2_Click);
            // 
            // headerBrowse
            // 
            this.headerBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.headerBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerBrowse.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Calendar;
            this.headerBrowse.Location = new System.Drawing.Point(190, 0);
            this.headerBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.headerBrowse.Name = "headerBrowse";
            this.headerBrowse.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleBlue;
            this.headerBrowse.Size = new System.Drawing.Size(191, 27);
            this.headerBrowse.StateNormal.Back.Color1 = System.Drawing.SystemColors.GrayText;
            this.headerBrowse.StateNormal.Back.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.headerBrowse.StateNormal.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerBrowse.StateNormal.Border.Color1 = System.Drawing.SystemColors.GradientActiveCaption;
            this.headerBrowse.StateNormal.Border.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.headerBrowse.StateNormal.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.headerBrowse.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerBrowse.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerBrowse.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerBrowse.StateNormal.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerBrowse.StateNormal.Content.ShortText.Color1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.headerBrowse.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerBrowse.TabIndex = 9;
            this.headerBrowse.Values.Description = "";
            this.headerBrowse.Values.Heading = "Browse";
            this.headerBrowse.Values.Image = global::BlueTube.Viewer.Properties.Resources.browse;
            this.headerBrowse.Click += new System.EventHandler(this.custom2_Click);
            // 
            // headerView
            // 
            this.headerView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.headerView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerView.HeaderStyle = ComponentFactory.Krypton.Toolkit.HeaderStyle.Calendar;
            this.headerView.Location = new System.Drawing.Point(0, 0);
            this.headerView.Margin = new System.Windows.Forms.Padding(0);
            this.headerView.Name = "headerView";
            this.headerView.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.headerView.Size = new System.Drawing.Size(190, 27);
            this.headerView.StateNormal.Back.Color1 = System.Drawing.SystemColors.InactiveCaptionText;
            this.headerView.StateNormal.Back.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.headerView.StateNormal.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerView.StateNormal.Border.Color1 = System.Drawing.SystemColors.GradientInactiveCaption;
            this.headerView.StateNormal.Border.Color2 = System.Drawing.SystemColors.InactiveCaption;
            this.headerView.StateNormal.Border.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.Sigma;
            this.headerView.StateNormal.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.headerView.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.headerView.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.headerView.StateNormal.Content.Image.ImageH = ComponentFactory.Krypton.Toolkit.PaletteRelativeAlign.Far;
            this.headerView.StateNormal.Content.ShortText.Color1 = System.Drawing.SystemColors.Window;
            this.headerView.StateNormal.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerView.TabIndex = 8;
            this.headerView.Values.Description = "";
            this.headerView.Values.Heading = "View";
            this.headerView.Values.Image = global::BlueTube.Viewer.Properties.Resources.video;
            this.headerView.Click += new System.EventHandler(this.custom2_Click);
            // 
            // panelViews
            // 
            this.panelViews.Controls.Add(this.labelLoading);
            this.panelViews.Controls.Add(this.viewerWindow);
            this.panelViews.Controls.Add(this.browseWindow);
            this.panelViews.Controls.Add(this.favouriteWindow);
            this.panelViews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelViews.Location = new System.Drawing.Point(0, 0);
            this.panelViews.Name = "panelViews";
            this.panelViews.Size = new System.Drawing.Size(1097, 542);
            this.panelViews.TabIndex = 2;
            // 
            // labelLoading
            // 
            this.labelLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLoading.Location = new System.Drawing.Point(52, 45);
            this.labelLoading.Name = "labelLoading";
            this.labelLoading.Size = new System.Drawing.Size(98, 28);
            this.labelLoading.StateCommon.ShortText.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLoading.TabIndex = 4;
            this.labelLoading.Values.Text = "Loading...";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStrip1.Location = new System.Drawing.Point(0, 542);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip1.Size = new System.Drawing.Size(1097, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.applicationToolStripMenuItem,
            this.playToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1097, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // applicationToolStripMenuItem
            // 
            this.applicationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.applicationToolStripMenuItem.Name = "applicationToolStripMenuItem";
            this.applicationToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.applicationToolStripMenuItem.Text = "Application";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pauseToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.toolStripMenuItem1,
            this.nextToolStripMenuItem,
            this.previousToolStripMenuItem,
            this.toolStripMenuItem2,
            this.fullscreenToolStripMenuItem});
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Visible = false;
            this.playToolStripMenuItem.DropDownOpening += new System.EventHandler(this.playToolStripMenuItem_DropDownOpening);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.pauseToolStripMenuItem.Text = "Pause";
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(124, 6);
            // 
            // nextToolStripMenuItem
            // 
            this.nextToolStripMenuItem.Name = "nextToolStripMenuItem";
            this.nextToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.nextToolStripMenuItem.Text = "Next";
            // 
            // previousToolStripMenuItem
            // 
            this.previousToolStripMenuItem.Name = "previousToolStripMenuItem";
            this.previousToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.previousToolStripMenuItem.Text = "Previous";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(124, 6);
            // 
            // fullscreenToolStripMenuItem
            // 
            this.fullscreenToolStripMenuItem.Name = "fullscreenToolStripMenuItem";
            this.fullscreenToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.fullscreenToolStripMenuItem.Text = "Fullscreen";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gotoWebsiteToolStripMenuItem,
            this.reportIssueToolStripMenuItem,
            this.helpToolStripMenuItem1,
            this.toolStripMenuItem3,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // gotoWebsiteToolStripMenuItem
            // 
            this.gotoWebsiteToolStripMenuItem.Name = "gotoWebsiteToolStripMenuItem";
            this.gotoWebsiteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.gotoWebsiteToolStripMenuItem.Text = "Goto Website";
            this.gotoWebsiteToolStripMenuItem.Click += new System.EventHandler(this.gotoWebsiteToolStripMenuItem_Click);
            // 
            // reportIssueToolStripMenuItem
            // 
            this.reportIssueToolStripMenuItem.Name = "reportIssueToolStripMenuItem";
            this.reportIssueToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.reportIssueToolStripMenuItem.Text = "Report issue";
            this.reportIssueToolStripMenuItem.Click += new System.EventHandler(this.reportIssueToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(145, 22);
            this.helpToolStripMenuItem1.Text = "Help";
            this.helpToolStripMenuItem1.Click += new System.EventHandler(this.helpToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(142, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // kryptonManager1
            // 
            this.kryptonManager1.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Office2007Blue;
            // 
            // searchPanelWidget
            // 
            this.searchPanelWidget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanelWidget.Location = new System.Drawing.Point(572, 0);
            this.searchPanelWidget.Margin = new System.Windows.Forms.Padding(0);
            this.searchPanelWidget.Name = "searchPanelWidget";
            this.searchPanelWidget.Size = new System.Drawing.Size(525, 27);
            this.searchPanelWidget.TabIndex = 7;
            this.searchPanelWidget.Search += new System.EventHandler<BlueTube.Viewer.SearchEventArgs>(this.searchPanelWidget_Search);
            // 
            // viewerWindow
            // 
            this.viewerWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerWindow.Location = new System.Drawing.Point(0, 0);
            this.viewerWindow.Name = "viewerWindow";
            this.viewerWindow.Size = new System.Drawing.Size(1097, 542);
            this.viewerWindow.TabIndex = 1;
            // 
            // browseWindow
            // 
            this.browseWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browseWindow.Location = new System.Drawing.Point(0, 0);
            this.browseWindow.Name = "browseWindow";
            this.browseWindow.Size = new System.Drawing.Size(1097, 542);
            this.browseWindow.TabIndex = 2;
            // 
            // favouriteWindow
            // 
            this.favouriteWindow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favouriteWindow.Location = new System.Drawing.Point(0, 0);
            this.favouriteWindow.Name = "favouriteWindow";
            this.favouriteWindow.Size = new System.Drawing.Size(1097, 542);
            this.favouriteWindow.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 615);
            this.Controls.Add(this.toolStripContainerMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "MainForm";
            this.Text = "MaxTube";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.toolStripContainerMain.ContentPanel.ResumeLayout(false);
            this.toolStripContainerMain.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainerMain.TopToolStripPanel.PerformLayout();
            this.toolStripContainerMain.ResumeLayout(false);
            this.toolStripContainerMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain.Panel2)).EndInit();
            this.kryptonSplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerMain)).EndInit();
            this.kryptonSplitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerSubMain.Panel1)).EndInit();
            this.kryptonSplitContainerSubMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerSubMain.Panel2)).EndInit();
            this.kryptonSplitContainerSubMain.Panel2.ResumeLayout(false);
            this.kryptonSplitContainerSubMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonSplitContainerSubMain)).EndInit();
            this.kryptonSplitContainerSubMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelViews)).EndInit();
            this.panelViews.ResumeLayout(false);
            this.panelViews.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainerMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem applicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainerMain;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainerSubMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ViewWindow viewerWindow;
        private SearchPanelWidget searchPanelWidget;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader headerFavourite;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader headerBrowse;
        private ComponentFactory.Krypton.Toolkit.KryptonHeader headerView;
        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panelViews;
        private BrowseWindow browseWindow;
        private Views.FavouriteWindow favouriteWindow;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem previousToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fullscreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gotoWebsiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelLoading;
    }
}

