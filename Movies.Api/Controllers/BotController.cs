using Movies.Api.Models;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using Newtonsoft.Json;
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
    public enum Operation
    {
        NewMovie,
        FindMovie
    }

    [RoutePrefix("api")]
    public class BotController : ApiController
    {
        private readonly IMovieManager _movieManager;

        private readonly string _secret = "qwe123rty456";

        private readonly string _token = "b8812d17edb240e4f07adb19ce627fb04111caf26de61e8ced668a6e6e173b107d6b7567d974b9b5a8962";

        private readonly static HttpClient _httpClient = new HttpClient();

        private static Operation operation;

        //private readonly static List<AddMovieInfo> addMovieInfos = new List<AddMovieInfo>();

        public BotController(IMovieManager movieManager)
        {
            _movieManager = movieManager ?? throw new ArgumentNullException(nameof(movieManager));
        }
        
        [HttpPost]
        [Route("bot")]
        public async Task<object> Post([FromBody]Message message)
        {
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
                if (message.ObjectMessage.Body.ToLower() == "старт")
                {
                    string responseMessage = "Привет, я бот. Чем могу помочь?";

                    var buttons = new List<Button>
                    {
                        new Button
                        {
                            color = "default",
                            action = new ResourceModels.Action
                            {
                                label = "Добавить фильм",
                                type = "text",
                                payload = JsonConvert.SerializeObject(new
                                {
                                    button = "1"
                                })
                            }
                        },
                        new Button
                        {
                            color = "default",
                            action = new ResourceModels.Action
                            {
                                label = "Найти фильм",
                                type = "text",
                                payload = JsonConvert.SerializeObject(new
                                {
                                    button = "1"
                                })
                            }
                        }
                    }.ToArray();

                    var keyboard = new ResponseMessage
                    {
                        OneTime = false,
                        buttons = new[] { buttons }
                    };

                    var json = JsonConvert.SerializeObject(keyboard);

                    var request = $"https://api.vk.com/method/messages.send?user_id={message.ObjectMessage.UserId}&group_id={message.GroupId}&message={responseMessage}&keyboard={json}&v=5.80&access_token={_token}";

                    await _httpClient.GetAsync(request);

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
                    return response;
                }

                if (message.ObjectMessage.Body.ToLower() == "добавить фильм")
                {
                    operation = Operation.NewMovie;

                    string responseMessage = "Введите название фильма" +
                        "\r\nУкажите ссылку на постер фильма" +
                        "\r\nУкажите качество фильма(0 - плохое, 1 - среднее, 2 - хорошее, 3 - отличное)" +
                        "\r\nУкажите ссылку на фильм";

                    var buttons = new List<Button>
                    {
                        new Button
                        {
                            color = "default",
                            action = new ResourceModels.Action
                            {
                                label = "Назад",
                                type = "text",
                                payload = JsonConvert.SerializeObject(new
                                {
                                    button = "1"
                                })
                            }
                        }
                    }.ToArray();

                    var keyboard = new ResponseMessage
                    {
                        OneTime = false,
                        buttons = new[] { buttons }
                    };

                    var json = JsonConvert.SerializeObject(keyboard);

                    var request = $"https://api.vk.com/method/messages.send?user_id={message.ObjectMessage.UserId}&group_id={message.GroupId}&message={responseMessage}&keyboard={json}&v=5.80&access_token={_token}";

                    await _httpClient.GetAsync(request);

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
                    return response;
                }

                if (message.ObjectMessage.Body.ToLower() == "найти фильм")
                {
                    operation = Operation.FindMovie;

                    string responseMessage = "Введите название фильма";

                    var buttons = new List<Button>
                    {
                        new Button
                        {
                            color = "default",
                            action = new ResourceModels.Action
                            {
                                label = "Назад",
                                type = "text",
                                payload = JsonConvert.SerializeObject(new
                                {
                                    button = "1"
                                })
                            }
                        }
                    }.ToArray();

                    var keyboard = new ResponseMessage
                    {
                        OneTime = false,
                        buttons = new[] { buttons }
                    };

                    var json = JsonConvert.SerializeObject(keyboard);

                    var request = $"https://api.vk.com/method/messages.send?user_id={message.ObjectMessage.UserId}&group_id={message.GroupId}&message={responseMessage}&keyboard={json}&v=5.80&access_token={_token}";

                    await _httpClient.GetAsync(request);

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
                    return response;
                }

                if (operation == Operation.NewMovie)
                {
                    String[] words = message.ObjectMessage.Body.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    var quality = (MovieQuality)Enum.ToObject(typeof(MovieQuality), Int32.Parse(words[2]));

                    _movieManager.AddMovieInfo(new AddMovieInfo
                    {
                        Title = words[0],
                        UrlPoster = words[1],
                        Quality = quality,
                        Url = words[3]
                    });

                    string responseMessage = "Фильм успешно добавлен";

                    var buttons = new List<Button>
                    {
                        new Button
                        {
                            color = "default",
                            action = new ResourceModels.Action
                            {
                                label = "Добавить фильм",
                                type = "text",
                                payload = JsonConvert.SerializeObject(new
                                {
                                    button = "1"
                                })
                            }
                        }
                    }.ToArray();

                    var keyboard = new ResponseMessage
                    {
                        OneTime = false,
                        buttons = new[] { buttons }
                    };

                    var json = JsonConvert.SerializeObject(keyboard);

                    var request = $"https://api.vk.com/method/messages.send?user_id={message.ObjectMessage.UserId}&group_id={message.GroupId}&message={responseMessage}&keyboard={json}&v=5.80&access_token={_token}";

                    await _httpClient.GetAsync(request);

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
                    return response;
                }

                if (operation == Operation.FindMovie)
                {
                    //String[] words = message.ObjectMessage.Body.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

                    //var quality = (MovieQuality)Enum.ToObject(typeof(MovieQuality), Int32.Parse(words[2]));

                    var movieListModel = _movieManager.GetMovies(message.ObjectMessage.Body);

                    string responseMessage = null;
                    if (movieListModel.Movies.Count() > 0)
                    {
                        var movie = movieListModel.Movies.First();
                        responseMessage = $"{movie.Title}\n" +
                            $"{movie.Quality}\n" +
                            $"{movie.UrlPoster}\n" +
                            $"{movie.Url}";
                    }

                    //var buttons = new List<Button>
                    //{
                    //    new Button
                    //    {
                    //        color = "default",
                    //        action = new ResourceModels.Action
                    //        {
                    //            label = "Добавить фильм",
                    //            type = "text",
                    //            payload = JsonConvert.SerializeObject(new
                    //            {
                    //                button = "1"
                    //            })
                    //        }
                    //    }
                    //}.ToArray();

                    //var keyboard = new ResponseMessage
                    //{
                    //    OneTime = false,
                    //    buttons = new[] { buttons }
                    //};

                    //var json = JsonConvert.SerializeObject(keyboard);

                    var request = $"https://api.vk.com/method/messages.send?user_id={message.ObjectMessage.UserId}&group_id={message.GroupId}&message={responseMessage}&v=5.80&access_token={_token}";

                    await _httpClient.GetAsync(request);

                    var response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent("ok", Encoding.UTF8, "text/plain");
                    return response;
                }
            }

            return BadRequest("Тип события не распознан.");
        }

        //[HttpPost]
        //[Route("movies/new")]
        //public object AddMovieInfo([FromBody]AddMovieInfo addMovieInfo)
        //{
        //    _movieManager.AddMovieInfo(addMovieInfo);

        //    return Ok();
        //}

        //[HttpPost]
        //[Route("movies/newurl")]
        //public object AddMovieContent(AddMovieSource content)
        //{
        //    _movieManager.AddMovieSource(content);

        //    return Ok();
        //}
    }
}