using Movies.Api.ResourceModels;
using System.Threading.Tasks;

namespace Movies.Api.Services.Interfaces
{
    public interface IMovieManager
    {
        void AddMovieInfo(AddMovieInfo movieInfoModel);

        void AddMovieSource(AddMovieSource content);

        MovieListModel GetMovies(int page = 1, int pageSize = 20);

        Task<MovieListModel> GetMovies(string like, int page = 1, int pageSize = 20);
    }
}
