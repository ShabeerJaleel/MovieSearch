using System;
using System.Collections.Generic;
using System.Text;
using Client.Scraper;
using System.Data.SQLite;
using System.IO;

namespace BlueTube.Viewer.Data
{
    public class DataService 
    {
        private static SQLiteConnection connection;

        static DataService()
        {
            connection = new SQLiteConnection("Data Source=mt.db;Version=3;");
            connection.Open();
        }
        private DataService() { }
        public static DataService Create()
        {
            return new DataService();
        }
       
        public void AddToFavourite(ScrapedVideo video)
        {
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = String.Format("INSERT INTO VIDEO(Url,Title,ImageUrl,Duration,PlayUrl,Quality,IsFavourite) " +
                    "VALUES('{0}','{1}','{2}',{3},'{4}',{5},{6})", Sanitize(video.Url),
                    Sanitize(video.Title), Sanitize(video.ImageUrl),
                    video.Duration.TotalSeconds, Sanitize(video.PlayUrl), 99, 1);
                cmd.ExecuteNonQuery();
            }
        }

        public void AddSearchWord(string text)
        {
            if (String.IsNullOrEmpty(text))
                return;
            using (var cmd = new SQLiteCommand(connection))
            {
                cmd.CommandText = String.Format("SELECT COUNT(*) FROM SEARCHWORD WHERE Text = '%{0}%'", Sanitize(text));
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                    return;

                cmd.CommandText = String.Format("INSERT INTO SEARCHWORD(Text) " +
                    "VALUES('{0}')", Sanitize(text));
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetAllSearchWords()
        {

            var words = new List<string>();
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = "SELECT * FROM SEARCHWORD";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        words.Add(reader[0].ToString());
                    }
                }

            }

            return words;
        }

        private string Sanitize(string text)
        {
            if (text == null)
                return text;
            return text.Replace("'", "");
        }

        public List<ScrapedVideo> SearchVideos(string query)
        {
            var videos = new List<ScrapedVideo>();
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = String.Format("SELECT * FROM VIDEO WHERE IsFavourite = 1 AND Title LIKE '%{0}%'", Sanitize(query));
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        videos.Add(new ScrapedVideo
                        {
                            Url = reader["Url"].ToString(),
                            Duration = TimeSpan.FromSeconds(Convert.ToInt32(reader["Duration"])),
                            ImageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : null,
                            PlayUrl = reader["PlayUrl"] != DBNull.Value ? reader["PlayUrl"].ToString() : null,
                            Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : null,
                            Quality = reader["Quality"] != DBNull.Value ? Convert.ToInt32(reader["Quality"]) : 100
                        });
                    }
                }

            }
            return videos;
        }

        public void DeleteFromFavourite(ScrapedVideo video)
        {
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = String.Format("DELETE FROM VIDEO WHERE Url = '{0}'", Sanitize(video.Url));
                cmd.ExecuteNonQuery();
            }
        }

        public List<ScrapedVideo> GetAllFavourites()
        {

            var videos = new List<ScrapedVideo>();
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = "SELECT * FROM VIDEO WHERE IsFavourite = 1";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        videos.Add(new ScrapedVideo
                        {
                            Url = reader["Url"].ToString(),
                            Duration = TimeSpan.FromSeconds(Convert.ToInt32(reader["Duration"])),
                            ImageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : null,
                            PlayUrl = reader["PlayUrl"] != DBNull.Value ? reader["PlayUrl"].ToString() : null,
                            Title = reader["Title"] != DBNull.Value ? reader["Title"].ToString() : null,
                            Quality = reader["Quality"] != DBNull.Value ? Convert.ToInt32(reader["Quality"]): 100
                        });
                    }
                }
                
            }

            return videos;
        }

        public bool IsFavourite(ScrapedVideo video)
        {
            using (var cmd = new SQLiteCommand(connection))
            {

                cmd.CommandText = String.Format("SELECT COUNT(*) FROM VIDEO WHERE Url = '{0}'", Sanitize(video.Url));
                var count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            
        }

    }
}
