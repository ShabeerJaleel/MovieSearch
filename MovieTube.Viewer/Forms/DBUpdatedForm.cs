using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MovieTube.Client.Scraper;
using System.Linq;

namespace MovieTube.Viewer
{
    public partial class DBUpdatedForm : Form
    {
        public DBUpdatedForm()
        {
            InitializeComponent();
            this.browseGalleryWidget1.ShowHeader = false;
        }

        public void ShowMe(List<Movie> movies)
        {
            this.browseGalleryWidget1.AddItems(new MoviePage{
                 Videos = movies.Where(x => x.ModifiedDate == null).Take(15).ToList()
            });
            this.dataGridView1.DataSource = movies.Where(x => x.ModifiedDate != null).Take(25).ToList();
            this.ShowDialog();
        }
    }
}
