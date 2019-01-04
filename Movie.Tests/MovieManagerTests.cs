using Moq;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Logic;
using NUnit.Framework;
using System;
using System.Linq;

namespace Movie.Tests
{
    [TestFixture]
    public class MovieManagerTests
    {
        [Test]
        public void Test()
        {
            MovieInfo movieInfo = default(MovieInfo);

            var movieRepository = new Mock<IMovieRepository>();
            movieRepository.Setup(t => t.Add(It.IsAny<MovieInfo>())).Callback((MovieInfo movie) =>
            {
                movieInfo = movie;
            });
            var movieManager = new MovieManager(movieRepository.Object);
            var movieInfoModel = new AddMovieInfo
            {
                Quality = Movies.Api.Models.MovieQuality.Poor,
                Title = "Название фильма",
                Url = "Ссылка на фильм",
                UrlPoster = "Ссылка на постер"
            };

            movieManager.AddMovieInfo(movieInfoModel);

            Assert.AreEqual(movieInfoModel.Title, movieInfo.Title);
            Assert.AreEqual(movieInfoModel.UrlPoster, movieInfo.UrlPoster);
            Assert.AreEqual(movieInfoModel.Url, movieInfo.MovieContents.First().Url);
            Assert.AreEqual(movieInfoModel.Quality, movieInfo.MovieContents.First().Quality);
        }
    }
}
