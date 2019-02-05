using Movies.Api.Models;
using Movies.Api.ResourceModels;
using System.Threading.Tasks;

namespace Movies.Api.Services.Interfaces
{
  public interface IMovieFromPostManager
  {
    Task Add(MovieFromPost movieFromPost);

    MovieListModel Get(string like, int page = 1, int pageSize = 20);

    MovieFromPost Get(int groupId, int postId);

    MovieFromPost GetRandom();
  }
}
