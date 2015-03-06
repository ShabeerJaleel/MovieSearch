using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using MovieTube.Viewer.Data;

namespace MovieTube.Viewer
{
    public partial class DownloadForm : Form
    {
       
        public DownloadForm()
        {
            InitializeComponent();
        }

        public DownloadForm(string msg)
            :this()
        {
            this.labelMessage.Text = msg;
        }

        public void Download()
        {
            var t = new Thread(DownloadDB);
            t.Start();
            ShowDialog();
        }

        public void DownloadDB()
        {

            try
            {
                new UpdaterService().DownloadMovieDatabase();
                var s = ClientDataService.Single;
            }
            catch (Exception ex)
            {
            }
            finally { this.InvokeEx(() => { this.Dispose(); }); }
        }
    }
}
