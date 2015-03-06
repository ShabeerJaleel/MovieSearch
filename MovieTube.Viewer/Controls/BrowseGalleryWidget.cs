using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Linq;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer.Controls
{
    public partial class BrowseGalleryWidget : GalleryWidgetBase
    {
        
        public BrowseGalleryWidget()
        {
            InitializeComponent();
        }

        #region Methods

        protected override void OnAddItems(MoviePage page)
        {

            this.flowLayoutPanel.SuspendLayout();

            var rand = new Random(DateTime.Now.Minute);
            var next = 4;
            for (var i = 0; i < Constants.GetMaxDisplayCount(page.Videos.Count); i++)
            {
                var movie = page.Videos[i];
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

                if (Constants.ShowAds )
                {
                    if (next == 0)
                    {
                        var ad = CreateAdWidget(Constants.HorizontalAdId);
                        this.flowLayoutPanel.Controls.Add(ad);
                        next = rand.Next(4, 8);
                    }
                    else 
                        next--;
                }
               
            }
            
            flowLayoutPanel_Resize(this, null);
           // AddLinks();
            this.flowLayoutPanel.ResumeLayout();
            base.OnAddItems(page);
        }
        protected override Control WidgetContainer
        {
            get
            {
                return this.flowLayoutPanel;   
            }
        }
       

        #endregion

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            this.flowLayoutPanel.SuspendLayout();
            if (this.flowLayoutPanel.Controls.Count > 0)
            {
                var width = this.flowLayoutPanel.Controls[0].Width;
                this.flowLayoutPanel.ColumnCount = this.flowLayoutPanel.ClientRectangle.Width / this.flowLayoutPanel.Controls[0].Width;
                if (this.flowLayoutPanel.ColumnCount > 0)
                {
                    var freeWidth = (this.flowLayoutPanel.ClientRectangle.Width -
                        (this.flowLayoutPanel.Controls[0].Width * this.flowLayoutPanel.ColumnCount)) /
                        this.flowLayoutPanel.ColumnCount;
                    if (freeWidth > 0)
                    {
                        foreach (Control c in this.flowLayoutPanel.Controls)
                            c.Margin = new Padding(freeWidth / 2, 10, freeWidth / 2, 0);

                    }
                }
            }

            ResizePageLinks();
            this.flowLayoutPanel.ResumeLayout();
        }

        private void AddLinks()
        {
            //this.flowLayoutPanelPaging.Controls.Clear();
            //if (base.currentPage == null || base.currentPage.Links.Count == 0)
            //    return;

            //this.flowLayoutPanelPaging.Padding = new Padding(0);
            
            //var totalWidth = 0;
            //foreach (var p in base.currentPage.Links)
            //{
            //    if (totalWidth >= this.flowLayoutPanelPaging.ClientSize.Width)
            //        break;
            //    var b = CreatePageLink(p);
            //    totalWidth += b.Width;
            //    this.flowLayoutPanelPaging.Controls.Add(b);
            //}
            //ResizePageLinks();
        }

        private void ResizePageLinks()
        {
            //var totalWidth = 0;
            //foreach (Control b in this.flowLayoutPanelPaging.Controls)
            //    totalWidth += b.Width;
            //if (totalWidth < this.flowLayoutPanelPaging.ClientSize.Width)
            //{
            //    var freeWidth = this.flowLayoutPanelPaging.ClientSize.Width - totalWidth;
            //    this.flowLayoutPanelPaging.Padding = new Padding(freeWidth / 2, 0, freeWidth / 2, 0);
            //}
            //else
            //    this.flowLayoutPanelPaging.Padding = new Padding(0);
        }

        private void UpdatePagingButtons(KryptonCheckButton button)
        {
            //foreach (KryptonCheckButton b in this.flowLayoutPanelPaging.Controls)
            //    b.Checked = b == button ? true : false;
        }

        private KryptonCheckButton CreatePageLink( PagingLink link)
        {
            var button = new KryptonCheckButton();
            button.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.LowProfile;
            button.Cursor = System.Windows.Forms.Cursors.Hand;
            button.Location = new System.Drawing.Point(0, 0);
            button.Margin = new System.Windows.Forms.Padding(0);
            button.Size = link.Text.Length == 1 ? new System.Drawing.Size(29, 25) : new System.Drawing.Size(10 + ( link.Text.Length * 10), 25);
            button.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom)
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left)
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            button.StateCommon.Border.Width = 1;
            button.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.Values.Text = link.Text;
            button.Tag = link;
            button.Checked = link.IsSelected;
            button.CheckedChanged += delegate(object sender, EventArgs e)
            {
                var b = (KryptonCheckButton)sender;
                if (b.Checked)
                {
                    OnPageSelected(this, new GalleryPageSelectedEventArgs(link));
                    UpdatePagingButtons(b);
                }
            };
            return button;
        }

        public bool ShowHeader
        {
            get
            {
                return this.kryptonLabel1.Visible;
            }
            set
            {
                this.kryptonLabel1.Visible = value;
                this.splitContainer.SplitterDistance = 0;
            }
        }
    }
}
