namespace BlueTube.Viewer.Controls
{
    partial class BrowseGalleryWidget
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
            this.panelMain = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.flowLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.flowLayoutPanelPaging = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).BeginInit();
            this.panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).BeginInit();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Controls.Add(this.flowLayoutPanel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(150, 120);
            this.panelMain.TabIndex = 0;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.AutoSize = true;
            this.flowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel.ColumnCount = 6;
            this.flowLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.RowCount = 1;
            this.flowLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.flowLayoutPanel.Size = new System.Drawing.Size(150, 0);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.Resize += new System.EventHandler(this.flowLayoutPanel_Resize);
            // 
            // splitContainer
            // 
            this.splitContainer.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.flowLayoutPanelPaging);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.panelMain);
            this.splitContainer.Size = new System.Drawing.Size(150, 150);
            this.splitContainer.SplitterDistance = 25;
            this.splitContainer.TabIndex = 3;
            // 
            // flowLayoutPanelPaging
            // 
            this.flowLayoutPanelPaging.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanelPaging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanelPaging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelPaging.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelPaging.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelPaging.Name = "flowLayoutPanelPaging";
            this.flowLayoutPanelPaging.Size = new System.Drawing.Size(150, 25);
            this.flowLayoutPanelPaging.TabIndex = 1;
            // 
            // BrowseGalleryWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "BrowseGalleryWidget";
            ((System.ComponentModel.ISupportInitialize)(this.panelMain)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer.Panel2)).EndInit();
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonPanel panelMain;
        private System.Windows.Forms.TableLayoutPanel flowLayoutPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer splitContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPaging;
    }
}
