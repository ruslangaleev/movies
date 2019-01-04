using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.ResourceModels
{
    public class AddMovieInfo : AddMovieContent
    {
        /// <summary>
        /// Заголовок фильма.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на постер к фильму.
        /// </summary>
        public string UrlPoster { get; set; }
    }
}