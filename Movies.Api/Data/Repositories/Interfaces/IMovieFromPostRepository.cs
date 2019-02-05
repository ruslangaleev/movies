using Movies.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Movies.Api.Data.Repositories.Interfaces
{
  /// <summary>
  /// Репозиторий для фильмов из постов.
  /// </summary>
  public interface IMovieFromPostRepository
  {
    /// <summary>
    /// Добавляет информацию о посте, содержащий ссылку на фильм.
    /// </summary>
    /// <param name="movieFromPost">Информация о фильме.</param>
    void Add(MovieFromPost movieFromPost);

    /// <summary>
    /// Вернет отфильтрованный список.
    /// </summary>
    /// <param name="predicate">Условие фильтра.</param>
    IEnumerable<MovieFromPost> Get(Expression<Func<MovieFromPost, bool>> predicate);

    IEnumerable<MovieFromPost> GetAll();

    /// <summary>
    /// Сохраняет изменения.
    /// </summary>
    Task SaveAsync();
  }
}
