using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.ResourceModels
{
    public class AddMovieSource
    {
        /// <summary>
        /// Идентификатор фильма.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Качество.
        /// </summary>
        public MovieQuality Quality { get; set; }

        /// <summary>
        /// Новая ссылка на фильм.
        /// </summary>
        public string Url { get; set; }
    }
}