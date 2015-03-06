using System;
using System.Collections.Generic;
using System.Text;
using MovieTube.Client.Scraper;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace MovieTube.Viewer
{
    public class DownloaderService : IScraperServiceCallback
    {
        private WebClient client = new WebClient();
        private IDownloadProgress dpClient;
        private DownloadInfo di;
        private bool stop;
        public void Download(DownloadInfo di, IDownloadProgress dpClient)
        {
            stop = false;
            this.dpClient = dpClient;
            this.di = di;
             if (di.Link.ScrapState == LinkScrapState.FullyLoaded)
                OnScrapVideoDetailsCompleted(di.Link);
            else
                new ScraperService().ScrapVideosDetailsAsync(this, di.Link);
        }

        public void Stop()
        {
            lock (this.client)
            {
                try
                {
                    this.stop = true;
                    this.client.CancelAsync();
                }
                catch { }
            }
        }

        public void OnScrapVideoDetailsCompleted(MovieLink link)
        {
            di.FilePath = CreateFilename(link);
            DoDownload(link, di.FilePath);
        }
        
        private string CreateFilename(MovieLink link)
        {
            string fileName = link.Parent.Name + "_" + Path.GetFileName(new Uri(link.PlayUrl).AbsolutePath);
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(c, '_');

            var f = fileName;
            for(var i = 0 ; i < 1000; i++)
            {

                f = Path.Combine(AppSettings.MovieDownloadFolder, f);
                if (File.Exists(f))
                {
                    var f1 = Path.GetFileNameWithoutExtension(fileName);
                    var f2 = Path.GetExtension(fileName);
                    f = String.Format("{0}({1}){2}", f1, i, f2);
                    continue;
                }

            }
            return f;
        }

        public void OnScrapError(MovieLink link, string message)
        {
            
        }

        private void DoDownload(MovieLink movieLink, string filePath)
        {
            lock (client)
            {
                if (this.stop)
                    return;
                client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(client_DownloadFileCompleted);
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileAsync(new Uri(movieLink.PlayUrl), filePath);
            }
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.dpClient.OnProgress(this.di, e);
        }

        void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.dpClient.OnCompletion(this.di, e);
        }
    }
}
