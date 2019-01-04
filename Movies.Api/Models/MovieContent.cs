using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.Models
{
    public class MovieContent
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Ксчество.
        /// </summary>
        public MovieQuality Quality { get; set; }

        /// <summary>
        /// Коллекция ссылок на фильм (Если вдруг одна из них перестала работать).
        /// </summary>
        public string Url { get; set; }

        public DateTime CreateAt { get; set; }
    }
}