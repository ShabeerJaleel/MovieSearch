using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MovieTube.Viewer.Data;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer
{
    public partial class SearchPanelWidget : UserControl
    {
        private const string deafultSearchText = "Search...\t";
        public event EventHandler<SearchEventArgs> Search;
        public SearchPanelWidget()
        {
            InitializeComponent();
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                Init();
                this.searchTextbox.Text = deafultSearchText;
            }
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Init()
        {
            this.kryptonComboBoxLanguage.DataSource = Language.Languages;
            this.kryptonComboBoxLanguage.SelectedIndex = 0;
            this.kryptonComboBoxLanguage.SelectedItem = Language.Languages.Find(x => x.Id == 
                ClientDataService.Single.Settings.DefaultLanguage);
            this.kryptonComboBoxYear.SelectedIndex = 0;
        }

        private void kryptonTextBox1_Enter(object sender, EventArgs e)
        {
            if (deafultSearchText == this.searchTextbox.Text)
                this.searchTextbox.Text = String.Empty;
            this.searchTextbox.StateCommon.Content.Color1 = SystemColors.WindowText;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.AddRange(ClientDataService.Single.GetAllSearchWords().ToArray());
            this.searchTextbox.AutoCompleteCustomSource = col;
        }

        private void kryptonTextBox1_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.searchTextbox.Text.Trim()))
            {
                this.searchTextbox.Text = deafultSearchText;
                this.searchTextbox.StateCommon.Content.Color1 = SystemColors.InactiveCaption;
            }
        }

        private void kryptonTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var text = this.searchTextbox.Text.Trim();
                if(!String.IsNullOrEmpty(text))
                {
                    ClientDataService.Single.AddSearchWord(text);
                    this.searchTextbox.AutoCompleteCustomSource.Add(text);
                }
                TriggerSearchEvent();
            }
            
        }

        private void TriggerSearchEvent()
        {

            var s = new SearchEventArgs(this.searchTextbox.Text != deafultSearchText ? this.searchTextbox.Text.Trim() : null);
            if (!String.IsNullOrEmpty(s.Params.Query))
                this.kryptonComboBoxYear.SelectedIndex = 0;

            if (this.kryptonComboBoxYear.SelectedItem.ToString() == "(New)")
                s.Params.Year = -1;
            else if (this.kryptonComboBoxYear.SelectedItem.ToString() != "(Year)")
                s.Params.Year = Convert.ToInt32(this.kryptonComboBoxYear.SelectedItem);
            
            s.Params.Language = ((Language)this.kryptonComboBoxLanguage.SelectedItem).Id;
            //s.Params.Period = this.anytimeFilter.Checked ? VideoPeriod.Anytime : (this.todayFilter.Checked ? VideoPeriod.Today :
            //    (this.weekFilter.Checked ? VideoPeriod.ThisWeek : VideoPeriod.ThisMonth));
            //s.Params.Duration = this.durationAllFilter.Checked ? VideoDuration.All : (this.shortDurationFilter.Checked ? VideoDuration.Short :
            //    (this.durationMediumFilter.Checked ? VideoDuration.Medium : VideoDuration.Long));
            Search(this, s);

            var settings = ClientDataService.Single.Settings;
            settings.DefaultLanguage = s.Params.Language;
            ClientDataService.Single.SaveGlobalSettings(settings);
        }

        public string GetLanguage()
        {
            return ((Language)this.kryptonComboBoxLanguage.SelectedItem).Id;
        }

       
        private void searchButton_Click(object sender, EventArgs e)
        {
            TriggerSearchEvent();
        }

        private void buttonSpecClear_Click(object sender, EventArgs e)
        {
            this.searchTextbox.Text = String.Empty;
            this.searchTextbox.Focus();
        }
    }

    public class SearchEventArgs : EventArgs
    {
        public SearchEventArgs(string query)
	    {
            Params = new SearchParameters();
            Params.Query = query;
	    }
        public SearchParameters Params { get; set; }
    }

 
}
