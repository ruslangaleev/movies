using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Movies.Api.ResourceModels
{
    public class AddMovieInfo
    {
        /// <summary>
        /// Заголовок фильма.
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Ссылка на постер к фильму.
        /// </summary>
        [Required]
        public string UrlPoster { get; set; }

        /// <summary>
        /// Качество.
        /// </summary>
        public MovieQuality Quality { get; set; }

        /// <summary>
        /// Ссылка на фильм.
        /// </summary>
        [Required]
        public string Url { get; set; }
    }
}