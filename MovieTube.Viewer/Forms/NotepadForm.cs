using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MovieTube.Viewer
{
    public partial class NotepadForm : Form
    {
        private Form parent;
        public NotepadForm()
        {
            InitializeComponent();
        }

        public NotepadForm(Form parent)
            :this()
        {
            this.parent = parent;
        }

        private void NotepadForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
            {
                this.Hide();
                this.parent.Show();
                e.SuppressKeyPress = true;
            }
        }

        private void NotepadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.parent.Close();
            }
        }
    }
}
