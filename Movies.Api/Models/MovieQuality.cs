namespace Movies.Api.Models
{
    /// <summary>
    /// Качество кино.
    /// </summary>
    public enum MovieQuality
    {
        /// <summary>
        /// Плохое. Обычно это TS или CAmRip, т.е. там где плохое видео и звук.
        /// </summary>
        Poor = 0,

        /// <summary>
        /// Среднее. Тут либо плохой звук, либо плохое видео.
        /// </summary>
        Medium,

        /// <summary>
        /// Хорошее. Отличный звук и отличное видео, но с рекламой.
        /// </summary>
        Good,

        /// <summary>
        /// Отличное. Отличный звук и отличное видео
        /// </summary>
        Great,

        /// <summary>
        /// Неизвестно.
        /// </summary>
        Unknown
    }
}