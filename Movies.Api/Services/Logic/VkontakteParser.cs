using System;
using System.Net.Http;
using System.Threading.Tasks;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using Newtonsoft.Json;

namespace Movies.Api.Services.Logic
{
  public class VkontakteParser : IVkontakteParser
  {
    private readonly int _groupId;

    private readonly HttpClient _httpClient;

    public VkontakteParser(int groupId)
    {
      _groupId = groupId;
      _httpClient = new HttpClient();
    }

    public async Task<Item[]> GetInfoPost(int count)
    {
      var token = "f468edbd99bfeabef9c35c6911d9aecf2c957e059d0f5705ef256af379b4832955e137fa83a439e7df34b";
      var request = $"https://api.vk.com/method/wall.get?owner_id=-{_groupId}&count={count}&v=5.92&access_token={token}";
      var response = await _httpClient.GetAsync(request);
      var json = await response.Content.ReadAsStringAsync();
      var infoPost = JsonConvert.DeserializeObject<InfoPost>(json);

      return infoPost.response.items;
    }

    public Task Start()
    {
      throw new NotImplementedException();
    }
  }
}