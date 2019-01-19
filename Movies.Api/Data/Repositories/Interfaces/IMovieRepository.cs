using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Movies.Api.Data.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        void Add(MovieInfo movieInfo);

        void Update(MovieInfo movieInfo);

        MovieInfo Get(Guid id);

        IQueryable<MovieInfo> Get();

        IEnumerable<MovieInfo> Get(Expression<Func<MovieInfo, bool>> predicate);

        void Save();
    }
}
