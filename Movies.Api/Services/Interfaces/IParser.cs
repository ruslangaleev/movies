using System.Threading.Tasks;

namespace Movies.Api.Services.Interfaces
{
    public interface IParser
    {
        /// <summary>
        /// Запускает парсер только на новые посты.
        /// </summary>
        /// <returns></returns>
        Task StartParserNewPosts();

        /// <summary>
        /// Запускает парсер на все посты.
        /// </summary>
        /// <returns></returns>
        Task StartParserAllPosts();
    }
}
