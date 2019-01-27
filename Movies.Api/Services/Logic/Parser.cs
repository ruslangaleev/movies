using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Movies.Api.Data.Repositories.Interfaces;
using Movies.Api.Services.Interfaces;

namespace Movies.Api.Services.Logic
{
    public class Parser : IParser
    {
        private readonly IVkontakteClient _vkontakteClient;

        private readonly IAccountManager _accountManager;

        private readonly IRawDataRepository _rawDataRepository;

        private readonly HttpClient _httpClient;

        private readonly string _token = "b8812d17edb240e4f07adb19ce627fb04111caf26de61e8ced668a6e6e173b107d6b7567d974b9b5a8962";

        private readonly int _groupId = 58170807;

        public Parser(IVkontakteClient vkontakteClient, IAccountManager accountManager,
            IRawDataRepository rawDataRepository)
        {
            _httpClient = new HttpClient();
            _vkontakteClient = vkontakteClient;
            _accountManager = accountManager;
            _rawDataRepository = rawDataRepository;
        }

        public async Task Start()
        {
            //await Monitoring();
            Task.Factory.StartNew(Monitoring);
        }

        public async Task ParsingAllPosts()
        {
            Task.Factory.StartNew(StartParsingAllPosts);
        }

        private async Task StartParsingAllPosts()
        {
            int offset = 245;
            int step = 0;
            while (true)
            {
                try
                {
                    var result = await _vkontakteClient.GetInfoPost(100, offset);
                    //var accounts = await _accountManager.Get(Models.Role.Admin);

                    foreach (var entry in result)
                    {
                        step += 1;
                        var rawdata = await _rawDataRepository.Get(_groupId, entry.id);
                        if (rawdata != null)
                        {
                            continue;
                        }
                        //foreach (var account in accounts)
                        //{
                        // https://vk.com/video{owner_id}_{id}
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
                                //attachments += $"\n{attachment.photo.sizes.LastOrDefault()?.url}";
                                attachments.Add($"photo{attachment.photo.owner_id}_{attachment.photo.id}");
                            }
                            if (attachment.type == "video")
                            {
                                //var video = $"https://vk.com/video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}";
                                //attachments += $"\n{attachment.video.title}\n{video}";
                                attachments.Add($"video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}");
                                existVideo = true;
                            }
                        }

                        if (!existVideo)
                        {
                            continue;
                        }

                        var message = $"id: {entry.id}\ntext: {entry.text}";

                        //var request = $"https://api.vk.com/method/messages.send?user_id={account.AccountId}&group_id={_groupId}&attachment={string.Join(",", attachments)}&message={message}&v=5.80&access_token={_token}";

                        //await _httpClient.GetStringAsync(request);

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
                        //}
                    }
                    offset += 1;

                    if (offset == 100000)
                    {
                        await _rawDataRepository.Add(new Models.RawData
                        {
                            Attachments = "конец",
                            Text = "конец",
                            GroupId = 0,
                            PostId = 0,
                            Id = Guid.NewGuid(),
                            CreateAt = DateTime.Now,
                            UpdateAt = DateTime.Now
                        });
                        await _rawDataRepository.Save();

                        break;
                    }
                }
                catch(Exception e)
                {
                    var s = e;
                }

                //Thread.Sleep(60000); // 10 min. 600000
            }
        }

        private async Task Monitoring()
        {
            while (true)
            {
                var result = await _vkontakteClient.GetInfoPost(10);
                //var accounts = await _accountManager.Get(Models.Role.Admin);

                foreach (var entry in result)
                {
                    var rawdata = await _rawDataRepository.Get(_groupId, entry.id);
                    if (rawdata != null)
                    {
                        continue;
                    }
                    //foreach (var account in accounts)
                    //{
                    // https://vk.com/video{owner_id}_{id}
                    var attachments = new List<string>();
                    var existVideo = false;
                    foreach (var attachment in entry.attachments)
                    {
                        if (attachment.type == "photo")
                        {
                            //attachments += $"\n{attachment.photo.sizes.LastOrDefault()?.url}";
                            attachments.Add($"photo{attachment.photo.owner_id}_{attachment.photo.id}");
                        }
                        if (attachment.type == "video")
                        {
                            //var video = $"https://vk.com/video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}";
                            //attachments += $"\n{attachment.video.title}\n{video}";
                            attachments.Add($"video{attachment.video.owner_id}_{attachment.video.id}_{attachment.video.access_key}");
                            existVideo = true;
                        }
                    }

                    if (!existVideo)
                    {
                        continue;
                    }

                    var message = $"id: {entry.id}\ntext: {entry.text}";

                    //var request = $"https://api.vk.com/method/messages.send?user_id={account.AccountId}&group_id={_groupId}&attachment={string.Join(",", attachments)}&message={message}&v=5.80&access_token={_token}";

                    //await _httpClient.GetStringAsync(request);

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
                    //}
                }

                Thread.Sleep(600000); // 10 min. 600000
            }
        }
    }
}