using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Movies.Api.Models;

namespace Movies.Api.Services.Interfaces
{
  public interface IAccountManager
  {
    Task Add(Account account);

    Task Update(Account account);

    Task Remove(Guid id);

    Task<IEnumerable<Account>> Get(Role role);
  }
}