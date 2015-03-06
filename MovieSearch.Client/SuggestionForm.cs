using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Collections.Specialized;
using System.Reflection;

namespace MovieFinder.Client
{
    public partial class SuggestionForm : Form
    {
        public SuggestionForm()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.textBoxSuggestion.Text))
            {
                MessageBox.Show("Please enter your suggestion/bug", "Movie Finder");
                return;
            }
            try
            {
                PostResource(this.textBoxSuggestion.Text.Trim());
                MessageBox.Show("Thank you for your suggestion!!", "Movie Finder");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to send. Error: " + ex.Message, "Movie Finder");
            }

        }

        private void PostResource(string text)
        {

            using (var client = new WebClient())
            {
                var data = new NameValueCollection();
                data.Add("text", text);
                data.Add("version", Assembly.GetExecutingAssembly().GetName().Version.ToString());
                client.UploadValues(UrlConstants.SuggestionUrl, "POST", data); 
            }

        }
    }
}
