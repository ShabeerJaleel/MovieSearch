using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Web;
using System.IO;
using System.Threading;

namespace MovieFinder.Test.Forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           // this.webBrowser1.DocumentText = "<embed type=\"application/x-shockwave-flash\" src=\"http://neovid.me/1.swf\" flashvars=\"file=317707bbc69fd6f171970a2e4dee290a.mp4&amp;plugins=hd-2&amp;bufferlength=5\">";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.textBox1.Text = HttpUtility.UrlDecode(this.textBox1.Text.Trim());
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            this.axWindowsMediaPlayer1.URL = this.textBoxUrl.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //FileStream source = new FileStream("unpacker.html", FileMode.Open, FileAccess.Read);
            //webBrowser1.DocumentStream = source;
            //Application.DoEvents();
            //var input = webBrowser1.Document.GetElementById("result").GetAttribute("value");
            runBrowserThread(new FileStream("unpacker.html", FileMode.Open, FileAccess.Read));

        }


        private void runBrowserThread(FileStream stream)
        {
            var th = new Thread(() =>
            {
                var br = new WebBrowser();
                br.DocumentCompleted += browser_DocumentCompleted;
                br.DocumentStream = stream;
                Application.Run();
                
            });
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = sender as WebBrowser;
            if (br.Url == e.Url)
            {
                var input = br.Document.GetElementById("result").GetAttribute("value");
                Application.ExitThread();   // Stops the thread
            }
        }
    }
}
