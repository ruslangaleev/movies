using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using System;
using System.Web.Http;

namespace Movies.Api.Controllers
{
    [RoutePrefix("api")]
    public class ValuesController : ApiController
    {
        private readonly IMovieManager _movieManager;

        public ValuesController(IMovieManager movieManager)
        {
            _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));
        }

        [HttpPost]
        [Route("movies/new")]
        public object AddMovieInfo([FromBody]AddMovieInfo addMovieInfo)
        {
            _movieManager.AddMovieInfo(addMovieInfo);

            return Ok();
        }

        [HttpPost]
        [Route("movies/newurl")]
        public object AddMovieContent(AddMovieContent content)
        {
            _movieManager.AddMovieContent(content);

            return Ok();
        }

        [HttpGet]
        [Route("movies")]
        public object GetMovies(int page, int pageSize)
        {
            return _movieManager.GetMovies(page, pageSize);
        }
    }
}
