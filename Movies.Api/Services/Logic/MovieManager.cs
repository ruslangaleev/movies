using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Title = movieInfoModel.Title,
                UrlPoster = movieInfoModel.UrlPoster,
                MovieContents = new List<MovieContent>
                {
                    new MovieContent
                    {
                        Url = movieInfoModel.Url,
                        Quality = movieInfoModel.Quality
                    }
                }
            };

            _movieRepository.Add(movieInfo);

            _movieRepository.Save();
        }

        public void AddMovieContent(AddMovieContent content)
        {
            var movieInfo = _movieRepository.Get(content.Id) ?? throw new ArgumentNullException(nameof(content.Id));

            if (movieInfo.MovieContents.Any(t => t.Url == content.Url))
            {
                throw new ArgumentException(nameof(content.Url));
            }

            var movieContents = movieInfo.MovieContents.ToList();
            movieContents.Add(new MovieContent
            {
                Quality = content.Quality,
                Url = content.Url
            });
            movieInfo.MovieContents = movieContents;

            _movieRepository.Update(movieInfo);
            _movieRepository.Save();
        }

        public MovieListModel GetMovies(int page = 1, int pageSize = 20)
        {
            var count = _movieRepository.GetAll().Count();
            var items = _movieRepository.GetAll().OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize).Take(pageSize).ToList();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            MovieListModel viewModel = new MovieListModel
            {
                PageViewModel = pageViewModel,
                Movies = items.Select(t =>
                {
                    return new AddMovieInfo
                    {
                        // TODO: Взять фильм только с лучшим качеством
                        Quality = t.MovieContents.FirstOrDefault().Quality,
                        Title = t.Title,
                        Url = t.MovieContents.FirstOrDefault().Url,
                        UrlPoster = t.UrlPoster
                    };
                })
            };
            return viewModel;
        }
    }
}