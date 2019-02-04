using System;

namespace Movies.Api.Models
{
    /// <summary>
    /// Информация о фильме.
    /// </summary>
    public class MovieFromPost
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор поста.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// Идентификатор группы.
        /// </summary>
        public int FromId { get; set; }

        /// <summary>
        /// Текст поста.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Ссылки на фотографии к посту.
        /// </summary>
        public string Photos { get; set; }

        /// <summary>
        /// Ссылки на видео к посту.
        /// </summary>
        public string Videos { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime UpdateAt { get; set; }
    }
}