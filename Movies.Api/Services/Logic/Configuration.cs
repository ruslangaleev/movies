using System;
using System.Web.Configuration;
using Movies.Api.Services.Interfaces;

namespace Movies.Api.Services.Logic
{
  public class Configuration : IConfiguration
  {
    private readonly System.Configuration.Configuration _configuration = WebConfigurationManager.OpenWebConfiguration("~");

    public T Read<T>(string param) where T : struct
    {
      var valueToString = _configuration.AppSettings.Settings[param].Value;
      return (T)Convert.ChangeType(valueToString, typeof(T));
    }

    public void Write(string param, string value)
    {
      _configuration.AppSettings.Settings[param].Value = value;
      _configuration.Save();
    }
  }
}