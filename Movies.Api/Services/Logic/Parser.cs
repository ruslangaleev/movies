using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Movies.Api.Services.Interfaces;

namespace Movies.Api.Services.Logic
{
  public class Parser : IParser
  {
    private readonly IVkontakteClient _vkontakteClient;

    private readonly IAccountManager _accountManager;

    private readonly HttpClient _httpClient;

    private readonly string _token = "b8812d17edb240e4f07adb19ce627fb04111caf26de61e8ced668a6e6e173b107d6b7567d974b9b5a8962";

    private readonly int _groupId = 58170807;

    public Parser(IVkontakteClient vkontakteClient, IAccountManager accountManager)
    {
      _httpClient = new HttpClient();
      _vkontakteClient = vkontakteClient;
      _accountManager = accountManager;
    }

    public async Task Start()
    {
      await Monitoring();
      //Task.Factory.StartNew(Monitoring);
    }

    private async Task Monitoring()
    {
      while(true)
      {
        var result = await _vkontakteClient.GetInfoPost(10);
        var accounts = await _accountManager.Get(Models.Role.Admin);
        
        foreach(var entry in result)
        {
          foreach (var account in accounts)
          {
            var message = $"{entry.id}\n{entry.text}";

            var request = $"https://api.vk.com/method/messages.send?user_id={account.AccountId}&group_id={_groupId}&message={message}&v=5.80&access_token={_token}";

            await _httpClient.GetStringAsync(request);
          }
        }

        Thread.Sleep(600000); // 10 min.
      }
    }
  }
}