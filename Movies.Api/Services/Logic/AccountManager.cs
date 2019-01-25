using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;
using Movies.Api.Services.Interfaces;

namespace Movies.Api.Services.Logic
{
  public class AccountManager : IAccountManager
  {
    private readonly IAccountRepository _accountRepository;

    public AccountManager(IAccountRepository accountRepository)
    {
      _accountRepository = accountRepository;
    }

    public async Task Add(Account account)
    {
      await _accountRepository.Add(account);

      await _accountRepository.Save();
    }

    public Task Remove(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task Update(Account account)
    {
      throw new NotImplementedException();
    }
  }
}