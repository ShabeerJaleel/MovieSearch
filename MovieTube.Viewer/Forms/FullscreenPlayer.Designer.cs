namespace MovieTube.Viewer
{
    partial class FullscreenPlayer
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
            this.kryptonLabelBuffering = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.playerControls1 = new MovieTube.Viewer.Controls.PlayerControls();
            this.SuspendLayout();
            // 
            // kryptonLabelBuffering
            // 
            this.kryptonLabelBuffering.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonLabelBuffering.Location = new System.Drawing.Point(917, 237);
            this.kryptonLabelBuffering.Name = "kryptonLabelBuffering";
            this.kryptonLabelBuffering.Size = new System.Drawing.Size(103, 24);
            this.kryptonLabelBuffering.StateNormal.ShortText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kryptonLabelBuffering.TabIndex = 3;
            this.kryptonLabelBuffering.Values.Text = "Buffering.....";
            this.kryptonLabelBuffering.Visible = false;
            // 
            // playerControls1
            // 
            this.playerControls1.Location = new System.Drawing.Point(63, 204);
            this.playerControls1.Name = "playerControls1";
            this.playerControls1.Size = new System.Drawing.Size(803, 53);
            this.playerControls1.TabIndex = 4;
            this.playerControls1.Visible = false;
            // 
            // FullscreenPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 262);
            this.Controls.Add(this.playerControls1);
            this.Controls.Add(this.kryptonLabelBuffering);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FullscreenPlayer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FullscreenPlayer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonLabel kryptonLabelBuffering;
        private Controls.PlayerControls playerControls1;
    }
}