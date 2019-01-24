using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.Api.Models
{
    public class MovieInfo
    {
        public Guid Id { get; set; }    

        /// <summary>
        /// Заголовок фильма.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Список источников.
        /// </summary>
        public virtual ICollection<MovieSource> MovieSources { get; set; }

        /// <summary>
        /// Ссылка на постер к фильму.
        /// </summary>
        public string UrlPoster { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }

        public MovieInfo()
        {
            MovieSources = new List<MovieSource>();
        }

        public MovieSource GetBestMovieSource()
        {
            var great = MovieSources.FirstOrDefault(t => t.Quality == MovieQuality.Great);
            if (great != null)
            {
                return great;
            }

            var good = MovieSources.FirstOrDefault(t => t.Quality == MovieQuality.Good);
            if (good != null)
            {
                return good;
            }

            var medium = MovieSources.FirstOrDefault(t => t.Quality == MovieQuality.Medium);
            if (medium != null)
            {
                return medium;
            }

            var poor = MovieSources.FirstOrDefault(t => t.Quality == MovieQuality.Poor);
            if (poor != null)
            {
                return poor;
            }

            return new MovieSource
            {
                Quality = MovieQuality.Unknown,
                Url = null
            };
        }
    }
}