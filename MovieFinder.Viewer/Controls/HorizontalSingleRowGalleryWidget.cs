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
    public partial class HorizontalSingleRowGalleryWidget : GalleryWidgetBase
    {
        #region Constructor

        public HorizontalSingleRowGalleryWidget()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods


        protected override void OnAddItems(ScrapedPage page)
        {
            if (this.kryptonPanel1.Controls.Contains(this.kryptonLabelPrivacy))
                this.kryptonPanel1.Controls.Remove(this.kryptonLabelPrivacy);
            
            for (var i = 0; i < Constants.GetMaxDisplayCount(page.Videos.Count); i++)
            {
                var video = page.Videos[i];
                var widget = new WebViewWidget(video,
                    Properties.Resources.TestHtml.Replace("{0}", video.Url).Replace("{1}", video.ImageUrl).
                       Replace("{2}", video.Title).Replace("{3}", video.Duration.ToString()));
                this.flowLayoutPanel.Controls.Add(widget);
                widget.ViewSelected += delegate(object sender, GalleryItemSelectedEventArgs e)
                {
                    OnItemSelected(sender, e);
                };

                if (Constants.ShowAds && i % 4 == 0)
                {
                    var ad = CreateAdWidget(Constants.HorizontalAdId);
                    this.flowLayoutPanel.Controls.Add(ad);
                }
            }
        }

        protected override Control WidgetContainer
        {
            get
            {
                return this.flowLayoutPanel;
            }
        }
       
        #endregion
    }
}
