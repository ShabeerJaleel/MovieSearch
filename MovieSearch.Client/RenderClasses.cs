using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BrightIdeasSoftware;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics;

namespace MovieFinder.Client
{
    /// <summary>
    /// Hackish renderer that draw a fancy version of a person for a Tile view.
    /// </summary>
    /// <remarks>This is not the way to write a professional level renderer.
    /// It is hideously inefficient (we should at least cache the images),
    /// but it is obvious</remarks>
    internal class BusinessCardRenderer : AbstractRenderer
    {
        #region Fields
        private static Queue<Movie> imagedUrls = new Queue<Movie>();
        
        private static Thread workerThread = new Thread(ImageDowloadWorker);
        private static volatile bool stopThread;
        #endregion

        #region Constructor

        static BusinessCardRenderer()
        {
            if (!Directory.Exists(Movie.ImgDirectory))
                Directory.CreateDirectory(Movie.ImgDirectory);
            workerThread.IsBackground = true;
            workerThread.Start();
        }

        #endregion

        #region Methods

        public override bool RenderItem(DrawListViewItemEventArgs e, Graphics g, Rectangle itemBounds, object rowObject)
        {
            // If we're in any other view than Tile, return false to say that we haven't done
            // the rendereing and the default process should do it's stuff
            ObjectListView olv = e.Item.ListView as ObjectListView;
            if (olv == null || olv.View != View.Tile)
                return false;

            // Use buffered graphics to kill flickers
            BufferedGraphics buffered = BufferedGraphicsManager.Current.Allocate(g, itemBounds);
            g = buffered.Graphics;
            g.Clear(olv.BackColor);
            g.SmoothingMode = ObjectListView.SmoothingMode;
            g.TextRenderingHint = ObjectListView.TextRenderingHint;

            if (e.Item.Selected)
            {
                this.BorderPen = Pens.Blue;
                this.HeaderBackBrush = new SolidBrush(olv.HighlightBackgroundColorOrDefault);
            }
            else
            {
                this.BorderPen = new Pen(Color.FromArgb(0x33, 0x33, 0x33));
                this.HeaderBackBrush = new SolidBrush(Color.FromArgb(0x33, 0x33, 0x33));
            }
            DrawBusinessCard(g, itemBounds, rowObject, olv, (OLVListItem)e.Item);

            // Finally render the buffered graphics
            buffered.Render();
            buffered.Dispose();

            // Return true to say that we've handled the drawing
            return true;
        }

        internal Pen BorderPen = new Pen(Color.FromArgb(0x33, 0x33, 0x33));
        internal Brush TextBrush = new SolidBrush(Color.FromArgb(0x22, 0x22, 0x22));
        internal Brush HeaderTextBrush = Brushes.AliceBlue;
        internal Brush HeaderBackBrush = new SolidBrush(Color.FromArgb(0x33, 0x33, 0x33));
        internal Brush BackBrush = Brushes.LemonChiffon;

