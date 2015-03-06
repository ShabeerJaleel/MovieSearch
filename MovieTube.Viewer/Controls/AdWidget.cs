using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MovieTube.Viewer
{
    public partial class AdWidget : UserControl
    {
        public AdWidget()
            :this("")
        {
            
        }

        public AdWidget(string id)
        {
            InitializeComponent();
            Init(id);
        }

        private void Init(string id)
        {
            this.webBrowser.Url = new Uri(String.Format("{0}?id={1}&uid={2}", UrlConstants.AdUrl, id, 
                Constants.UniqueId.ToString()));
            this.webBrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.webBrowser_Navigating);
        }

        private void webBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //cancel the current event
            e.Cancel = true;
            
            //this opens the URL in the user's default browser
            Process.Start(e.Url.ToString());
        }

        private void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //this.Height = this.webBrowser.Document.Body.ScrollRectangle.Size.Height;
          //  this.webBrowser.Height = this.webBrowser.Document.Body.ScrollRectangle.Size.Height;
        }

    }
}
