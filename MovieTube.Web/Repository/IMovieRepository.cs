using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MovieTube.Web.Models;

namespace MovieTube.Web.Repository
{
    public interface IMovieRepository
    {
        ThumbNailVm List(string term, string language, int? year, int? page);
        List<MovieThumbnailVm> QueryMovies(string term, string langCode, int? year, int? page, int count);
        MovieVm QueryMovie(string id, bool updateStat = false);
        void RemoveLink(string link);
    }
}
