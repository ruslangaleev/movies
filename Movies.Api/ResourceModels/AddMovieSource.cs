using Movies.Api.Models;
using System;

namespace Movies.Api.ResourceModels
{
    /// <summary>
    /// Информация о новом источнике фильма.
    /// </summary>
    public class AddMovieSource
    {
        /// <summary>
        /// Идентификатор фильма из базы.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Качество фильма.
        /// </summary>
        public MovieQuality Quality { get; set; }

        /// <summary>
        /// Новая ссылка на фильм.
        /// </summary>
        public string Url { get; set; }
    }
}