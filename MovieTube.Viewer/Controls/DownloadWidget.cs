using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace MovieTube.Viewer
{
    public partial class DownloadWidget : UserControl
    {
        private DownloadInfo di;

        public DownloadWidget()
        {
            InitializeComponent();
        }

        public DownloadWidget(DownloadInfo di)
            :this()
        {
            this.di = di;
            this.labelName.Text = String.Format("{0} ({1}) - {2}", 
                di.Link.Parent.Name, 
                di.Link.Parent.LanguageText,
                di.Link.Provider.ID);
        }


        public void UpdateStatus(DownloadProgressChangedEventArgs e)
        {
                
                this.InvokeEx( () => {
                        if (!this.buttonStop.Enabled)
                            return;
                    
                        this.progressBar.Value = e.ProgressPercentage;
                        this.labelProgress.Text = e.ProgressPercentage.ToString() + "%";
                        this.labelSize.Text = String.Format("{0} MB / {1} MB", e.BytesReceived / (1024 * 1024), e.TotalBytesToReceive / (1024 * 1024));
                }
                );
            
        }

        public void UpdateCompletion(Exception error)
        {
             this.InvokeEx( () => {
                 if (error == null)
                 {
                     this.labelProgress.Text = "Completed";
                 }
                 else
                 {
                     this.labelProgress.Text = "Errror";
                 }
                 this.progressBar.Value = 100;
             });
        }

        public DownloadInfo Info
        {
            get
            {
                return this.di;
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!File.Exists(di.FilePath))
                return;

            string argument = "/select, \"" + di.FilePath + "\"";
            Process.Start("explorer.exe", argument);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (String.IsNullOrEmpty(di.Link.PlayUrl))
                return;

            Process.Start(di.Link.PlayUrl);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.buttonStop.Enabled = false;
            this.progressBar.Value = 100;
            this.di.Service.Stop();
            this.labelProgress.Text = "Cancelled";
            
        }
        
    }
}
