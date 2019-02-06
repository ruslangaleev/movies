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

        private readonly int[] _groupIds = new int[8]
        {
            58170807,
            40335272,
            19802817,
            48319873,
            1444878,
            7142203,
            123915905,
            84875903
        };

        private static int _offset = 0;

        private static bool _isWork = false;

        public Parser(IVkontakteClient vkontakteClient, IMovieFromPostManager movieFromPostManager)
        {
            _vkontakteClient = vkontakteClient ?? throw new ArgumentNullException(nameof(vkontakteClient));
            _movieFromPostManager = movieFromPostManager ?? throw new ArgumentNullException(nameof(movieFromPostManager));
        }
        
        public async Task StartParserNewPosts()
        {
            if (_isWork)
            {
                return;
            }

            _isWork = true;

            foreach (var groupId in _groupIds)
            {
                var result = await _vkontakteClient.GetInfoPostByGroupId(groupId, 10);

                foreach (var entry in result)
                {
                    if (_movieFromPostManager.Get(groupId, entry.id) != null)
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
                        FromId = groupId,
                        PostId = entry.id,
                        Id = Guid.NewGuid(),
                        Photos = string.Join(",", photos),
                        Text = entry.text,
                        Videos = string.Join(",", videos),
                        UpdateAt = DateTime.Now
                    });
                }
            }

            _isWork = false;
        }

        public async Task StartParserAllPosts()
        {
            if (_isWork)
            {
                return;
            }

            _isWork = true;

            foreach (var groupId in _groupIds)
            {
                var result = await _vkontakteClient.GetInfoPostByGroupId(groupId, 100, _offset);

                foreach (var entry in result)
                {
                    if (_movieFromPostManager.Get(groupId, entry.id) != null)
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
                        FromId = groupId,
                        PostId = entry.id,
                        Id = Guid.NewGuid(),
                        Photos = string.Join(",", photos),
                        Text = entry.text,
                        Videos = string.Join(",", videos),
                        UpdateAt = DateTime.Now
                    });
                }
            }

             _offset += 1;

            _isWork = false;
        }
    }
}