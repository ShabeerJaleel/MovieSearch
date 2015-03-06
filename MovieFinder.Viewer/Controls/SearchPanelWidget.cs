using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Client.Scraper;
using BlueTube.Viewer.Data;

namespace BlueTube.Viewer
{
    public partial class SearchPanelWidget : UserControl
    {
        private const string deafultSearchText = "Search...\t";
        public event EventHandler<SearchEventArgs> Search;
        public SearchPanelWidget()
        {
            InitializeComponent();
            this.searchTextbox.Text = deafultSearchText;
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonTextBox1_Enter(object sender, EventArgs e)
        {
            if (deafultSearchText == this.searchTextbox.Text)
                this.searchTextbox.Text = String.Empty;
            this.searchTextbox.StateCommon.Content.Color1 = SystemColors.WindowText;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            col.AddRange(DataService.Create().GetAllSearchWords().ToArray());
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
                    DataService.Create().AddSearchWord(text);
                    this.searchTextbox.AutoCompleteCustomSource.Add(text);
                }
                TriggerSearchEvent();
            }
            
        }

        private void TriggerSearchEvent()
        {
            if (this.searchTextbox.Text != deafultSearchText)
            {
                var s = new SearchEventArgs(this.searchTextbox.Text.Trim());
                s.Params.SortBy = this.ratingFilter.Checked ? VideoSortby.Rating : (this.uploadDateFilter.Checked ?
                    VideoSortby.Date : VideoSortby.Relevance );
                s.Params.Period = this.anytimeFilter.Checked ? VideoPeriod.Anytime : (this.todayFilter.Checked ? VideoPeriod.Today :
                    (this.weekFilter.Checked ? VideoPeriod.ThisWeek : VideoPeriod.ThisMonth));
                s.Params.Duration = this.durationAllFilter.Checked ? VideoDuration.All : (this.shortDurationFilter.Checked ? VideoDuration.Short :
                    (this.durationMediumFilter.Checked ? VideoDuration.Medium : VideoDuration.Long));

                Search(this, s);
            }
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
