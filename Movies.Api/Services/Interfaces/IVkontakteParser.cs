using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Api.Services.Interfaces
{
  public interface IVkontakteParser
  {
    Task<string> GetInfoLastPost();

    Task Start();
  }
}
