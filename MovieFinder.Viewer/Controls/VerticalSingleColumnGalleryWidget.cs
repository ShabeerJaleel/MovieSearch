using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Client.Scraper;

namespace BlueTube.Viewer
{
    public partial class VerticalSingleColumnGalleryWidget : GalleryWidgetBase
    {
        #region Constructor

        public VerticalSingleColumnGalleryWidget()
        {
            InitializeComponent();
           
        }

        #endregion

        protected override void OnAddItems(ScrapedPage page)
        {

            for (var i = 0; i < Constants.GetMaxDisplayCount(page.Videos.Count); i++)
            {
                var video = page.Videos[i];
                AddItem(video);
                if (Constants.ShowAds && i % 4 == 0)
                {
                    var ad = CreateAdWidget(Constants.VerticalAdId);
                    this.tableLayoutPanel.Controls.Add(ad);
                }
            }
        }

        private WebViewWidget AddItem(ScrapedVideo video)
        {
            var widget = new WebViewWidget(video, Properties.Resources.TestHtml.Replace("{0}", video.Url).Replace("{1}", video.ImageUrl).
                     Replace("{2}", video.Title).Replace("{3}", video.Duration.ToString()));
            widget.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            widget.Width = this.Width;
            this.tableLayoutPanel.Controls.Add(widget);
            Application.DoEvents();
            widget.ViewSelected += delegate(object sender, GalleryItemSelectedEventArgs e)
            {
                OnItemSelected(sender, e);
            };
            return widget;
        }

        protected override void OnAddItem(ScrapedVideo video, bool select)
        {
            var found = false;
            LogManager.Log("Adding items to Veritcal gallery");
            foreach (Control c in this.tableLayoutPanel.Controls)
            {
                var widget = c as WebViewWidget;
                if (widget != null)
                {
                    if (widget.Video.Url == video.Url)
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
                var widget = AddItem(video);
                this.tableLayoutPanel.Controls.SetChildIndex(widget, 0);
                if (select)
                    widget.SelectView();
            }
        }

        protected override Control WidgetContainer
        {
            get
            {
                return this.tableLayoutPanel;
            }
        }
       

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            foreach (Control c in this.tableLayoutPanel.Controls)
                c.Width = this.Width;
        }
    }
}
