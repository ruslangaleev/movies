using System;
using System.Collections;
using System.Collections.Generic;
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

    Task<IEnumerable<Account>> Get(Role role);
  }
}
