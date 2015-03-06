using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace MovieTube.Viewer
{
    public partial class MovieDownloadForm : Form
    {
        public MovieDownloadForm()
        {
            InitializeComponent();
        }

        public void Add(DownloadInfo di, int progress)
        {
            var widget = new DownloadWidget(di);
            this.flowLayoutPanel.Controls.Add(widget);
        }

        public void UpdateProgress(DownloadInfo di, DownloadProgressChangedEventArgs e)
        {
            foreach (DownloadWidget cnt in this.flowLayoutPanel.Controls)
            {
                if (cnt.Info == di)
                {
                    cnt.UpdateStatus(e);
                    return;
                }
            }
        }

        public void UpdateCompletion(DownloadInfo di, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            foreach (DownloadWidget cnt in this.flowLayoutPanel.Controls)
            {
                if (cnt.Info == di)
                {
                    cnt.UpdateCompletion(e.Error);
                    return;
                }
            }
        }


        //public void AddOrUpdate(DownloadInfo di, int progress)
        //{
        //    this.di = di;
        //    foreach (DownloadWidget cnt in this.flowLayoutPanel.Controls)
        //    {
        //        if (cnt.Info.Url == this.di.Url)
        //        {
        //            cnt.Progress = progress;
        //            return;
        //        }
        //    }
        //    var widget = new DownloadWidget(this.di) { Size = new Size(this.flowLayoutPanel.ClientSize.Width, 60), };
        //    this.flowLayoutPanel.Controls.Add(widget);
        //}

        private void MovieDownloadForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        public void ShowMe()
        {
            if (!this.Visible)
                this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            
        }
    }
}
