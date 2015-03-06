using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MovieTube.Viewer
{
    public partial class TraceForm : Form
    {
        public TraceForm()
        {
            InitializeComponent();
           
            Tracer.Trace += new EventHandler<TraceEventArgs>(Tracer_Trace);
        }

        void Tracer_Trace(object sender, TraceEventArgs e)
        {
            this.InvokeEx(() =>
            {
                this.richTextBox1.AppendText(e.Text + Environment.NewLine);
            });
        }

        private void TraceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tracer.Trace -= new EventHandler<TraceEventArgs>(Tracer_Trace);
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }
    }
}
