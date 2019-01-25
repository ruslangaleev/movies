using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Movies.Api.ResourceModels;

namespace Movies.Api.Services.Interfaces
{
  public interface IVkontakteParser
  {
    Task<Item[]> GetInfoPost(int count);

    Task Start();
  }
}
