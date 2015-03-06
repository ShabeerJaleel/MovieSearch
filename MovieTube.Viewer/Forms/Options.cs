using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MovieTube.Viewer.Data;
using MovieTube.Client.Scraper;
using System.IO;

namespace MovieTube.Viewer
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
            var s =ClientDataService.Single.Settings ;
            this.comboBox1.DataSource = Language.Languages;
            this.comboBox1.SelectedItem = Language.Languages.Find(x => x.Id == s.DefaultLanguage);
            this.checkBox1.Checked = s.PlayFirstLink;
            this.textBoxDFolder.Text = AppSettings.MovieDownloadFolder;
           
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            var s = ClientDataService.Single.Settings;
            s.DefaultLanguage = ((Language)this.comboBox1.SelectedItem).Id;
            s.PlayFirstLink = this.checkBox1.Checked;
            ClientDataService.Single.SaveGlobalSettings(s);
            AppSettings.MovieDownloadFolder = this.textBoxDFolder.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {

            using (var di = new FolderBrowserDialog() { Description = "Select download folder" })
            {
                if (di.ShowDialog() == DialogResult.OK)
                    this.textBoxDFolder.Text = di.SelectedPath;
            }

        }
    }
}
