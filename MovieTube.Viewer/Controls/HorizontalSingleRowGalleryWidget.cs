using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MovieTube.Client.Scraper;
using System.Linq;

namespace MovieTube.Viewer
{
    public partial class HorizontalSingleRowGalleryWidget : GalleryWidgetBase
    {
        private int startIndex, endIndex;

        #region Constructor

        public HorizontalSingleRowGalleryWidget()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods


        protected override void OnAddItems(MoviePage page)
        {
            this.startIndex = 0;
            this.endIndex = Math.Min(page.Videos.Count - 1,GetControlEndIndex()) ;
            AddSinglePage();
        }

        protected override void OnAddItem(Movie movie, bool select)
        {
            var found = false;
            LogManager.Log("Adding items to Veritcal gallery");
            foreach (Control c in this.flowLayoutPanel.Controls)
            {
                var widget = c as WebViewWidget;
                if (widget != null)
                {
                    var link = widget.Video;
                    if (link.ID == movie.ID)
                    {
                        widget.SelectView();
                        found = true;
                    }
                    else
                        widget.DeselectView();
                }
            }
            if (!found)
            {
                var m = this.currentPage.Videos.FirstOrDefault(x => x.ID == movie.ID);
                if (m != null) 
                    this.currentPage.Videos.Remove(m);
                this.currentPage.Videos.Insert(this.startIndex, movie);
                AddSinglePage();
                if (select)
                    ((WebViewWidget)this.flowLayoutPanel.Controls[0]).SelectView();
             }
        }

        private WebViewWidget AddItem(Movie movie)
        {
            var widget = new WebViewWidget(movie,
                    Properties.Resources.TestHtml.Replace("{0}", movie.Url)
                    .Replace("{1}", String.IsNullOrEmpty(movie.ImageUrl) ? "no_image.gif" : movie.ImageUrl)
                    .Replace("{2}", movie.Name)
                    .Replace("{3}", movie.ReleaseDate.Year.ToString())
                    .Replace("{4}", movie.Description)
                    .Replace("{5}", movie.LanguageText));
            this.flowLayoutPanel.Controls.Add(widget);
            widget.ViewSelected += delegate(object sender, GalleryItemSelectedEventArgs e)
            {
                OnItemSelected(sender, e);
            };
            widget.Favourited += delegate(object sender, GalleryItemFavouriteEventArgs e)
            {
                OnItemFavourited(sender, e);
            };

            return widget;
        }

        private void AddSinglePage()
        {
            this.flowLayoutPanel.Controls.Clear();
            for (var i = this.startIndex; i <= this.endIndex; i++)
            {
                AddItem(this.currentPage.Videos[i]);
                if (Constants.ShowAds && i % 4 == 0)
                {
                    var ad = CreateAdWidget(Constants.HorizontalAdId);
                    this.flowLayoutPanel.Controls.Add(ad);
                }
            }
        }

        private int GetControlEndIndex()
        {
            if (this.Size.Width < 351)
                return 0;
            var i = this.Size.Width / 350 ;
            if (this.Size.Width % 350 > 100)
                return i;
            return i - 1;

        }

        protected override Control WidgetContainer
        {
            get
            {
                return this.flowLayoutPanel;
            }
        }

        public void ShowNext()
        {
            this.startIndex = this.endIndex;
            this.endIndex = Math.Min(this.startIndex  +GetControlEndIndex(), this.currentPage.Videos.Count - 1);
            AddSinglePage();
        }

        public void ShowPrevious()
        {
            this.startIndex = Math.Max(this.startIndex - GetControlEndIndex(), 0);
            this.endIndex = Math.Min(this.startIndex + GetControlEndIndex(), this.currentPage.Videos.Count - 1);
            AddSinglePage();
        }

        public bool IsEnd
        {
            get { return this.endIndex == this.currentPage.Videos.Count - 1; }
        }

        public bool IsBeginning { get { return this.startIndex == 0; } }
       
        #endregion

        private void HorizontalSingleRowGalleryWidget_Resize(object sender, EventArgs e)
        {
            if (this.currentPage != null)
            {
                this.endIndex =  Math.Min(this.startIndex + GetControlEndIndex(), this.currentPage.Videos.Count - 1);
                AddSinglePage();
            }
        }
    }
}
