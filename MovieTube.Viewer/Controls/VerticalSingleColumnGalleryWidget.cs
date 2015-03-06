using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MovieTube.Client.Scraper;

namespace MovieTube.Viewer
{
    public partial class VerticalSingleColumnGalleryWidget : GalleryWidgetBase
    {
        #region Constructor

        public VerticalSingleColumnGalleryWidget()
        {
            InitializeComponent();
           
        }

        #endregion

        protected override void OnAddItems(MoviePage page)
        {

            for (var i = 0; i < Constants.GetMaxDisplayCount(page.Videos.Count); i++)
            {
                var video = page.Videos[i];
                foreach(var l in video.Links)
                    AddItem(l);
                if (Constants.ShowAds && i % 4 == 0)
                {
                    var ad = CreateAdWidget(Constants.VerticalAdId);
                    this.tableLayoutPanel.Controls.Add(ad);
                }
            }
        }

        private LinkViewWidget AddItem(MovieLink video)
        {
            //create links
            var widget = new LinkViewWidget(video);
            widget.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            widget.Width = this.Width;
            this.tableLayoutPanel.Controls.Add(widget);
            Application.DoEvents();
            widget.ViewSelected += delegate(object sender, GalleryItemSelectedEventArgs e)
            {
                OnItemSelected(sender, e);
            };
            widget.DownloadClicked += delegate(object sender, GalleryItemSelectedEventArgs e)
            {
                OnDownloadClicked(sender, e);
            };
            return widget;
        }

        protected override void OnAddItem(Movie video, bool select)
        {
            var found = false;
            LogManager.Log("Adding items to Veritcal gallery");
            foreach (Control c in this.tableLayoutPanel.Controls)
            {
                var widget = c as LinkViewWidget;
                if (widget != null)
                {
                    var link = widget.Video as MovieLink;
                    if(link.DowloadUrl == video.Url)
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
                foreach (var link in video.Links)
                {
                    var widget = AddItem(link);
                    //this.tableLayoutPanel.Controls.SetChildIndex(widget, 0);
                }

                if (select)
                {
                    var widget = this.tableLayoutPanel.Controls[0] as LinkViewWidget;
                    widget.SelectView();
                }
            }
        }

        protected override Control WidgetContainer
        {
            get
            {
                return this.tableLayoutPanel;
            }
        }

        public LinkViewWidget GetItem(MovieLink link)
        {
            foreach (LinkViewWidget c in this.tableLayoutPanel.Controls)
            {
                if (c.Video == link)
                    return c;
            }
            return null;
        }
       

        private void flowLayoutPanel_Resize(object sender, EventArgs e)
        {
            foreach (Control c in this.tableLayoutPanel.Controls)
                c.Width = this.Width;
        }
    }
}
