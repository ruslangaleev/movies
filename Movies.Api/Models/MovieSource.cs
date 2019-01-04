using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.Models
{
    /// <summary>
    /// Ссылка на фильм.
    /// Если вдруг одна из ссылок перестает работать, берется следующая.
    /// Или когда появилось новое качество фильма.
    /// </summary>
    public class MovieSource
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Качество.
        /// </summary>
        public MovieQuality Quality { get; set; }

        /// <summary>
        /// Ссылка на фильм.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Дата добавления ссылки.
        /// </summary>
        public DateTime CreateAt { get; set; }
    }
}