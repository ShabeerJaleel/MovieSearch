using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MovieFinder.Client
{
    public partial class DBUpdatedForm : Form
    {
        public DBUpdatedForm()
        {
            InitializeComponent();
        }

        public void ShowMe(List<Movie> movies)
        {
            this.dataGridView1.DataSource = movies;
            this.ShowDialog();
        }
    }
}
