using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Api.Services.Logic
{
    public class MovieManager : IMovieManager
    {
        private readonly IMovieRepository _movieRepository;

        public MovieManager(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
        }

        public void AddMovieInfo(AddMovieInfo movieInfoModel)
        {
            // TODO: Проверка на уникальность фильма, может такой уже есть

            var movieInfo = new MovieInfo
            {
                // TODO: Сделать инициализацию со стороны базы данных
                Id = Guid.NewGuid(),
                Title = movieInfoModel.Title,
                UrlPoster = movieInfoModel.UrlPoster,
                CreateAt = DateTime.Now,
                UpdateAt = DateTime.Now,
                MovieSources = new List<MovieSource>
                {
                    new MovieSource
                    {
                        // TODO: Сделать инициализацию со стороны базы данных
                        Id = Guid.NewGuid(),
                        Url = movieInfoModel.Url,
                        Quality = movieInfoModel.Quality,
                        CreateAt = DateTime.Now
                    }
                }
            };

            try
            {
                _movieRepository.Add(movieInfo);
            }
            catch(Exception e)
            {
                var s = e;
            }

            _movieRepository.Save();
        }

        public void AddMovieSource(AddMovieSource content)
        {
            var movieInfo = _movieRepository.Get(content.Id) ?? throw new ArgumentNullException(nameof(content.Id));

            if (movieInfo.MovieSources.Any(t => t.Url == content.Url))
            {
                throw new ArgumentException(nameof(content.Url));
            }

            var movieContents = movieInfo.MovieSources.ToList();
            movieContents.Add(new MovieSource
            {
                Id = Guid.NewGuid(),
                Quality = content.Quality,
                Url = content.Url,
                CreateAt = DateTime.Now
            });
            movieInfo.MovieSources = movieContents;

            _movieRepository.Update(movieInfo);
            _movieRepository.Save();
        }

        public MovieListModel GetMovies(int page = 1, int pageSize = 20)
        {
            var count = _movieRepository.Get().Count();
            var items = _movieRepository.Get()
                .OrderBy(t => t.CreateAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MovieListModel viewModel = new MovieListModel
            {
                PageViewModel = pageViewModel,
                Movies = items.Select(t =>
                {
                    var bestQuality = t.GetBestMovieSource();

                    return new AddMovieInfo
                    {
                        Quality = bestQuality.Quality,
                        Title = t.Title,
                        Url = bestQuality.Url,
                        UrlPoster = t.UrlPoster
                    };
                })
            };
            return viewModel;
        }

        public async Task<MovieListModel> GetMovies(string like, int page = 1, int pageSize = 20)
        {
            var items = await _movieRepository.Get(t => t.Title.ToLower().IndexOf(like.ToLower()) > -1);

            var count = items.Count();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MovieListModel viewModel = new MovieListModel
            {
                PageViewModel = pageViewModel,
                Movies = items.Select(t =>
                {
                    var bestQuality = t.GetBestMovieSource();
                    return new AddMovieInfo
                    {
                        Quality = bestQuality.Quality,
                        Title = t.Title,
                        Url = bestQuality.Url,
                        UrlPoster = t.UrlPoster
                    };
                })
            };
            return viewModel;
        }
    }
}