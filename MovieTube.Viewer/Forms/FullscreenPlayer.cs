using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MovieTube.Viewer
{
    public partial class FullscreenPlayer : Form
    {
        private Control player;
        private Control parent;
        public FullscreenPlayer()
        {
            InitializeComponent();
            this.TopLevel = true;
        }

        public FullscreenPlayer(Control player)
            : this()
        {
            this.parent = player.Parent;
            this.player = player;
            this.Controls.Add(player);
            var tp = new TransparentPanel();
            tp.Dock = DockStyle.Fill;
            tp.DoubleClick += new EventHandler(transparentPanel_DoubleClick);
            this.Controls.Add(tp);
            tp.BringToFront();
            this.kryptonLabelBuffering.BringToFront();
        }

        private void transparentPanel_DoubleClick(object sender, EventArgs e)
        {
            this.Controls.Remove((Control)sender);
            this.parent.Controls.Add(this.player);
            this.Close();
        }

      
    }

    

}
