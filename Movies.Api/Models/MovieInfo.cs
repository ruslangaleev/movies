using System;
using System.Collections;
using System.Collections.Generic;

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
        public IEnumerable<MovieContent> MovieContents { get; set; }

        /// <summary>
        /// Ссылка на постер к фильму.
        /// </summary>
        public string UrlPoster { get; set; }
    }
}