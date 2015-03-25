using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieFinder.Data;
using System.Net;
using System.IO;

namespace MovieFinder.Scraper
{
    public class ImageScrapperService
    {
        
        public void Run(string imagePath)
        {
            using (var db = new MovieFinderEntities())
            {
                while (!IsCancelled)
                {
                    //var movies = db.Movies
                    //    .Where(x => x.ImageUrl != null && x.ImageLocalUrl == null)
                    //    .OrderByDescending(x => x.ID)
                    //    .Take(100).ToList();
                    //if (movies.Count == 0)
                    //    return;
                    foreach (var m in db.Movies
                        .Where(x => x.ImageUrl != null && x.ImageLocalUrl == null)
                        .OrderByDescending(x => x.ID))
                    {
                        if (IsCancelled)
                            return;
                       if(CopyImageToLocal(m, imagePath))
                           db.SaveChanges();
                    }
                }

            }
        }

        public bool CopyImageToLocal(Movie movie, string imagePath)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var downloadPath = Path.Combine(imagePath, movie.ReleaseDate.Year.ToString());
                    if (!Directory.Exists(downloadPath))
                        Directory.CreateDirectory(downloadPath);
                    downloadPath = Path.Combine(downloadPath, movie.UniqueID);
                    var fileBytes = client.DownloadData(movie.ImageUrl);

                    var fileType = client.ResponseHeaders[HttpResponseHeader.ContentType];

                    if (fileType != null)
                    {
                        switch (fileType)
                        {
                            case "image/gif":
                                downloadPath += ".gif";
                                break;
                            case "image/png":
                                downloadPath += ".png";
                                break;
                            default:
                                downloadPath += ".jpg";
                                break;
                        }

                        File.WriteAllBytes(downloadPath, fileBytes);
                    }
                    else
                    {

                    }
                    movie.ImageLocalUrl = downloadPath.Split(new string[] { imagePath }, StringSplitOptions.RemoveEmptyEntries)[0];
                    return true;
                }
                catch (Exception ex)
                {
                    File.AppendAllText("log.txt", String.Format("{0}{1}", ex.ToString(), Environment.NewLine));
                }
            }
            return false;
        }

        public bool IsCancelled { get; set; }
    }
}
