using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Movies.Api.Data.Repositories.Logic
{
    public class MovieRepository : IMovieRepository
    {
        private DbContext _dbContext;

        private readonly DbSet<MovieInfo> _movies;

        public MovieRepository(DbContext dbContext)
        {
            _movies = dbContext.Set<MovieInfo>();

            _dbContext = dbContext;
        }

        public void Add(MovieInfo movieInfo)
        {
            _movies.Add(movieInfo);
        }

        public MovieInfo Get(Guid id)
        {
            return _movies.Find(id);
        }

        public IQueryable<MovieInfo> Get()
        {
            return _movies.AsQueryable();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(MovieInfo movieInfo)
        {
            
        }

        public IEnumerable<MovieInfo> Get(Expression<Func<MovieInfo, bool>> predicate)
        {
            return _movies.Where(predicate).ToList();
        }
    }
}