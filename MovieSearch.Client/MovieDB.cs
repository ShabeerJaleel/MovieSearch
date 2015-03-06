using System;
using System.Collections.Generic;
using System.Text;

namespace MovieFinder.Client
{
    public class MovieDB
    {
        public MovieDB()
        {
            Movies = new List<Movie>();
        }

        public int Version { get; set; }
        public List<Movie> Movies { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
