using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using MovieFinder.Scraper;
using MovieFinder.Data;
using System.Data.Entity;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Diagnostics;




namespace MovieFinder.ScraperClient
{
    public partial class MainForm : Form
    {
        #region Fields

        private static SortableBindingList<ScrapedMovie> movies = new SortableBindingList<ScrapedMovie>();
        private bool stop;
        private static int NewDBVersion;
        private bool running;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            this.dataGridView.DataSource = movies;
            var path = ConfigurationManager.AppSettings["ExportPath"];
            this.labelDBPath.Text = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path, "movie.db"));
        }

        #endregion

        #region Events

        private void buttonStopValidation_Click(object sender, EventArgs e)
        {
            this.stop = true;
        }

        private void richTextBoxRemoved_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void buttonTVCD_Click(object sender, EventArgs e)
        {
            Start(new List<MovieDetailsScraperBase> { new ThriruttuVCD() });
        }

        private void buttonTamiz_Click(object sender, EventArgs e)
        {
            Start(new List<MovieDetailsScraperBase> { new TamizhWS() });
        }

        private void buttonI4M_Click(object sender, EventArgs e)
        {
            Start(new List<MovieDetailsScraperBase> { new India4movie() });
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.stop = true;
        }

        void scraper_Notify(object sender, NotificationEventArgs e)
        {
            this.InvokeEx(() =>
            {
                this.labelMsg.Text = e.Message;
            });
        }

        void scraper_ScraperNotFound(object sender, ScraperNotFound e)
        {
            AddError(e.Url + ",    " + e.PageUrl);
            
        }

        void scraper_MovieFound(object sender, MovieFoundEventArgs e)
        {
            UpdateUI(e.Movie);
            if (this.stop)
                ((MovieDetailsScraperBase)sender).Stop();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            NewDBVersion = GetDBVersion() + 1;
            Export();
        }

        private void buttonABC_Click(object sender, EventArgs e)
        {
            Start(new List<MovieDetailsScraperBase> { new ABCMalayalam() });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Start(new List<MovieDetailsScraperBase> { new Hindi4ULink() });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Start(new List<MovieDetailsScraperBase> { new Einthusan() });
        }

        private void buttonValidateLinks_Click(object sender, EventArgs e)
        {
            EnableDisable(false);
            StartValidate();

        }

        private void buttonDoAll_Click(object sender, EventArgs e)
        {

            StartAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void timer_Tick(object sender, EventArgs e)
        {
            StartAll();
        }

        private void StartAll()
        {
            if(!running)
                Start(new List<MovieDetailsScraperBase> { new Einthusan(), new ABCMalayalam(), new Hindi4ULink(), 
                    new ThriruttuVCD(), new TamizhWS(), new India4movie() }, true);
        }

        #endregion


        #region Methods
       
        private void AddError(string text)
        {
            this.InvokeEx(() =>
            {
                this.textBoxMsg.AppendText(text + Environment.NewLine) ;
            });
        }

        private int GetDBVersion()
        {
            using (var db = new MovieFinderEntities())
            {
                return db.Movies.DefaultIfEmpty().Max(p => p == null ? 0 : p.Version);
            }
        }

        private void Start(List<MovieDetailsScraperBase> scrapers, bool export = false)
        {
            this.textBoxMsg.Text = String.Empty;
            movies.Clear();
            EnableDisable(false);

            var yrs = new List<int>();
            for(var i = this.numericUpDown1.Value; i <= 2014; i++)
                yrs.Add((int)i);

            NewDBVersion = GetDBVersion() + 1;

            Task.Factory.StartNew(() =>
            {
                running = true;
                try
                {
                    foreach (var scraper in scrapers)
                    {
                        try
                        {
                            List<string> existing = null;
                            using (var db = new MovieFinderEntities())
                            {
                                existing = db.MovieLinks.Select(x => x.DowloadUrl).ToList();
                            }
                            scraper.MovieFound += new EventHandler<MovieFoundEventArgs>(scraper_MovieFound);
                            scraper.ScraperNotFound += new EventHandler<ScraperNotFound>(scraper_ScraperNotFound);
                            scraper.Notify += new EventHandler<NotificationEventArgs>(scraper_Notify);

                            scraper.ScrapeMovies(existing, yrs);
                        }
                        catch { }
                    }

                    if (export && GetDBVersion() >= NewDBVersion)
                        Export();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.InvokeEx(() =>
                    {
                        EnableDisable(true);
                    });
                    running = false;
                }
            });
        }

        private void UpdateUI(ScrapedMovie movie)
        {
            this.InvokeEx(() =>
            {
                if (movie.Links.Count == 0)
                    return;

                try
                {
                    using (var db = new MovieFinderEntities())
                    {
                        //check if movie already exists
                        var dbMovie = db.Movies.FirstOrDefault(x => (x.UniqueID == movie.UniqueId));
                        var modified = false;
                        if (dbMovie == null) //new movie
                        {
                            dbMovie = new Movie
                            {
                                CreateDate = DateTime.Now,
                                Description = movie.Description,
                                ImageUrl = movie.ImageUrl,
                                LanguageCode = movie.LangCode,
                                Name = movie.Name,
                                ReleaseDate = movie.ReleasedDate,
                                Version = NewDBVersion,
                                VersionChange = 0,
                                UniqueID = movie.UniqueId,
                                ImageScrapperID = movie.Scraper.ID
                            };
                            db.Movies.Add(dbMovie);
                        }
                        else
                        {
                            //get the previous scraper 
                            var prevScrapper = MovieDetailsScraperBase.Scrappers.First(x => x.ID == dbMovie.ImageScrapperID);

                            if (String.IsNullOrWhiteSpace(dbMovie.Description) ||
                                (dbMovie.Description.Length < 50 && !String.IsNullOrWhiteSpace(movie.Description) &&
                                movie.Description.Length > 50))
                            {
                                dbMovie.Description = movie.Description;
                                modified = true;
                            }
                            if (!String.IsNullOrWhiteSpace(movie.ImageUrl) &&
                                (String.IsNullOrWhiteSpace(dbMovie.ImageUrl) ||
                                movie.Scraper.ImagePriority < prevScrapper.ImagePriority))
                            {
                                dbMovie.ImageUrl = movie.ImageUrl;
                                dbMovie.ImageScrapperID = movie.Scraper.ID;
                                modified = true;
                            }
                        }

                        db.SaveChanges();

                        foreach (var l in movie.Links)
                        {
                            if (!db.MovieLinks.Any(x => x.DowloadUrl == l.DownloadUrl))
                            {
                                db.MovieLinks.Add(new MovieLink
                                {
                                    MovieID = dbMovie.ID,
                                    LinkTitle = l.Title,
                                    SiteTitle = movie.Scraper.Title,
                                    PageSiteID = movie.Scraper.ID,
                                    PageUrl = movie.PageUrl,
                                    DowloadUrl = l.DownloadUrl,
                                    DownloadSiteID = l.DownloadSiteID,
                                    Version = NewDBVersion,
                                    HasSubtitle = movie.Scraper.Title == "EIH"
                                });
                                db.SaveChanges();
                                modified = true;
                            }
                        }

                        if (dbMovie.Version != NewDBVersion && modified)
                        {
                            dbMovie.Version = NewDBVersion;
                            dbMovie.ModifiedDate = DateTime.Now;
                            db.SaveChanges();
                        }
                    }

                    movies.Add(movie);
                    this.labelCount.Text = "Total: " + movies.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });
        }

        private void Export()
        {
            using (var db = new MovieFinderEntities())
            {
                var movies = db.Movies.OrderByDescending(x => x.ID)
                    .Include(x => x.MovieLinks)
                    .ToList();

                var path = ConfigurationManager.AppSettings["ExportPath"];
                var moviePathTemp = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path, "movie_temp.db"));
                var moviePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path, "movie.db"));
                var videoPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path, "video.db"));

                if (File.Exists(moviePathTemp))
                    File.Delete(moviePathTemp);
                File.Copy(videoPath, moviePathTemp);
                var dataService = new DataService(moviePathTemp);
                foreach (var m in movies)
                {
                    if (m.MovieLinks.Count == 0 || m.MovieLinks.Any(x => x.PageUrl.ToLower().Contains("-in-hindi")))
                        continue;
                    dataService.AddMovie(m);
                }
                NewDBVersion = GetDBVersion();
                dataService.UpdateMovieSettings(NewDBVersion, DateTime.Now);
                dataService.ShutDown();

                if (File.Exists(moviePath))
                    File.Delete(moviePath);
                File.Copy(moviePathTemp, moviePath);

                File.Delete(moviePathTemp);

                this.InvokeEx(() =>
                {
                    this.labelLastExportedTime.Text = DateTime.Now.ToString() + "(" + NewDBVersion + ")";
                });
            }
        }

        private void StartValidate()
        {
            this.textBoxMsg.Text = String.Empty;
            int total = 0, validated = 0, removed= 0 , unknown = 0;
            var yrs = new List<int>();
            if (this.checkBoxQuickValidate.Checked)
                yrs.AddRange(new List<int> { 2014, 2013, 2012 });

            Task.Factory.StartNew(() =>
            {
                try
                {
                    List<Movie> movies = null;
                    using (var db = new MovieFinderEntities())
                        movies = db.Movies.Where(x => yrs.Contains(x.ReleaseDate.Year)) .Include(x => x.MovieLinks).ToList();
                    var d = DateTime.Now;
                    foreach (var movie in movies)
                    {
                        if (this.stop)
                            break;

                        var recs = movie.MovieLinks.Where(x => x.FailedAttempts < 4 &&
                            (x.LastValidatedWhen == null || x.LastValidatedWhen.Value.AddDays(2) < d) &&
                            x.PageSiteID != "eih").OrderByDescending(x => x.ID);
                        foreach (var ml in recs)
                        {
                            this.InvokeEx(() =>
                            {
                                this.labelValidatingLink.Text = ml.DowloadUrl;
                                this.labelTotalValidated.Text = total.ToString();
                                this.labelValidatedLink.Text = validated.ToString();
                                this.labelUnknownLink.Text = unknown.ToString();
                                this.labelRemovedLink.Text = removed.ToString();
                            });

                            var result = MovieTube.Client.Scraper.VideoScraperBase.ValidateUrl(ml.DowloadUrl);
                            total++;
                            switch (result)
                            {
                                case MovieTube.Client.Scraper.ScraperResult.Success:
                                    validated++;
                                    break;
                                case MovieTube.Client.Scraper.ScraperResult.UnknownError:
                                    unknown++;
                                    AddError("Unknown error: " + ml.DowloadUrl);
                                    continue;
                                case MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist:
                                    this.InvokeEx(() =>
                                        {
                                            this.richTextBoxRemoved.AppendText(ml.DowloadUrl + Environment.NewLine);
                                        });
                                    removed++;
                                    break;
                                default:
                                    break;
                            }
                            
                            using (var db = new MovieFinderEntities())
                            {
                                var dbMl = db.MovieLinks.Single(x => x.ID == ml.ID);
                                dbMl.FailedAttempts = result == MovieTube.Client.Scraper.ScraperResult.Success ? 0 :
                                    (result == MovieTube.Client.Scraper.ScraperResult.VideoDoesNotExist ? 4 : dbMl.FailedAttempts + 1);
                                dbMl.LastValidatedWhen = DateTime.Now;
                                db.SaveChanges();
                            }
                        }
                    }

                    this.InvokeEx(() =>
                    {
                        EnableDisable(true);
                        MessageBox.Show("Done");
                    });
                }
                catch (Exception ex)
                {
                    this.InvokeEx(() =>
                    {
                        EnableDisable(true);
                        MessageBox.Show(ex.Message);
                    });
                }
            });
        }

        private void EnableDisable(bool enable)
        {
             this.buttonTamiz.Enabled =  this.buttonTVCD.Enabled =   this.buttonABC.Enabled = this.buttonDoAll.Enabled = this.buttonEIH.Enabled =
                this.buttonHL4U.Enabled = this.buttonExport.Enabled = this.buttonValidateLinks.Enabled = enable;
        }

        #endregion

        private void buttonFetchImage_Click(object sender, EventArgs e)
        {

        }

      
       

     

       
    }

    public static class Extensions
    {
        public static void InvokeEx<TControl>(this TControl control, Action action)
     where TControl : Control
        {
            if (control.InvokeRequired)
                control.BeginInvoke(action);
            else
                action();

        }
    }
}
