using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Api.Services.Interfaces;

namespace Movies.Api.Services.Logic
{
    // TODO: 
    // 1.Сохранение текущего offset

    public class Parser : IParser
    {
        private readonly IVkontakteClient _vkontakteClient;

        private readonly IMovieFromPostManager _movieFromPostManager;

        private readonly int _groupId = 58170807;

        private static int _offset = 1;

        public Parser(IVkontakteClient vkontakteClient, IMovieFromPostManager movieFromPostManager)
        {
            _vkontakteClient = vkontakteClient ?? throw new ArgumentNullException(nameof(vkontakteClient));
            _movieFromPostManager = movieFromPostManager ?? throw new ArgumentNullException(nameof(movieFromPostManager));
        }
        
        public async Task StartParserNewPosts()
        {
            var result = await _vkontakteClient.GetInfoPost(10);

            foreach (var entry in result)
            {
                if (_movieFromPostManager.Get(_groupId, entry.id) != null)
                {
                    continue;
                }

                List<string> photos = new List<string>();
                List<string> videos = new List<string>();
                foreach(var attachment in entry.attachments)
                {
                    if (attachment.type == "photo")
                    {
                        photos.Add($"photo{attachment.photo.owner_id}_{attachment.photo.id}");
                    }
                    if (attachment.type == "video")
                    {
                        videos.Add($"video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}");
                    }
                }
                
                if (videos.Count() == 0)
                {
                    continue;
                }

                await _movieFromPostManager.Add(new Models.MovieFromPost
                {
                    CreateAt = DateTime.Now,
                    FromId = _groupId,
                    PostId = entry.id,
                    Id = Guid.NewGuid(),
                    Photos = string.Join(",", photos),
                    Text = entry.text,
                    Videos = string.Join(",", videos),
                    UpdateAt = DateTime.Now
                });
            }
        }

        public async Task StartParserAllPosts()
        {
            //int offset = 1;
            var result = await _vkontakteClient.GetInfoPost(100, _offset);

            foreach (var entry in result)
            {
                if (_movieFromPostManager.Get(_groupId, entry.id) != null)
                {
                    continue;
                }

                if (entry.attachments == null)
                {
                    continue;
                }

                List<string> photos = new List<string>();
                List<string> videos = new List<string>();
                foreach (var attachment in entry.attachments)
                {
                    if (attachment.type == "photo")
                    {
                        photos.Add($"photo{attachment.photo.owner_id}_{attachment.photo.id}");
                    }
                    if (attachment.type == "video")
                    {
                        videos.Add($"video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}");
                    }
                }

                if (videos.Count() == 0)
                {
                    continue;
                }

                await _movieFromPostManager.Add(new Models.MovieFromPost
                {
                    CreateAt = DateTime.Now,
                    FromId = _groupId,
                    PostId = entry.id,
                    Id = Guid.NewGuid(),
                    Photos = string.Join(",", photos),
                    Text = entry.text,
                    Videos = string.Join(",", videos),
                    UpdateAt = DateTime.Now
                });
            }

             _offset += 1;
        }
    }
}