        public void DrawBusinessCard(Graphics g, Rectangle itemBounds, object rowObject, ObjectListView olv, OLVListItem item)
        {
            try
            {
                const int spacing = 8;

                // Allow a border around the card
                itemBounds.Inflate(-2, -2);

                // Draw card background
                const int rounding = 20;
                GraphicsPath path = this.GetRoundedRect(itemBounds, rounding);
                g.FillPath(this.BackBrush, path);
                g.DrawPath(this.BorderPen, path);

                g.Clip = new Region(itemBounds);

                // Draw the photo
                Rectangle photoRect = itemBounds;
                photoRect.Inflate(-spacing, -spacing);
                Movie movie = rowObject as Movie;
                if (movie != null)
                {
                    photoRect.Width = 200;
                    if (!File.Exists(movie.ImagePath))
                        DownloadImage(movie);

                    if (IsFileExists(movie.ImagePath))
                    {
                        try
                        {
                            using (var photo = Image.FromFile(movie.ImagePath))
                            {
                                if (photo.Width > photoRect.Width)
                                    photoRect.Height = (int)(photo.Height * ((float)photoRect.Width / photo.Width));
                                else
                                    photoRect.Height = photo.Height;
                                g.DrawImage(photo, photoRect);
                            }
                        }
                        catch
                        {
                            g.DrawRectangle(Pens.DarkGray, photoRect);
                            File.Delete(movie.ImagePath);
                        }

                    }
                    else
                    {
                        g.DrawRectangle(Pens.DarkGray, photoRect);
                    }
                }

                // Now draw the text portion
                RectangleF textBoxRect = photoRect;
                textBoxRect.X += (photoRect.Width + spacing);
                textBoxRect.Width = itemBounds.Right - textBoxRect.X - spacing;

                using (StringFormat fmt = new StringFormat())
                {
                    fmt.Trimming = StringTrimming.EllipsisCharacter;
                    fmt.Alignment = StringAlignment.Center;
                    fmt.LineAlignment = StringAlignment.Near;
                    String txt = item.Text;

                    using (Font font = new Font("Tahoma", 11))
                    {
                        // Measure the height of the title
                        SizeF size = g.MeasureString(txt, font, (int)textBoxRect.Width, fmt);
                        // Draw the title
                        RectangleF r3 = textBoxRect;
                        r3.Height = size.Height;
                        path = this.GetRoundedRect(r3, 15);
                        g.FillPath(this.HeaderBackBrush, path);
                        g.DrawString(txt, font, this.HeaderTextBrush, textBoxRect, fmt);
                        textBoxRect.Y += size.Height + spacing;
                    }

                    // Draw the other bits of information
                    using (Font font = new Font("Tahoma", 8))
                    {
                        for (int i = 0; i < olv.Columns.Count; i++)
                        {
                            OLVColumn column = olv.GetColumn(i);
                            if (column.IsTileViewColumn)
                            {
                                SizeF size = g.MeasureString(column.GetStringValue(movie), font, (int)textBoxRect.Width, fmt);
                                textBoxRect.Height = size.Height;
                                fmt.Alignment = StringAlignment.Near;
                                txt = column.GetStringValue(rowObject);
                                g.DrawString(txt, font, this.TextBrush, textBoxRect, fmt);
                                textBoxRect.Y += size.Height ;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine("This exception: " + ex.Message); }
        }

        private GraphicsPath GetRoundedRect(RectangleF rect, float diameter)
        {
            GraphicsPath path = new GraphicsPath();

            RectangleF arc = new RectangleF(rect.X, rect.Y, diameter, diameter);
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }

        private bool IsFileExists(string file)
        {
            try
            {
                if (File.Exists(file))
                {
                    using (var currentWriteableFile = File.OpenWrite(file)) { }
                }
            }
            catch { return false; }
            return true;
        }

        private void DownloadImage(Movie movie)
        {
            if (String.IsNullOrEmpty(movie.ImagePath))
                return;

            if (!File.Exists(movie.ImagePath))
            {
                lock (imagedUrls)
                {
                    imagedUrls.Enqueue(movie);
                }
            }
        }

        private static void ImageDowloadWorker()
        {
            while (!stopThread)
            {
                try
                {
                    Movie movie = null;
                    lock (imagedUrls)
                    {
                        if (imagedUrls.Count > 0)
                            movie = imagedUrls.Dequeue();
                        else
                            Thread.Sleep(100);
                    }
                    if (movie != null && !String.IsNullOrEmpty(movie.ImagePath) && !File.Exists(movie.ImagePath))
                    {
                        using (var client = new WebClient())
                        {
                            try
                            {
                                client.DownloadFile(movie.ImageUrl, movie.ImagePath);
                            }
                            catch { }
                        }
                    }
                }
                catch { }
            }
        }

        public static void StopThread()
        {
            stopThread = true;
            workerThread.Join(5000);
        }

        #endregion


    }

    /// <summary>
    /// This simple class just shows how an overlay can be drawn when the hot item changes.
    /// </summary>
    internal class BusinessCardOverlay : AbstractOverlay
    {
        public BusinessCardOverlay()
        {
            businessCardRenderer.HeaderBackBrush = Brushes.DarkBlue;
            businessCardRenderer.BorderPen = new Pen(Color.DarkBlue, 2);
            this.Transparency = 255;
        }
        #region IOverlay Members

        public override void Draw(ObjectListView olv, Graphics g, Rectangle r)
        {
            if (olv.HotRowIndex < 0)
                return;

            if (olv.View == View.Tile)
                return;

            OLVListItem item = olv.GetItem(olv.HotRowIndex);
            if (item == null)
                return;

            Size cardSize = new Size(400, 250);
            Rectangle cardBounds = new Rectangle(
                r.Right - cardSize.Width - 8, r.Bottom - cardSize.Height - 20, cardSize.Width, cardSize.Height);
            businessCardRenderer.DrawBusinessCard(g, cardBounds, item.RowObject, olv, item);
        }

        #endregion

        private readonly BusinessCardRenderer businessCardRenderer = new BusinessCardRenderer();
    }

}
