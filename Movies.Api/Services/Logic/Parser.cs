using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Services.Interfaces;

namespace Movies.Api.Services.Logic
{
    public class Parser : IParser
    {
        private readonly IVkontakteClient _vkontakteClient;

        private readonly IRawDataRepository _rawDataRepository;

        private readonly string _token = "b8812d17edb240e4f07adb19ce627fb04111caf26de61e8ced668a6e6e173b107d6b7567d974b9b5a8962";

        private readonly int _groupId = 58170807;

        public Parser(IVkontakteClient vkontakteClient, IRawDataRepository rawDataRepository)
        {
            _vkontakteClient = vkontakteClient;
            _rawDataRepository = rawDataRepository;
        }
        
        public async Task StartParserNewPosts()
        {
            var result = await _vkontakteClient.GetInfoPost(10);

            foreach (var entry in result)
            {
                var rawdata = await _rawDataRepository.Get(_groupId, entry.id);
                // Проверяем, если в базе уже такая запись есть, то пропускаем обработку.
                if (rawdata != null)
                {
                    continue;
                }

                var attachments = new List<string>();
                var existVideo = false;
                foreach (var attachment in entry.attachments)
                {
                    if (attachment.type == "photo")
                    {
                        attachments.Add($"photo{attachment.photo.owner_id}_{attachment.photo.id}");
                    }
                    if (attachment.type == "video")
                    {
                        attachments.Add($"video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}");
                        existVideo = true;
                    }
                }

                // Не будем сохранять те посты, у которых не видео
                if (!existVideo)
                {
                    continue;
                }

                await _rawDataRepository.Add(new Models.RawData
                {
                    Attachments = string.Join(",", attachments),
                    Text = entry.text,
                    GroupId = _groupId,
                    PostId = entry.id,
                    Id = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                });
                await _rawDataRepository.Save();
            }
        }

        public async Task StartParserAllPosts()
        {
            var appSettingsClient = new AppSettingsClient();
            var appSettings = appSettingsClient.Get();

            var result = await _vkontakteClient.GetInfoPost(100, appSettings.Parser.Offset);

            foreach (var entry in result)
            {
                var rawdata = await _rawDataRepository.Get(_groupId, entry.id);
                if (rawdata != null)
                {
                    continue;
                }

                var attachments = new List<string>();
                var existVideo = false;
                if (entry.attachments == null)
                {
                    continue;
                }
                foreach (var attachment in entry.attachments)
                {
                    if (attachment.type == "photo")
                    {
                        attachments.Add($"photo{attachment.photo.owner_id}_{attachment.photo.id}");
                    }
                    if (attachment.type == "video")
                    {
                        attachments.Add($"video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}");
                        existVideo = true;
                    }
                }

                if (!existVideo)
                {
                    continue;
                }

                await _rawDataRepository.Add(new Models.RawData
                {
                    Attachments = string.Join(",", attachments),
                    Text = entry.text,
                    GroupId = _groupId,
                    PostId = entry.id,
                    Id = Guid.NewGuid(),
                    CreateAt = DateTime.Now,
                    UpdateAt = DateTime.Now
                });
                await _rawDataRepository.Save();
            }

            appSettings.Parser.Offset += 1;
            appSettingsClient.Set(appSettings);
        }
    }
}