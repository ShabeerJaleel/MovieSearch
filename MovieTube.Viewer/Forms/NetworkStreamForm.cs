using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MovieTube.Viewer
{
    public partial class NetworkStreamForm : Form
    {
        public NetworkStreamForm()
        {
            InitializeComponent();
        }

        public string Url { get { return this.textBoxUrl.Text; } }
    }
}
