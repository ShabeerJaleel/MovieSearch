namespace MovieTube.Viewer
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.searchTextbox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.buttonSpecClear = new ComponentFactory.Krypton.Toolkit.ButtonSpecAny();
            this.searchButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonComboBoxYear = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonComboBoxLanguage = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.kryptonContextMenuItems2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.kryptonContextMenuItems3 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBoxYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBoxLanguage)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.tableLayoutPanel1);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(388, 25);
            this.kryptonPanel1.StateNormal.Color1 = System.Drawing.SystemColors.GrayText;
            this.kryptonPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.searchTextbox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.searchButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonComboBoxYear, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.kryptonComboBoxLanguage, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(388, 25);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // searchTextbox
            // 
            this.searchTextbox.AllowButtonSpecToolTips = true;
            this.searchTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextbox.ButtonSpecs.AddRange(new ComponentFactory.Krypton.Toolkit.ButtonSpecAny[] {
            this.buttonSpecClear});
            this.searchTextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTextbox.Location = new System.Drawing.Point(0, 0);
            this.searchTextbox.Margin = new System.Windows.Forms.Padding(0);
            this.searchTextbox.Name = "searchTextbox";
            this.searchTextbox.Size = new System.Drawing.Size(156, 25);
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
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Location = new System.Drawing.Point(351, 0);
            this.searchButton.Margin = new System.Windows.Forms.Padding(0);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(37, 25);
            this.searchButton.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.searchButton.TabIndex = 2;
            this.searchButton.Values.Image = global::MovieTube.Viewer.Properties.Resources.search;
            this.searchButton.Values.Text = global::MovieTube.Viewer.Properties.Resources.DownloadDirectory;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // kryptonComboBoxYear
            // 
            this.kryptonComboBoxYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonComboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kryptonComboBoxYear.DropDownWidth = 86;
            this.kryptonComboBoxYear.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.kryptonComboBoxYear.Items.AddRange(new object[] {
            "(Year)",
            "(New)",
            "2014",
            "2013",
            "2012",
            "2011",
            "2010",
            "2009",
            "2008",
            "2007",
            "2006",
            "2005",
            "2004",
            "2003",
            "2002",
            "2001",
            "2000",
            "1999",
            "1998",
            "1997",
            "1996",
            "1995",
            "1994",
            "1993",
            "1992",
            "1991",
            "1990",
            "1989",
            "1988",
            "1987",
            "1986",
            "1985",
            "1984",
            "1983",
            "1982",
            "1981",
            "1980",
            "1979",
            "1978",
            "1977",
            "1976",
            "1975",
            "1974",
            "1972",
            "1971",
            "1970",
            "1960",
            "1950",
            "1920"});
            this.kryptonComboBoxYear.Location = new System.Drawing.Point(276, 0);
            this.kryptonComboBoxYear.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonComboBoxYear.MinimumSize = new System.Drawing.Size(0, 23);
            this.kryptonComboBoxYear.Name = "kryptonComboBoxYear";
            this.kryptonComboBoxYear.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonComboBoxYear.Size = new System.Drawing.Size(75, 25);
            this.kryptonComboBoxYear.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonComboBoxYear.TabIndex = 5;
            // 
            // kryptonComboBoxLanguage
            // 
            this.kryptonComboBoxLanguage.DisplayMember = "Name";
            this.kryptonComboBoxLanguage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonComboBoxLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.kryptonComboBoxLanguage.DropDownWidth = 86;
            this.kryptonComboBoxLanguage.InputControlStyle = ComponentFactory.Krypton.Toolkit.InputControlStyle.Ribbon;
            this.kryptonComboBoxLanguage.Location = new System.Drawing.Point(156, 0);
            this.kryptonComboBoxLanguage.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonComboBoxLanguage.Name = "kryptonComboBoxLanguage";
            this.kryptonComboBoxLanguage.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.ProfessionalSystem;
            this.kryptonComboBoxLanguage.Size = new System.Drawing.Size(120, 25);
            this.kryptonComboBoxLanguage.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonComboBoxLanguage.TabIndex = 4;
            this.kryptonComboBoxLanguage.ValueMember = "Id";
            // 
            // SearchPanelWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.kryptonPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SearchPanelWidget";
            this.Size = new System.Drawing.Size(388, 25);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBoxYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBoxLanguage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox searchTextbox;
        private ComponentFactory.Krypton.Toolkit.ButtonSpecAny buttonSpecClear;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems2;
        private ComponentFactory.Krypton.Toolkit.KryptonButton searchButton;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems kryptonContextMenuItems3;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox kryptonComboBoxLanguage;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox kryptonComboBoxYear;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
