using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Movies.Api.Controllers
{
  [RoutePrefix("api")]
  public class BotController : ApiController
  {
    private static Dictionary<int, List<string>> _cache = new Dictionary<int, List<string>>();

    private readonly IMovieFromPostManager _movieFromPostManager;

    private readonly string _secret = "qwe123rty456";

    private readonly IVkontakteClient _vkontakteClient;

    public BotController(IMovieFromPostManager movieFromPostManager, IVkontakteClient vkontakteClient)
    {
      _movieFromPostManager = movieFromPostManager ?? throw new ArgumentNullException(nameof(movieFromPostManager));
      _vkontakteClient = vkontakteClient ?? throw new ArgumentNullException(nameof(vkontakteClient));
    }

    [HttpPost]
    [Route("bot")]
    public async Task<object> Post([FromBody]Message message)
    {
      var list = default(List<string>);
      if (!_cache.TryGetValue(message.ObjectMessage.UserId, out list))
      {
        _cache.Add(message.ObjectMessage.UserId, new List<string>());
      }
      else
      if (list.FirstOrDefault(t => t == message.ObjectMessage.Body) != null)
      {
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
        return response;
      }

      if (message.Type == "confirmation")
      {
        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
          Content = new StringContent("31897f71", Encoding.UTF8, "text/plain")
        };
        return response;
      }

      if (message.Secret != _secret)
      {
        return BadRequest("Не верный секретный ключ.");
      }

      if (message.Type == "message_new")
      {
        if (message.ObjectMessage.Body.ToLower() == "Случайный фильм")
        {
          var random = _movieFromPostManager.GetRandom();

          await _vkontakteClient.SendMessageWithAttachment(message.ObjectMessage.UserId, message.GroupId, random.Text, $"{random.Photos},{random.Videos}");
          var response1 = new HttpResponseMessage(HttpStatusCode.OK);
          response1.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
          return response1;
        }

        var items = _movieFromPostManager.Get(message.ObjectMessage.Body);

        if (items.Movies.Count() == 0)
        {
          _cache[message.ObjectMessage.UserId] = new List<string>();
          var l1 = _cache[message.ObjectMessage.UserId];
          l1.Add(message.ObjectMessage.Body.ToLower());

          await _vkontakteClient.SendMessage(message.ObjectMessage.UserId, message.GroupId, "Фильм не найден");
          var response1 = new HttpResponseMessage(HttpStatusCode.OK);
          response1.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
          return response1;
        }

        foreach (var entry in items.Movies)
        {
          var attachment = $"{entry.Photos},{entry.Videos}";
          await _vkontakteClient.SendMessageWithAttachment(message.ObjectMessage.UserId, message.GroupId, entry.Text, attachment);
        }

        _cache[message.ObjectMessage.UserId] = new List<string>();
        var l = _cache[message.ObjectMessage.UserId];
        l.Add(message.ObjectMessage.Body.ToLower());

        var response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
        return response;
      }

      return BadRequest("Тип события не распознан.");
    }
  }
}