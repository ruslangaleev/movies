using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movies.Api.Models
{
    /// <summary>
    /// Сырые данные.
    /// </summary>
    public class RawData
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Опубликован ли?
        /// </summary>
        public bool Published { get; set; }

        public int PostId { get; set; }

        public int GroupId { get; set; }

        public string Text { get; set; }

        public string Attachments { get; set; }
    }
}