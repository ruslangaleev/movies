using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Services.Logic
{
  public class MovieFromPostManager : IMovieFromPostManager
  {
    private readonly IMovieFromPostRepository _movieFromPostRepository;

    public MovieFromPostManager(IMovieFromPostRepository movieFromPostRepository)
    {
      _movieFromPostRepository = movieFromPostRepository ?? throw new ArgumentNullException(nameof(movieFromPostRepository));
    }

    public async Task Add(MovieFromPost movieFromPost)
    {
      _movieFromPostRepository.Add(movieFromPost);

      await _movieFromPostRepository.SaveAsync();
    }

    public MovieListModel Get(string like, int page = 1, int pageSize = 20)
    {
      var items = _movieFromPostRepository.Get(t => t.Text.ToLower().IndexOf(like.ToLower()) > -1).ToList();

      var count = items.Count();

      PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
      MovieListModel viewModel = new MovieListModel
      {
        PageViewModel = pageViewModel,
        Movies = items
      };

      return viewModel;
    }

    public MovieFromPost Get(int groupId, int postId)
    {
      return _movieFromPostRepository.Get(t => t.FromId == groupId && t.PostId == postId).ToList().FirstOrDefault();
    }

    public MovieFromPost GetRandom()
    {
      var items = _movieFromPostRepository.GetAll().ToList();
      Random random = new Random();
      int item = random.Next(0, items.Count());
      return _movieFromPostRepository.GetAll().Skip(item).Take(1).FirstOrDefault();
    }
  }
}