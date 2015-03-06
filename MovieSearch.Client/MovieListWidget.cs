using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BrightIdeasSoftware;
using System.Diagnostics;
using System.Collections;
using System.IO;
using MovieFinder.Client.Properties;

namespace MovieFinder.Client
{
    public partial class MovieListWidget : UserControl
    {
        private string selectedLangauge = String.Empty;
        private List<Movie> list = new List<Movie>();
        private int MovieDbVersion;
        public MovieListWidget()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            
            this.objectListView.HotItemStyle.Overlay = new BusinessCardOverlay();
            this.objectListView.HotItemStyle = this.objectListView.HotItemStyle;
            this.objectListView.Dock = DockStyle.Fill;
            AddLanguageButton("All", "").Checked = true;
            AddLanguageButton("Hindi", "hi");
            AddLanguageButton("Malayalam", "ml");
            AddLanguageButton("Tamil", "ta");
            AddLanguageButton("Telugu", "te");
        }

        public void LoadData(bool newVersion)
        {
            list.Clear();
            string json = null;
            if (File.Exists(Constants.MovieDatabaseFilePath))
                json = File.ReadAllText(Constants.MovieDatabaseFilePath);
            else
                json = System.Text.Encoding.UTF8.GetString(Properties.Resources.db);
            var dbMovie = Newtonsoft.Json.JsonConvert.DeserializeObject<MovieDB>(json);
            dbMovie.Movies.Sort(delegate(Movie p1, Movie p2)
                {
                    var c = p2.Year.CompareTo(p1.Year);
                    if (c == 0)
                        return p2.CreatedDate.CompareTo(p1.CreatedDate);
                    else
                        return c;
                    
                });
            list.AddRange(dbMovie.Movies);
            if (newVersion)
            {
               var movies = dbMovie.Movies.FindAll(x => x.EntryStatus == EntryChange.EntryAdded 
                   && x.Version == dbMovie.Version);
               if (movies.Count > 0)
               {
                   new DBUpdatedForm().ShowMe(movies);
               }
            }
            Program.MovieDBVersion = dbMovie.Version;
            this.objectListView.SetObjects(list);
            
        }

     

        private RadioButton AddLanguageButton(string text, string tag)
        {
            // 
            // radioButton4
            // 
            
            var button = new RadioButton();
            button.Appearance = System.Windows.Forms.Appearance.Button;
            button.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
           // button.Location = new System.Drawing.Point(304, -1);
            button.FlatStyle = FlatStyle.Flat;
            button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

            button.Margin = new System.Windows.Forms.Padding(0);
            button.Padding = new System.Windows.Forms.Padding(0);
            button.Size = new System.Drawing.Size(104, 30);
            button.TabStop = true;
            button.Text = text;
            button.Tag = tag;
            button.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            button.UseVisualStyleBackColor = true;
            if (panelButton.Controls.Count > 0)
            {
                var lButton = panelButton.Controls[panelButton.Controls.Count - 1];
                button.Location = new Point(lButton.Location.X + 102,-2);
            }
            else
            {
                button.Location = new Point(0, -2);
            }
            this.panelButton.Controls.Add(button);
            button.CheckedChanged += new EventHandler(button_CheckedChanged);
            return button;
            // 
        }

        void button_CheckedChanged(object sender, EventArgs e)
        {
            var button = ((RadioButton)sender);
            if (!button.Checked)
                return;
            var lang = button.Tag.ToString();
            if (!String.IsNullOrEmpty(lang))
            {
                var items = this.list.FindAll(x => x.LCode == lang);
                this.objectListView.SetObjects(items);
            }
            else
                this.objectListView.SetObjects(this.list);

        }

        void TimedFilter(string txt, int matchKind)
        {
            TextMatchFilter filter = null;
            var olv = this.objectListView;
            if (!String.IsNullOrEmpty(txt))
            {
                switch (matchKind)
                {
                    case 0:
                    default:
                        filter = TextMatchFilter.Contains(olv, txt);
                        break;
                    case 1:
                        filter = TextMatchFilter.Prefix(olv, txt);
                        break;
                    case 2:
                        filter = TextMatchFilter.Regex(olv, txt);
                        break;
                }
            }
            // Setup a default renderer to draw the filter matches
            if (filter == null)
                olv.DefaultRenderer = null;
            else
            {
                olv.DefaultRenderer = new HighlightTextRenderer(filter);

                // Uncomment this line to see how the GDI+ rendering looks
                //olv.DefaultRenderer = new HighlightTextRenderer { Filter = filter, UseGdiTextRendering = false };
            }

            // Some lists have renderers already installed
            var highlightingRenderer = olv.GetColumn(0).Renderer as HighlightTextRenderer;
            if (highlightingRenderer != null)
                highlightingRenderer.Filter = filter;

            
            olv.AdditionalFilter = filter;
        }

        private void buttonSearch_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            var drawBrush = new SolidBrush(this.buttonSearch.ForeColor);
            var sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            // this.buttonSearch.Text = string.Empty;
            e.Graphics.DrawString(this.buttonSearch.Text, this.buttonSearch.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackColor = System.Drawing.Color.DimGray;
            foreach (var c in panel1.Controls)
            {
                var b = c as Button;

                if (b != null & b != button)
                    b.BackColor = System.Drawing.Color.Gray;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TimedFilter(this.textBox1.Text.Trim(), 0);
        }

        private void objectListView_CellToolTipShowing(object sender, ToolTipShowingEventArgs e)
        {
            var col = e.Column ?? e.ListView.GetColumn(0);
            Movie movie = e.Model as Movie;
            if (movie != null)
                e.Text = movie.Name;
        }

        private void objectListView_HyperlinkClicked(object sender, HyperlinkClickedEventArgs e)
        {
           var movie = e.Model as Movie;
           if (movie != null)
           {
               if(e.Column == this.olvColumnLink1)
                e.Url = movie.Link1.Url;
               else if(e.Column == this.olvColumnLink2)
                   e.Url = movie.Link2.Url;

           }
        }

        public List<Movie> Movies
        {
            get { return this.list; }
        }

    }


}
