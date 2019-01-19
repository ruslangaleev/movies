using Movies.Api.ResourceModels;

namespace Movies.Api.Services.Interfaces
{
    public interface IMovieManager
    {
        void AddMovieInfo(AddMovieInfo movieInfoModel);

        void AddMovieSource(AddMovieSource content);

        MovieListModel GetMovies(int page = 1, int pageSize = 20);

        MovieListModel GetMovies(string like, int page = 1, int pageSize = 20);
    }
}
