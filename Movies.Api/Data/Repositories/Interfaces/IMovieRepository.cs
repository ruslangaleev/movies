using Movies.Api.Models;
using Movies.Api.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Api.Data.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        void Add(MovieInfo movieInfo);

        void Update(MovieInfo movieInfo);

        MovieInfo Get(Guid id);

        IQueryable<MovieInfo> GetAll();

        void Save();
    }
}
