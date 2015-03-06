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
    public partial class GalleryWidgetBase : UserControl
    {
        public event EventHandler<GalleryItemSelectedEventArgs> ItemSelected;
        public event EventHandler<GalleryPageSelectedEventArgs> PageSelected;
        protected ScrapedPage currentPage;


        public GalleryWidgetBase()
        {
            InitializeComponent();
        }

        protected virtual void OnItemSelected(object sender, GalleryItemSelectedEventArgs e)
        {
            foreach (Control c in WidgetContainer.Controls)
            {
                var v = c as WebViewWidget;
                if (v != null)
                {
                    if (v == sender)
                        v.SelectView();
                    else
                        v.DeselectView();
                }
            }
            if (ItemSelected != null)
                ItemSelected(sender, e);
        }

        protected virtual void OnPageSelected(object sender, GalleryPageSelectedEventArgs e)
        {
            if (PageSelected != null)
                PageSelected(sender, e);
        }

        public void AddItems(ScrapedPage page)
        {
            this.currentPage = page;
            OnAddItems(page);
        }

        public void AddItem(ScrapedVideo video, bool select)
        {
            OnAddItem(video, select);
        }

        protected virtual void OnAddItems(ScrapedPage page)
        {

        }

        protected virtual void OnAddItem(ScrapedVideo video, bool select)
        {

        }

        public void RefreshView()
        {
            foreach (Control c in WidgetContainer.Controls)
            {
                 var v = c as WebViewWidget;
                 if (v != null)
                     v.RefreshView();
            }
        }
        
        public void ClearItems()
        {
            this.WidgetContainer.Controls.Clear();
        }

       

        private int GetWidgetCount()
        {
            var i = 0;
            foreach (var c in WidgetContainer.Controls)
            {
                if (c is WebViewWidget)
                    i++;
            }
            return i;
        }

        protected AdWidget CreateAdWidget(string id)
        {
            var adWidget = new AdWidget(id);
            adWidget.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            return adWidget;
        }


        private int GetWidgetLastIndex()
        {
            for( var i = WidgetContainer.Controls.Count - 1; i >=  0; i--)
            {
                if (WidgetContainer.Controls[i] is WebViewWidget)
                    return i;
                    
            }
            return -1;
        }

        private int GetNextWidget(int index)
        {
            for (var i = index + 1; i < this.WidgetContainer.Controls.Count; i++)
            {
                if (WidgetContainer.Controls[i] is WebViewWidget)
                    return i;

            }
            return -1;
        }

        private int GetPreviousWidget(int index)
        {
            for (var i = index - 1; i >= 0; i--)
            {
                if (WidgetContainer.Controls[i] is WebViewWidget)
                    return i;

            }
            return -1;
        }

        public ScrapedVideo GetPreviousVideo(ScrapedVideo current)
        {
            int index = -1;
            for (var i = 0; i < WidgetContainer.Controls.Count; i++)
            {
                var v = WidgetContainer.Controls[i] as WebViewWidget ;
                if (v != null && v.Video == current)
                {
                    index = i;
                    break;
                }
            }

            if (GetWidgetCount() > 0)
            {
                if (index == -1 || index == 0)
                    return ((WebViewWidget)WidgetContainer.Controls[GetWidgetLastIndex()]).Video;
                else
                    return ((WebViewWidget)WidgetContainer.Controls[GetPreviousWidget(index)]).Video;
            }
            return null;
        }

        public ScrapedVideo GetNextVideo(ScrapedVideo current)
        {
            int index = -1;
            for (var i = 0; i < WidgetContainer.Controls.Count; i++)
            {
                var v = WidgetContainer.Controls[i] as WebViewWidget;
                if (v != null && v.Video == current)
                {
                    index = i;
                    break;
                }
            }

            if (WidgetContainer.Controls.Count > 0)
            {
                if (index == -1 || index == GetWidgetLastIndex())
                    return ((WebViewWidget)WidgetContainer.Controls[0]).Video;
                else
                    return ((WebViewWidget)WidgetContainer.Controls[GetNextWidget(index)]).Video;
            }
            return null;
        }

        protected virtual Control WidgetContainer { get { throw new NotImplementedException(); } }
    }

    public class GalleryItemSelectedEventArgs : EventArgs
    {
        public GalleryItemSelectedEventArgs(ScrapedVideo video)
        {
            Video = video;
        }

        public ScrapedVideo Video { get; set; }
    }

    public class GalleryPageSelectedEventArgs : EventArgs
    {
        public GalleryPageSelectedEventArgs(PagingLink link)
        {
            Link = link;
        }

        public PagingLink Link { get; set; }
    }
}
