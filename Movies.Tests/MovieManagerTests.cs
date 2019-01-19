using Moq;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Movies.Tests
{
    public class MovieManagerTests
    {
        [Test]
        public void CheckSuccessAddingMovieInfo()
        {
            MovieInfo movieInfo = null;

            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(t => t.Add(It.IsAny<MovieInfo>())).Callback((MovieInfo movie) =>
            {
                movieInfo = movie;
            });
            var movieManager = new MovieManager(movieRepository.Object);
            var addMovieInfo = new AddMovieInfo
            {
                Quality = MovieQuality.Poor,
                Title = "Название фильма",
                Url = "Ссылка на фильм",
                UrlPoster = "Ссылка на постер"
            };

            movieManager.AddMovieInfo(addMovieInfo);

            Assert.AreEqual(addMovieInfo.Title, movieInfo.Title);
            Assert.AreEqual(addMovieInfo.UrlPoster, movieInfo.UrlPoster);
            Assert.AreEqual(addMovieInfo.Url, movieInfo.MovieSources.First().Url);
            Assert.AreEqual(addMovieInfo.Quality, movieInfo.MovieSources.First().Quality);
        }

        [Test]
        public void ReturnsFirstPageMovies()
        {
            var items = new MovieInfo[100].Select(t => new MovieInfo
            {
                MovieSources = new List<MovieSource>()
                {
                    new MovieSource()
                }
            });

            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(t => t.Get()).Returns(items.AsQueryable());
            var movieManager = new MovieManager(movieRepository.Object);
            var movies = movieManager.GetMovies();

            Assert.AreEqual(20, movies.Movies.Count());
            Assert.AreEqual(5, movies.PageViewModel.TotalPages);
        }

        [Test]
        public void CheckUrlCount()
        {
            var movieId = Guid.NewGuid();
            var movieInfo = new MovieInfo
            {
                Id = movieId,
                Title = "Какой-то заголовок фильма",
                MovieSources = new List<MovieSource>
                {
                    new MovieSource
                    {
                        Quality = MovieQuality.Medium,
                        Url = "Какая-то старая ссылка"
                    }
                }
            };

            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(t => t.Get(It.IsAny<Guid>())).Returns(movieInfo);
            movieRepository.Setup(t => t.Update(It.IsAny<MovieInfo>())).Callback((MovieInfo newMovieInfo) =>
            {
                movieInfo = newMovieInfo;
            });
            var movieManager = new MovieManager(movieRepository.Object);

            var addMovieSource = new AddMovieSource
            {
                Id = movieId,
                Quality = MovieQuality.Good,
                Url = "Какая-то ссылка"
            };

            movieManager.AddMovieSource(addMovieSource);

            Assert.AreEqual(2, movieInfo.MovieSources.Count);
        }

        [Test]
        public void ReturnsMovieWithGoodQuality()
        {
            var movieInfo = new MovieInfo
            {
                Id = Guid.NewGuid(),
                Title = "Какой-то заголовок фильма",
                MovieSources = new List<MovieSource>
                {
                    new MovieSource
                    {
                        Quality = MovieQuality.Medium,
                    },
                    new MovieSource
                    {
                        Quality = MovieQuality.Good,
                    }
                }
            };

            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(t => t.Get()).Returns(new List<MovieInfo>
            {
                movieInfo
            }.AsQueryable());
            var movieManager = new MovieManager(movieRepository.Object);
            var movieListModel = movieManager.GetMovies();

            Assert.AreEqual(MovieQuality.Good, movieListModel.Movies.First().Quality);
        }

        [Test]
        public void Returns2FoundMovies()
        {
            var movies = new List<MovieInfo>
            {
                new MovieInfo
                {
                    Title = "Гарри Поттер и филосовский камень",
                    MovieSources = new List<MovieSource>
                    {
                        new MovieSource()
                    }
                },
                new MovieInfo
                {
                    Title = "Гарри Поттер и Терминатор",
                    MovieSources = new List<MovieSource>
                    {
                        new MovieSource()
                    }
                },
                new MovieInfo
                {
                    Title = "Терминатор. Возвращение",
                    MovieSources = new List<MovieSource>
                    {
                        new MovieSource()
                    }
                }
            };

            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(t => t.Get(It.IsAny<Expression<Func<MovieInfo, bool>>>())).Returns((Expression<Func<MovieInfo, bool>> where) =>
            {
                return movies.AsQueryable().Where(where);
            });

            var movieManager = new MovieManager(movieRepository.Object);
            var movieListModel = movieManager.GetMovies("гаррИ потТер");

            Assert.AreEqual(2, movieListModel.Movies.Count());
            Assert.IsNotNull(movieListModel.Movies.FirstOrDefault(t => t.Title.IndexOf(movies[0].Title) > -1));
            Assert.IsNotNull(movieListModel.Movies.FirstOrDefault(t => t.Title.IndexOf(movies[1].Title) > -1));

            movieListModel = movieManager.GetMovies("термИнатор");

            Assert.AreEqual(2, movieListModel.Movies.Count());
            Assert.IsNotNull(movieListModel.Movies.FirstOrDefault(t => t.Title.IndexOf(movies[1].Title) > -1));
            Assert.IsNotNull(movieListModel.Movies.FirstOrDefault(t => t.Title.IndexOf(movies[2].Title) > -1));
        }
    }
}
