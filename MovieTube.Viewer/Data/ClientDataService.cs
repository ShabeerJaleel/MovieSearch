using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using MovieTube.Client.Scraper;
using System.Configuration;
using System.Reflection;
using System.Linq;

namespace MovieTube.Viewer.Data
{
    public class ClientDataService 
    {
        private static SQLiteConnection connection;
        private static SQLiteConnection prefConnection;
        private static ClientDataService dataService;
        private static object sync = new object();

        private ClientDataService() { }
        public static ClientDataService Single
        {
            get
            {
                lock (sync)
                {
                    if (dataService == null)
                    {
                        dataService = new ClientDataService();
                        var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "pref.db"));
                        prefConnection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", path));
                        prefConnection.Open();
                    }

                    if (connection == null)
                    {
                        var path = ConfigurationManager.AppSettings["VideoPath"];
                        var dbPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path));
                        if (File.Exists(dbPath))
                        {
                            connection = new SQLiteConnection(String.Format("Data Source={0};Version=3;", dbPath));
                            connection.Open();
                        }
                    }
                }

                return dataService;
            }
        }

        public void Close()
        {
            lock (sync)
            {
                if (connection != null)
                {
                    connection.Close();
                    connection = null;
                }
            }

        }
        public void AddToFavourite(Movie movie)
        {
            lock (sync)
            {
                using (var cmd = new SQLiteCommand(prefConnection))
                {
                    cmd.CommandText = String.Format("INSERT INTO Favourite(UniqueID,CreatedDate) " +
                        "VALUES('{0}','{1}')", Sanitize(movie.UniqueID), DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        

        public void AddSearchWord(string text)
        {
            lock (sync)
            {
                if (String.IsNullOrEmpty(text))
                    return;
                using (var cmd = new SQLiteCommand(prefConnection))
                {
                    cmd.CommandText = String.Format("SELECT COUNT(*) FROM SEARCHWORD WHERE Text like '%{0}%'", Sanitize(text));
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                        return;

                    cmd.CommandText = String.Format("INSERT INTO SEARCHWORD(Text) " +
                        "VALUES('{0}')", Sanitize(text));
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetAllSearchWords()
        {
            lock (sync)
            {
                var words = new List<string>();
                using (var cmd = new SQLiteCommand(prefConnection))
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
        }

        private string Sanitize(string text)
        {
            if (text == null)
                return text;
            return text.Replace("'", "");
        }

        public List<Movie> SearchFavourites(SearchParameters sParam)
        {
            lock (sync)
            {
                var favourties = GetAllFavourites().Videos;

                if (!String.IsNullOrEmpty(sParam.Query))
                    favourties = favourties.FindAll(x => x.Name.ToLower().Contains(sParam.Query.ToLower()));
                if (!String.IsNullOrEmpty(sParam.Language))
                    favourties = favourties.FindAll(x => x.LanguageCode == sParam.Language);

                if (sParam.Year != 0)
                {
                    if (sParam.Year > 1960)
                        favourties = favourties.FindAll(x => x.ReleaseDate.Year == sParam.Year);
                    else
                        favourties = favourties.FindAll(x => x.ReleaseDate.Year >= sParam.Year && x.ReleaseDate.Year <= x.ReleaseDate.Year + 9);
                }
                return favourties;
            }
        }

        public void DeleteFromFavourite(Movie video)
        {
            lock (sync)
            {
                using (var cmd = new SQLiteCommand(prefConnection))
                {

                    cmd.CommandText = String.Format("DELETE FROM Favourite WHERE UniqueID = '{0}'", video.UniqueID);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public MoviePage GetAllFavourites()
        {
            lock (sync)
            {

                var ids = new List<string>();
                using (var cmd = new SQLiteCommand(prefConnection))
                {

                    cmd.CommandText = "SELECT * FROM Favourite ORDER BY CreatedDate DESC";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add((string)reader["UniqueID"]);
                        }
                    }

                }

                return GetAllMovies(ids);
            }
        }

        public bool IsFavourite(Movie video)
        {
            lock (sync)
            {
                using (var cmd = new SQLiteCommand(prefConnection))
                {

                    cmd.CommandText = String.Format("SELECT COUNT(*) FROM Favourite WHERE UniqueID = '{0}'", video.UniqueID);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            
        }

     

        //new
        public MoviePage GetAllMovies(List<string> uniqueIDs)
        {
            lock (sync)
            {
                var movies = new List<Movie>();
                if (uniqueIDs.Count > 0)
                {

                    using (var cmd = new SQLiteCommand(connection))
                    {
                        string where = "WHERE UniqueID IN(";
                        foreach (var id in uniqueIDs)
                            where += "'" + id + "',";

                        where = String.Format("{0}{1}", where.Remove(where.Length - 1, 1), ") ");

                        cmd.CommandText = String.Format("SELECT  ID, CAST(CreatedDate as nvarchar(20)) CreatedDate , Description, LanguageCode," +
                            "CAST(ModifiedDate as nvarchar(20)) ModifiedDate , Name,CAST(ReleaseDate as nvarchar(20)) ReleaseDate , Version, ImageUrl, UniqueID" +
                            " FROM Movie {0} ORDER BY ReleaseDate DESC, CreatedDate ", where);

                        using (var reader = cmd.ExecuteReader())
                            while (reader.Read())
                                movies.Add(ReadMovie(reader));
                       // ReadMovieLinks(movies, cmd);
                    }
                }
                return new MoviePage { Videos = movies };
            }
        }


        public MoviePage GetAllMovies(SearchParameters sParam)
        {
            lock (sync)
            {
                if (sParam == null)
                    throw new ArgumentNullException("sParam");
                var count = sParam.Year != 0 ? 500 : 200;

                var movies = new List<Movie>();

                if (sParam.Query == "_new" || sParam.Year == -1)
                {
                    movies = GetLatestMovies(sParam.Language, 2);
                }
                else
                {

                    using (var cmd = new SQLiteCommand(connection))
                    {
                        string where = null;
                        if (!String.IsNullOrEmpty(sParam.Query))
                            where = String.Format(" WHERE Movie.Name LIKE '%{0}%'", sParam.Query);
                        if (!String.IsNullOrEmpty(sParam.Language))
                            where += String.Format(" {0} Movie.LanguageCode = '{1}'", where == null ? "WHERE" : "AND", sParam.Language);
                        if (sParam.Year != 0)
                        {
                            if (sParam.Year > 1960)
                                where += String.Format(" {0} CAST(strftime('%Y', ReleaseDate) as INT)  = {1} ", where == null ? "WHERE" : "AND", sParam.Year);
                            else
                            {
                                var start = sParam.Year;
                                var end = sParam.Year + 9;
                                where += String.Format(" {0} (CAST(strftime('%Y', ReleaseDate) as INT)  >= {1} AND CAST(strftime('%Y', ReleaseDate) as INT)  <= {2} ) ",
                                    where == null ? "WHERE" : "AND", start, end);

                            }
                        }

                        cmd.CommandText = String.Format("SELECT  ID, CAST(CreatedDate as nvarchar(20)) CreatedDate , Description, LanguageCode," +
                            "CAST(ModifiedDate as nvarchar(20)) ModifiedDate , Name,CAST(ReleaseDate as nvarchar(20)) ReleaseDate , Version, ImageUrl, UniqueID" +
                            " FROM Movie {0} ORDER BY ReleaseDate DESC, CreatedDate DESC LIMIT {1}", where, count);

                        using (var reader = cmd.ExecuteReader())
                            while (reader.Read())
                                movies.Add(ReadMovie(reader));
                        //ReadMovieLinks(movies, cmd);
                    }
                }

                return new MoviePage { Videos = movies };
            }
        }



        public List<Movie> GetLatestMovies(string language = "", int prevCount = 0)
        {
            lock (sync)
            {
                var movies = new List<Movie>();

                using (var cmd = new SQLiteCommand(connection))
                {
                    string where = null;
                    if (!String.IsNullOrEmpty(language))
                        where = String.Format(" AND Movie.LanguageCode = '{0}'", language);

                    cmd.CommandText = String.Format("SELECT  ID, CAST(CreatedDate as nvarchar(20)) CreatedDate , Description, LanguageCode," +
                        "CAST(ModifiedDate as nvarchar(20)) ModifiedDate , Name,CAST(ReleaseDate as nvarchar(20)) ReleaseDate , Version, ImageUrl, UniqueID" +
                        " FROM Movie WHERE Version > {0} {1} ORDER BY ReleaseDate DESC, CreatedDate DESC LIMIT 100",
                        AppSettings.LastDBVersion - prevCount, where);

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            movies.Add(ReadMovie(reader));
                        }

                }
                return movies;
            }
        }

        public GlobalSettings Settings
        {
            get
            {
                lock (sync)
                {
                    var ids = new List<string>();
                    using (var cmd = new SQLiteCommand(prefConnection))
                    {

                        cmd.CommandText = "SELECT * FROM Settings LIMIT 1";
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            return new GlobalSettings
                            {
                                DefaultLanguage = (string)reader["DefaultLanguage"],
                                AppVersionCheckedTime = DateTime.Parse(reader["AppVersionCheckedTime"].ToString()),
                                DBVersionCheckedTime = DateTime.Parse(reader["DBVersionCheckedTime"].ToString()),
                                PlayFirstLink = (bool)reader["PlayFirstLink"]
                            };
                        }

                    }
                }
            }
        }

        public void SaveGlobalSettings(GlobalSettings settings)
        {
            lock (sync)
            {
                var ids = new List<string>();
                using (var cmd = new SQLiteCommand(prefConnection))
                {

                    cmd.CommandText = String.Format("UPDATE Settings SET DefaultLanguage = '{0}', AppVersionCheckedTime= '{1}', " +
                                                    "DBVersionCheckedTime= '{2}', PlayFirstLink={3}",
                                                    settings.DefaultLanguage, settings.AppVersionCheckedTime.ToString("yyyy-MM-dd hh:mm:ss"),
                                                    settings.DBVersionCheckedTime.ToString("yyyy-MM-dd HH:mm:ss"), 
                                                    settings.PlayFirstLink ? 1 : 0);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int MovieDBVersion
        {
            get
            {

                lock (sync)
                {
                    var s  = Single;
                    if (connection == null)
                        return -1;

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
        }

        private Movie ReadMovie(SQLiteDataReader reader)
        {
            return new Movie
                        {
                            ID = (int)reader["ID"],
                            CreatedDate = DateTime.Parse(reader["CreatedDate"].ToString()),
                            Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : null,
                            ImageUrl = reader["ImageUrl"] != DBNull.Value ? reader["ImageUrl"].ToString() : null,
                            LanguageCode = reader["LanguageCode"].ToString(),
                            ModifiedDate = reader["ModifiedDate"] != DBNull.Value && !String.IsNullOrEmpty(reader["ModifiedDate"].ToString())
                            ? DateTime.Parse(reader["ModifiedDate"].ToString()) : (DateTime?)null,
                            Name = reader["Name"].ToString(),
                            ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString()),
                            Version = (int)reader["Version"],
                            UniqueID = (string)reader["UniqueID"]

                        };
        }

        public List<MovieLink> ReadMovieLinks(Movie movie)
        {
            var links = new List<MovieLink>();
            lock (sync)
            {
                using (var cmd = new SQLiteCommand(connection))
                {

                    cmd.CommandText = String.Format("SELECT * FROM MovieLink WHERE MovieID = {0} ORDER BY ID", movie.ID);
                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                        {
                            var url = reader["DownloadUrl"].ToString();
                            if(VideoScraperBase.GetScraper(url) == null)
                                continue;
                            int version = MovieDBVersion - 1;
                            try
                            {
                                version = (int)reader["Version"];
                            }
                            catch { }
                            links.Add(new MovieLink
                            {
                                ID = (int)reader["ID"],
                                DowloadUrl = url,
                                LinkTitle = reader["LinkTitle"].ToString(),
                                Parent = movie,
                                DownloadSiteID = reader["DownloadSiteID"].ToString(),
                                Version = version
                            });
                        }
                    links.Sort();
                }
            }
            return links;
        }
        
    }
}
