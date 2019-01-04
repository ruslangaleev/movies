using Movies.Api.Models;
using Movies.Api.ResourceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Api.Services.Interfaces
{
    public interface IMovieManager
    {
        void AddMovieInfo(AddMovieInfo movieInfoModel);

        void AddMovieContent(AddMovieSource content);

        MovieListModel GetMovies(int page = 1, int pageSize = 20);
    }
}
