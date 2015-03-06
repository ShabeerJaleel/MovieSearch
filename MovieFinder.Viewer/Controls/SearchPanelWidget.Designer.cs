namespace BlueTube.Viewer
{
    partial class SearchPanelWidget
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
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.searchButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.dropdownFilter = new ComponentFactory.Krypton.Toolkit.KryptonDropButton();
            this.menuFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenu();
            this.kryptonContextMenuHeading1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.kryptonContextMenuItems1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.relevanceFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.uploadDateFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.ratingFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.kryptonContextMenuHeading2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.anytimeFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.todayFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.weekFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.monthFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.kryptonContextMenuHeading3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading();
            this.durationAllFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.shortDurationFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.durationMediumFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.durationLongFilter = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton();
            this.searchTextbox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.buttonSpecClear = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.searchButton);
            this.kryptonPanel1.Controls.Add(this.dropdownFilter);
            this.kryptonPanel1.Controls.Add(this.searchTextbox);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(388, 27);
            this.kryptonPanel1.StateNormal.Color1 = System.Drawing.SystemColors.GrayText;
            this.kryptonPanel1.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(351, 0);
            this.searchButton.Margin = new System.Windows.Forms.Padding(0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(37, 26);
            this.searchButton.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.searchButton.TabIndex = 2;
            this.searchButton.Values.Image = global::BlueTube.Viewer.Properties.Resources.search;
            this.searchButton.Values.Text = "";
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // dropdownFilter
            // 
            this.dropdownFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dropdownFilter.KryptonContextMenu = this.menuFilter;
            this.dropdownFilter.Location = new System.Drawing.Point(255, 0);
            this.dropdownFilter.Margin = new System.Windows.Forms.Padding(0);
            this.dropdownFilter.Name = "dropdownFilter";
            this.dropdownFilter.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.dropdownFilter.Size = new System.Drawing.Size(97, 26);
            this.dropdownFilter.Splitter = false;
            this.dropdownFilter.TabIndex = 1;
            this.dropdownFilter.Values.Text = "Filter";
            // 
            // menuFilter
            // 
            this.menuFilter.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.kryptonContextMenuHeading1,
            this.kryptonContextMenuItems1,
            this.relevanceFilter,
            this.uploadDateFilter,
            this.ratingFilter,
            this.kryptonContextMenuHeading2,
            this.anytimeFilter,
            this.todayFilter,
            this.weekFilter,
            this.monthFilter,
            this.kryptonContextMenuHeading3,
            this.durationAllFilter,
            this.shortDurationFilter,
            this.durationMediumFilter,
            this.durationLongFilter});
            // 
            // kryptonContextMenuHeading1
            // 
            this.kryptonContextMenuHeading1.ExtraText = "";
            this.kryptonContextMenuHeading1.Text = "Sort By";
            // 
            // relevanceFilter
            // 
            this.relevanceFilter.Checked = true;
            this.relevanceFilter.ExtraText = "";
            this.relevanceFilter.Text = "Relevance";
            // 
            // uploadDateFilter
            // 
            this.uploadDateFilter.ExtraText = "";
            this.uploadDateFilter.Text = "Upload date";
            // 
            // ratingFilter
            // 
            this.ratingFilter.ExtraText = "";
            this.ratingFilter.Text = "Rating";
            // 
            // kryptonContextMenuHeading2
            // 
            this.kryptonContextMenuHeading2.ExtraText = "";
            this.kryptonContextMenuHeading2.Text = "Date";
            // 
            // anytimeFilter
            // 
            this.anytimeFilter.Checked = true;
            this.anytimeFilter.ExtraText = "";
            this.anytimeFilter.Text = "Anytime";
            // 
            // todayFilter
            // 
            this.todayFilter.ExtraText = "";
            this.todayFilter.Text = "Today";
            // 
            // weekFilter
            // 
            this.weekFilter.ExtraText = "";
            this.weekFilter.Text = "This week";
            // 
            // monthFilter
            // 
            this.monthFilter.ExtraText = "";
            this.monthFilter.Text = "This month";
            // 
            // kryptonContextMenuHeading3
            // 
            this.kryptonContextMenuHeading3.ExtraText = "";
            this.kryptonContextMenuHeading3.Text = "Duration";
            // 
            // durationAllFilter
            // 
            this.durationAllFilter.Checked = true;
            this.durationAllFilter.ExtraText = "";
            this.durationAllFilter.Text = "All";
            // 
            // shortDurationFilter
            // 
            this.shortDurationFilter.ExtraText = "";
            this.shortDurationFilter.Text = "Short";
            // 
            // durationMediumFilter
            // 
            this.durationMediumFilter.ExtraText = "";
            this.durationMediumFilter.Text = "Medium";
            // 
            // durationLongFilter
            // 
            this.durationLongFilter.ExtraText = "";
            this.durationLongFilter.Text = "Long";
            // 
            // searchTextbox
            // 
            this.searchTextbox.AllowButtonSpecToolTips = true;
            this.searchTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextbox.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecClear});
            this.searchTextbox.Location = new System.Drawing.Point(1, 1);
            this.searchTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(254, 25);
            this.searchTextbox.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.searchTextbox.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.searchTextbox.StateCommon.Border.Rounding = 1;
            this.searchTextbox.StateCommon.Border.Width = 1;
            this.searchTextbox.StateCommon.Content.Color1 = System.Drawing.SystemColors.InactiveCaption;
            this.searchTextbox.StateCommon.Content.Font = new System.Drawing.Font("Tahoma", 11F);
            this.searchTextbox.TabIndex = 1;
            this.searchTextbox.TextChanged += new System.EventHandler(this.kryptonTextBox1_TextChanged);
            this.searchTextbox.Enter += new System.EventHandler(this.kryptonTextBox1_Enter);
            this.searchTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.kryptonTextBox1_KeyDown);
            this.searchTextbox.Leave += new System.EventHandler(this.kryptonTextBox1_Leave);
            // 
            // buttonSpecClear
            // 
            this.buttonSpecClear.Type = ComponentFactory.Krypton.Toolkit.PaletteButtonSpecStyle.PendantClose;
            this.buttonSpecClear.UniqueName = "CD6A6F12A57E4A8F0D9BA852FFDF870B";
            this.buttonSpecClear.Click += new System.EventHandler(this.buttonSpecClear_Click);
            // 
            // SearchPanelWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SearchPanelWidget";
            this.Size = new System.Drawing.Size(388, 27);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox searchTextbox;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecClear;
        private ComponentFactory.Krypton.Toolkit.KryptonDropButton dropdownFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenu menuFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton relevanceFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton uploadDateFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton ratingFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading2;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton anytimeFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton todayFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton weekFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton monthFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuHeading kryptonContextMenuHeading3;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton shortDurationFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton durationMediumFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton durationLongFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuRadioButton durationAllFilter;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton searchButton;
    }
}
