using System;
using System.Threading.Tasks;
using Movies.Api.Models;

namespace Movies.Api.Data.Repositories.Interfaces
{
  public interface IAccountRepository
  {
    Task Add(Account account);

    Task Update(Account account);

    Task Remove(Guid id);

    Task Save();
  }
}
