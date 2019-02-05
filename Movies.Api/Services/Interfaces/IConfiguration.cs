namespace Movies.Api.Services.Interfaces
{
  public interface IConfiguration
  {
    T Read<T>(string param) where T : struct;

    void Write(string param, string value);
  }
}
