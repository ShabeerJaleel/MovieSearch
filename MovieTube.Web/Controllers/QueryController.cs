﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MovieTube.Web.Models;
using MovieFinder.Data;
using WebAPI.OutputCache;
using MovieTube.Web.Repository;
using MovieTube.Web.Attributes;

namespace MovieTube.Web.Controllers
{
    [UserProfileFilter]
    public class QueryController : ApiController
    {
      
        private readonly IMovieRepository repository ;

        public QueryController()
        {
            this.repository = new MovieRepository();
        }
        
        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public ThumbNailVm List(string language, int? year, int? page, string term = "")
        {
            return repository.List(term, language, year, page);
        }

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public List<SearchResult> Search(string term, string language, int year = 0, int count = 10)
        {
            return repository.QueryMovies(term, language, year, 0, count)
                      .Take(10)
                      .ToList()
                      .Select(x => new SearchResult
                      {
                          name = x.Title + " ( " + x.Language + ") - " + x.ReleasedYear,
                          id = x.Title
                      }).ToList();

        }

        [HttpGet]
        //[CacheOutput(ClientTimeSpan = 3600, ServerTimeSpan = 3600)]
        public MovieVm Movie(string id)
        {
            return repository.QueryMovie(id, true);
        }

        [HttpPost]
        public void RemoveLink(string link)
        {
            repository.RemoveLink(link);
        }

       
    }
}
