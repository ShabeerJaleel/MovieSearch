using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Linq;

namespace MovieFinder.Scraper
{
    public class DataService 
    {
        private static SQLiteConnection connection;
        private static object sync = new object();

        public DataService(string name)
        {
            connection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", name));
            connection.Open();
        }
        //private DataService() { }
        //public static DataService Create()
        //{
        //    return new DataService();
        //}

        public void ShutDown()
        {
            connection.Dispose();
        }
       
        public void AddMovie(MovieFinder.Data.Movie movie)
        {
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = String.Format("INSERT INTO MOVIE(ID,Name,ImageUrl,ReleaseDate,LanguageCode,Description, CreatedDate,ModifiedDate, Version, UniqueID, HasSubtitle) " +
                    "VALUES({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}',{10})",
                    movie.ID, Sanitize(movie.Name), movie.ImageUrl, movie.ReleaseDate.ToString("yyyy-MM-dd"),
                    movie.LanguageCode,Sanitize( movie.Description),
                    movie.CreateDate.ToString("yyyy-MM-dd"), 
                    movie.ModifiedDate != null ?
                    movie.ModifiedDate.Value.ToString("yyyy-MM-dd") : null, movie.Version, 
                    movie.UniqueID, movie.MovieLinks.Any(x => x.HasSubtitle) ? 1 : 0);
                cmd.ExecuteNonQuery();

                foreach (var link in movie.MovieLinks)
                {
                    if (link.FailedAttempts > 3)
                        continue;
                    cmd.CommandText = String.Format("INSERT INTO MOVIELINK(ID,MovieID,LinkTitle,PageUrl,PageSiteID,DownloadUrl,DownloadSiteID,Version, HasSubtitle) " +
                    "VALUES({0},{1},'{2}','{3}','{4}','{5}','{6}',{7},{8})",
                    link.ID, link.MovieID, Sanitize(link.LinkTitle), link.PageUrl, link.PageSiteID, link.DowloadUrl, link.DownloadSiteID,
                    link.Version, link.HasSubtitle ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMovieSettings(int version, DateTime createdDate)
        {
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = String.Format("UPDATE SETTINGS SET Version = {0}, CreatedDate= '{1}' ", version, createdDate.ToString("yyyy-MM-dd hh:mm:ss"));
                cmd.ExecuteNonQuery();
            }
        }

        public int MovieDBVersion
        {
            get
            {

                using (var cmd = new SQLiteCommand(connection))
                {

                    cmd.CommandText = "SELECT * FROM Settings LIMIT 1";
                    using (var reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        return (int)reader["Version"];
                    }
                }

            }
        }

        private string Sanitize(string text)
        {
            return text.Replace("'", "''");
        }


    }
}
