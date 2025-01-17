﻿namespace MovieTube.Viewer.Controls
{
    partial class PlayerControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControls));
            this.tableLayoutPanelControls = new System.Windows.Forms.TableLayoutPanel();
            this.kryptonPanel4 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.labelTotalTime = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.labelTimeProgress = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonTrackBar1 = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.buttonVolume = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.kryptonTrackBarVolume = new ComponentFactory.Krypton.Toolkit.KryptonTrackBar();
            this.kryptonPanel3 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel2 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.borderEdgeSep3 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.buttonNext = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.buttonPlayPause = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.borderEdgeSep2 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.buttonStop = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.borderEdgeSep1 = new ComponentFactory.Krypton.Toolkit.KryptonBorderEdge();
            this.buttonPrevious = new ComponentFactory.Krypton.Toolkit.KryptonCheckButton();
            this.tableLayoutPanelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).BeginInit();
            this.kryptonPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).BeginInit();
            this.kryptonPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).BeginInit();
            this.kryptonPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelControls
            // 
            this.tableLayoutPanelControls.ColumnCount = 3;
            this.tableLayoutPanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tableLayoutPanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanelControls.Controls.Add(this.kryptonPanel4, 1, 0);
            this.tableLayoutPanelControls.Controls.Add(this.kryptonPanel1, 2, 0);
            this.tableLayoutPanelControls.Controls.Add(this.kryptonPanel3, 0, 0);
            this.tableLayoutPanelControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelControls.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelControls.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelControls.Name = "tableLayoutPanelControls";
            this.tableLayoutPanelControls.RowCount = 1;
            this.tableLayoutPanelControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelControls.Size = new System.Drawing.Size(803, 53);
            this.tableLayoutPanelControls.TabIndex = 2;
            // 
            // kryptonPanel4
            // 
            this.kryptonPanel4.Controls.Add(this.labelTotalTime);
            this.kryptonPanel4.Controls.Add(this.labelTimeProgress);
            this.kryptonPanel4.Controls.Add(this.kryptonTrackBar1);
            this.kryptonPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel4.Location = new System.Drawing.Point(160, 0);
            this.kryptonPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonPanel4.Name = "kryptonPanel4";
            this.kryptonPanel4.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.kryptonPanel4.Size = new System.Drawing.Size(443, 53);
            this.kryptonPanel4.TabIndex = 0;
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.Location = new System.Drawing.Point(6, 27);
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(56, 20);
            this.labelTotalTime.TabIndex = 4;
            this.labelTotalTime.Values.Text = "00:00:00";
            // 
            // labelTimeProgress
            // 
            this.labelTimeProgress.Location = new System.Drawing.Point(6, 10);
            this.labelTimeProgress.Name = "labelTimeProgress";
            this.labelTimeProgress.Size = new System.Drawing.Size(56, 20);
            this.labelTimeProgress.TabIndex = 3;
            this.labelTimeProgress.Values.Text = "00:00:00";
            // 
            // kryptonTrackBar1
            // 
            this.kryptonTrackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonTrackBar1.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.kryptonTrackBar1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kryptonTrackBar1.DrawBackground = true;
            this.kryptonTrackBar1.LargeChange = 10;
            this.kryptonTrackBar1.Location = new System.Drawing.Point(70, 10);
            this.kryptonTrackBar1.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonTrackBar1.Maximum = 1000;
            this.kryptonTrackBar1.Name = "kryptonTrackBar1";
            this.kryptonTrackBar1.Size = new System.Drawing.Size(373, 39);
            this.kryptonTrackBar1.TabIndex = 2;
            this.kryptonTrackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.kryptonTrackBar1.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Large;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.buttonVolume);
            this.kryptonPanel1.Controls.Add(this.kryptonTrackBarVolume);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(603, 0);
            this.kryptonPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.kryptonPanel1.Size = new System.Drawing.Size(200, 53);
            this.kryptonPanel1.TabIndex = 2;
            // 
            // buttonVolume
            // 
            this.buttonVolume.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonVolume.Location = new System.Drawing.Point(23, 0);
            this.buttonVolume.Margin = new System.Windows.Forms.Padding(0);
            this.buttonVolume.Name = "buttonVolume";
            this.buttonVolume.Size = new System.Drawing.Size(47, 45);
            this.buttonVolume.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonVolume.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonVolume.StateCommon.Border.Rounding = 8;
            this.buttonVolume.StateCommon.Border.Width = 2;
            this.buttonVolume.TabIndex = 8;
            this.buttonVolume.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonVolume.Values.Image")));
            this.buttonVolume.Values.Text = "";
            // 
            // kryptonTrackBarVolume
            // 
            this.kryptonTrackBarVolume.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.kryptonTrackBarVolume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.kryptonTrackBarVolume.DrawBackground = true;
            this.kryptonTrackBarVolume.Location = new System.Drawing.Point(77, 10);
            this.kryptonTrackBarVolume.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.kryptonTrackBarVolume.Maximum = 100;
            this.kryptonTrackBarVolume.Name = "kryptonTrackBarVolume";
            this.kryptonTrackBarVolume.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.kryptonTrackBarVolume.Size = new System.Drawing.Size(123, 34);
            this.kryptonTrackBarVolume.TabIndex = 1;
            this.kryptonTrackBarVolume.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.kryptonTrackBarVolume.TrackBarSize = ComponentFactory.Krypton.Toolkit.PaletteTrackBarSize.Large;
            this.kryptonTrackBarVolume.VolumeControl = true;
            // 
            // kryptonPanel3
            // 
            this.kryptonPanel3.Controls.Add(this.kryptonPanel2);
            this.kryptonPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel3.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonPanel3.Name = "kryptonPanel3";
            this.kryptonPanel3.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.kryptonPanel3.Size = new System.Drawing.Size(160, 53);
            this.kryptonPanel3.TabIndex = 3;
            // 
            // kryptonPanel2
            // 
            this.kryptonPanel2.AutoSize = true;
            this.kryptonPanel2.Controls.Add(this.borderEdgeSep3);
            this.kryptonPanel2.Controls.Add(this.buttonNext);
            this.kryptonPanel2.Controls.Add(this.buttonPlayPause);
            this.kryptonPanel2.Controls.Add(this.borderEdgeSep2);
            this.kryptonPanel2.Controls.Add(this.buttonStop);
            this.kryptonPanel2.Controls.Add(this.borderEdgeSep1);
            this.kryptonPanel2.Controls.Add(this.buttonPrevious);
            this.kryptonPanel2.Location = new System.Drawing.Point(3, 1);
            this.kryptonPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.kryptonPanel2.Name = "kryptonPanel2";
            this.kryptonPanel2.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.kryptonPanel2.Size = new System.Drawing.Size(146, 42);
            this.kryptonPanel2.TabIndex = 4;
            // 
            // borderEdgeSep3
            // 
            this.borderEdgeSep3.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeSep3.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderEdgeSep3.Location = new System.Drawing.Point(108, 0);
            this.borderEdgeSep3.Name = "borderEdgeSep3";
            this.borderEdgeSep3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeSep3.Size = new System.Drawing.Size(1, 42);
            this.borderEdgeSep3.StateCommon.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.borderEdgeSep3.Text = "kryptonBorderEdge2";
            // 
            // buttonNext
            // 
            this.buttonNext.AutoSize = true;
            this.buttonNext.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonNext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonNext.Location = new System.Drawing.Point(108, 0);
            this.buttonNext.Margin = new System.Windows.Forms.Padding(0);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(38, 42);
            this.buttonNext.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.buttonNext.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonNext.StateCommon.Border.Rounding = 8;
            this.buttonNext.TabIndex = 9;
            this.buttonNext.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonNext.Values.Image")));
            this.buttonNext.Values.Text = "";
            // 
            // buttonPlayPause
            // 
            this.buttonPlayPause.AutoSize = true;
            this.buttonPlayPause.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPlayPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPlayPause.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPlayPause.Location = new System.Drawing.Point(74, 0);
            this.buttonPlayPause.Name = "buttonPlayPause";
            this.buttonPlayPause.Size = new System.Drawing.Size(34, 42);
            this.buttonPlayPause.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)));
            this.buttonPlayPause.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonPlayPause.StateCommon.Border.Rounding = 8;
            this.buttonPlayPause.TabIndex = 8;
            this.buttonPlayPause.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonPlayPause.Values.Image")));
            this.buttonPlayPause.Values.Text = "";
            // 
            // borderEdgeSep2
            // 
            this.borderEdgeSep2.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeSep2.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderEdgeSep2.Location = new System.Drawing.Point(73, 0);
            this.borderEdgeSep2.Name = "borderEdgeSep2";
            this.borderEdgeSep2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeSep2.Size = new System.Drawing.Size(1, 42);
            this.borderEdgeSep2.StateCommon.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.borderEdgeSep2.Text = "kryptonBorderEdge1";
            // 
            // buttonStop
            // 
            this.buttonStop.AutoSize = true;
            this.buttonStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStop.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonStop.Location = new System.Drawing.Point(39, 0);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(34, 42);
            this.buttonStop.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)));
            this.buttonStop.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonStop.StateCommon.Border.Rounding = 8;
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Values.Image")));
            this.buttonStop.Values.Text = "";
            // 
            // borderEdgeSep1
            // 
            this.borderEdgeSep1.BorderStyle = ComponentFactory.Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.borderEdgeSep1.Dock = System.Windows.Forms.DockStyle.Left;
            this.borderEdgeSep1.Location = new System.Drawing.Point(38, 0);
            this.borderEdgeSep1.Name = "borderEdgeSep1";
            this.borderEdgeSep1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.borderEdgeSep1.Size = new System.Drawing.Size(1, 42);
            this.borderEdgeSep1.StateCommon.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.borderEdgeSep1.Text = "kryptonBorderEdge3";
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.AutoSize = true;
            this.buttonPrevious.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonPrevious.Dock = System.Windows.Forms.DockStyle.Left;
            this.buttonPrevious.Location = new System.Drawing.Point(0, 0);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(38, 42);
            this.buttonPrevious.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)(((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)));
            this.buttonPrevious.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.buttonPrevious.StateCommon.Border.Rounding = 8;
            this.buttonPrevious.TabIndex = 6;
            this.buttonPrevious.Values.Image = ((System.Drawing.Image)(resources.GetObject("buttonPrevious.Values.Image")));
            this.buttonPrevious.Values.Text = "";
            // 
            // PlayerControls
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelControls);
            this.Name = "PlayerControls";
            this.Size = new System.Drawing.Size(803, 53);
            this.tableLayoutPanelControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel4)).EndInit();
            this.kryptonPanel4.ResumeLayout(false);
            this.kryptonPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel3)).EndInit();
            this.kryptonPanel3.ResumeLayout(false);
            this.kryptonPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel2)).EndInit();
            this.kryptonPanel2.ResumeLayout(false);
            this.kryptonPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelControls;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel4;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelTotalTime;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel labelTimeProgress;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBar1;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonVolume;
        private ComponentFactory.Krypton.Toolkit.KryptonTrackBar kryptonTrackBarVolume;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel3;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel2;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeSep3;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonNext;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonPlayPause;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeSep2;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonStop;
        private ComponentFactory.Krypton.Toolkit.KryptonBorderEdge borderEdgeSep1;
        private ComponentFactory.Krypton.Toolkit.KryptonCheckButton buttonPrevious;
    }
}
