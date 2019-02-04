using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.Api.Data.Repositories.Logic
{
    public class MovieFromPostRepository : IMovieFromPostRepository
    {
        private DbContext _dbContext;

        private readonly DbSet<MovieFromPost> _moviesFromPosts;

        public MovieFromPostRepository(DbContext dbContext)
        {
            _moviesFromPosts = dbContext.Set<MovieFromPost>();

            _dbContext = dbContext;
        }

        public void Add(MovieFromPost movieFromPost)
        {
            _moviesFromPosts.Add(movieFromPost);
        }

        public IEnumerable<MovieFromPost> Get(Expression<Func<MovieFromPost, bool>> predicate)
        {
            return _moviesFromPosts.Where(predicate);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}