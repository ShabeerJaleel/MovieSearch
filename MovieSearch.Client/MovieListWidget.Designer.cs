namespace MovieFinder.Client
{
    partial class MovieListWidget
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
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle1 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle2 = new BrightIdeasSoftware.HeaderStateStyle();
            BrightIdeasSoftware.HeaderStateStyle headerStateStyle3 = new BrightIdeasSoftware.HeaderStateStyle();
            this.objectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.headerFormatStyle = new BrightIdeasSoftware.HeaderFormatStyle();
            this.olvColumnLanguage = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnYear = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLink1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLink2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelButton = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.olvColumnLink3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnLink4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.olvColumnName);
            this.objectListView.AllColumns.Add(this.olvColumnLanguage);
            this.objectListView.AllColumns.Add(this.olvColumnYear);
            this.objectListView.AllColumns.Add(this.olvColumnLink1);
            this.objectListView.AllColumns.Add(this.olvColumnLink2);
            this.objectListView.AllColumns.Add(this.olvColumnLink3);
            this.objectListView.AllColumns.Add(this.olvColumnLink4);
            this.objectListView.AllColumns.Add(this.olvColumnDescription);
            this.objectListView.AllowColumnReorder = true;
            this.objectListView.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(220)))));
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnLanguage,
            this.olvColumnYear,
            this.olvColumnLink1,
            this.olvColumnLink2,
            this.olvColumnLink3,
            this.olvColumnLink4,
            this.olvColumnDescription});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectListView.HeaderUsesThemes = false;
            this.objectListView.Location = new System.Drawing.Point(642, 269);
            this.objectListView.Name = "objectListView";
            this.objectListView.OverlayImage.Image = global::MovieFinder.Client.Properties.Resources.overlay;
            this.objectListView.ShowGroups = false;
            this.objectListView.Size = new System.Drawing.Size(348, 120);
            this.objectListView.TabIndex = 0;
            this.objectListView.UseAlternatingBackColors = true;
            this.objectListView.UseCellFormatEvents = true;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.UseFilterIndicator = true;
            this.objectListView.UseFiltering = true;
            this.objectListView.UseHotItem = true;
            this.objectListView.UseHyperlinks = true;
            this.objectListView.UseTranslucentHotItem = true;
            this.objectListView.View = System.Windows.Forms.View.Details;
            this.objectListView.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.objectListView_CellToolTipShowing);
            this.objectListView.HyperlinkClicked += new System.EventHandler<BrightIdeasSoftware.HyperlinkClickedEventArgs>(this.objectListView_HyperlinkClicked);
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "Name";
            this.olvColumnName.CellPadding = null;
            this.olvColumnName.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnName.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnName.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnName.IsEditable = false;
            this.olvColumnName.Text = "Movie Name";
            this.olvColumnName.ToolTipText = "Movie name";
            this.olvColumnName.Width = 300;
            this.olvColumnName.WordWrap = true;
            // 
            // headerFormatStyle
            // 
            headerStateStyle1.Font = new System.Drawing.Font("Tempus Sans ITC", 12F);
            headerStateStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.headerFormatStyle.Hot = headerStateStyle1;
            headerStateStyle2.Font = new System.Drawing.Font("Tempus Sans ITC", 12F);
            headerStateStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.headerFormatStyle.Normal = headerStateStyle2;
            headerStateStyle3.Font = new System.Drawing.Font("Tempus Sans ITC", 12F);
            headerStateStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.headerFormatStyle.Pressed = headerStateStyle3;
            // 
            // olvColumnLanguage
            // 
            this.olvColumnLanguage.AspectName = "Language";
            this.olvColumnLanguage.CellPadding = null;
            this.olvColumnLanguage.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnLanguage.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnLanguage.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnLanguage.IsEditable = false;
            this.olvColumnLanguage.IsTileViewColumn = true;
            this.olvColumnLanguage.Text = "Language";
            this.olvColumnLanguage.UseFiltering = false;
            this.olvColumnLanguage.Width = 100;
            // 
            // olvColumnYear
            // 
            this.olvColumnYear.AspectName = "Year";
            this.olvColumnYear.CellPadding = null;
            this.olvColumnYear.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnYear.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnYear.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnYear.IsTileViewColumn = true;
            this.olvColumnYear.Text = "Year";
            // 
            // olvColumnLink1
            // 
            this.olvColumnLink1.AspectName = "Link1.Title";
            this.olvColumnLink1.CellPadding = null;
            this.olvColumnLink1.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnLink1.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnLink1.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnLink1.Hyperlink = true;
            this.olvColumnLink1.IsEditable = false;
            this.olvColumnLink1.Text = "Link";
            this.olvColumnLink1.UseFiltering = false;
            // 
            // olvColumnLink2
            // 
            this.olvColumnLink2.AspectName = "Link2.Title";
            this.olvColumnLink2.CellPadding = null;
            this.olvColumnLink2.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnLink2.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnLink2.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnLink2.Hyperlink = true;
            this.olvColumnLink2.IsEditable = false;
            this.olvColumnLink2.Text = "Link";
            this.olvColumnLink2.UseFiltering = false;
            // 
            // olvColumnDescription
            // 
            this.olvColumnDescription.AspectName = "Description";
            this.olvColumnDescription.CellPadding = null;
            this.olvColumnDescription.FillsFreeSpace = true;
            this.olvColumnDescription.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnDescription.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnDescription.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnDescription.IsEditable = false;
            this.olvColumnDescription.IsTileViewColumn = true;
            this.olvColumnDescription.Text = "Description";
            this.olvColumnDescription.ToolTipText = "Movie description";
            this.olvColumnDescription.UseFiltering = false;
            this.olvColumnDescription.Width = 100;
            this.olvColumnDescription.WordWrap = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.objectListView);
            this.splitContainer1.Size = new System.Drawing.Size(990, 423);
            this.splitContainer1.SplitterDistance = 28;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Controls.Add(this.panelButton);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(990, 28);
            this.panel1.TabIndex = 0;
            // 
            // panelButton
            // 
            this.panelButton.Location = new System.Drawing.Point(0, 1);
            this.panelButton.Margin = new System.Windows.Forms.Padding(0);
            this.panelButton.Name = "panelButton";
            this.panelButton.Size = new System.Drawing.Size(667, 27);
            this.panelButton.TabIndex = 18;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.buttonSearch);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(668, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(322, 28);
            this.panel2.TabIndex = 17;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(80, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(241, 27);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.SystemColors.Control;
            this.buttonSearch.Enabled = false;
            this.buttonSearch.Font = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.buttonSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buttonSearch.Location = new System.Drawing.Point(1, -1);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(81, 30);
            this.buttonSearch.TabIndex = 1;
            this.buttonSearch.Text = "Filter";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonSearch_Paint);
            // 
            // olvColumnLink3
            // 
            this.olvColumnLink3.AspectName = "Link3.Title";
            this.olvColumnLink3.CellPadding = null;
            this.olvColumnLink3.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnLink3.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnLink3.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnLink3.Hyperlink = true;
            this.olvColumnLink3.Text = "Link";
            this.olvColumnLink3.UseFiltering = false;
            // 
            // olvColumnLink4
            // 
            this.olvColumnLink4.AspectName = "Link4.Title";
            this.olvColumnLink4.CellPadding = null;
            this.olvColumnLink4.HeaderFont = new System.Drawing.Font("Tempus Sans ITC", 12F);
            this.olvColumnLink4.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.olvColumnLink4.HeaderFormatStyle = this.headerFormatStyle;
            this.olvColumnLink4.Hyperlink = true;
            this.olvColumnLink4.Text = "Link";
            this.olvColumnLink4.UseFiltering = false;
            // 
            // MovieListWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MovieListWidget";
            this.Size = new System.Drawing.Size(990, 423);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnLanguage;
        private BrightIdeasSoftware.OLVColumn olvColumnYear;
        private BrightIdeasSoftware.OLVColumn olvColumnDescription;
        private BrightIdeasSoftware.OLVColumn olvColumnLink1;
        private BrightIdeasSoftware.OLVColumn olvColumnLink2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonSearch;
        private BrightIdeasSoftware.HeaderFormatStyle headerFormatStyle;
        private System.Windows.Forms.Panel panelButton;
        private BrightIdeasSoftware.OLVColumn olvColumnLink3;
        private BrightIdeasSoftware.OLVColumn olvColumnLink4;
    }
}
