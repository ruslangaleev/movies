using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.Data.Repositories.Logic
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<MovieInfo> _movies;

        public MovieRepository()
        {
            _movies = new List<MovieInfo>();
        }

        public void Add(MovieInfo movieInfo)
        {
            _movies.Add(movieInfo);
        }

        public MovieInfo Get(Guid id)
        {
            return _movies.Find(t => t.Id == id);
        }

        public IQueryable<MovieInfo> GetAll()
        {
            return _movies.AsQueryable();
        }

        public void Save()
        {

        }

        public void Update(MovieInfo movieInfo)
        {

        }
    }
}