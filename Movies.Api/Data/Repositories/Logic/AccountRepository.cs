using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Models;

namespace Movies.Api.Data.Repositories.Logic
{
  public class AccountRepository : IAccountRepository
  {
    private DbContext _dbContext;

    private readonly DbSet<Account> _accounts;

    public AccountRepository(DbContext dbContext)
    {
      _accounts = dbContext.Set<Account>();

      _dbContext = dbContext;
    }

    public async Task Add(Account account)
    {
      _accounts.Add(account);
    }

    public async Task Remove(Guid id)
    {
      var account = await _accounts.FindAsync(id);
      if (account != null)
      {
        _accounts.Remove(account);
      }
    }

    public Task Update(Account account)
    {
      throw new NotImplementedException();
    }

    public async Task Save()
    {
      await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Account>> Get(Role role)
    {
      return await _accounts.Where(t => t.Role == role).ToListAsync();
    }
  }
}