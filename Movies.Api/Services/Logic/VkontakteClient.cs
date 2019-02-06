using System.Net.Http;
using System.Threading.Tasks;
using Movies.Api.ResourceModels;
using Movies.Api.Services.Interfaces;
using Newtonsoft.Json;

namespace Movies.Api.Services.Logic
{
    public class VkontakteClient : IVkontakteClient
    {
        private readonly int _groupId;

        private readonly HttpClient _httpClient;

        private readonly string _token = "b8812d17edb240e4f07adb19ce627fb04111caf26de61e8ced668a6e6e173b107d6b7567d974b9b5a8962";

        public VkontakteClient()
        {
            _groupId = 58170807;
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

        public async Task<Item[]> GetInfoPost(int count, int offset)
        {
            var token = "f468edbd99bfeabef9c35c6911d9aecf2c957e059d0f5705ef256af379b4832955e137fa83a439e7df34b";
            var request = $"https://api.vk.com/method/wall.get?owner_id=-{_groupId}&count={count}&offset={offset}&v=5.92&access_token={token}";
            var response = await _httpClient.GetAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var infoPost = JsonConvert.DeserializeObject<InfoPost>(json);

            return infoPost.response.items;
        }

        public async Task SendMessageWithKeyboard(int userId, int groupId, string message, string keyboard)
        {
            var request = $"https://api.vk.com/method/messages.send?user_id={userId}&group_id={groupId}&message={message}&keyboard={keyboard}&v=5.80&access_token={_token}";

            var result = await _httpClient.GetAsync(request);
            var content = await result.Content.ReadAsStringAsync();
        }

        public async Task SendMessageWithAttachment(int userId, int groupId, string message, string attachment)
        {
            var request = $"https://api.vk.com/method/messages.send?user_id={userId}&group_id={groupId}&message={message}&attachment={attachment}&v=5.80&access_token={_token}";

            await _httpClient.GetAsync(request);
        }

        public async Task SendMessage(int userId, int groupId, string message)
        {
            var request = $"https://api.vk.com/method/messages.send?user_id={userId}&group_id={groupId}&message={message}&v=5.80&access_token={_token}";

            await _httpClient.GetAsync(request);
        }

        public async Task<Item[]> GetInfoPostByGroupId(int groupId, int count)
        {
            var token = "f468edbd99bfeabef9c35c6911d9aecf2c957e059d0f5705ef256af379b4832955e137fa83a439e7df34b";
            var request = $"https://api.vk.com/method/wall.get?owner_id=-{groupId}&count={count}&v=5.92&access_token={token}";
            var response = await _httpClient.GetAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var infoPost = JsonConvert.DeserializeObject<InfoPost>(json);

            return infoPost.response.items;
        }

        public async Task<Item[]> GetInfoPostByGroupId(int groupId, int count, int offset)
        {
            var token = "f468edbd99bfeabef9c35c6911d9aecf2c957e059d0f5705ef256af379b4832955e137fa83a439e7df34b";
            var request = $"https://api.vk.com/method/wall.get?owner_id=-{groupId}&count={count}&offset={offset}&v=5.92&access_token={token}";
            var response = await _httpClient.GetAsync(request);
            var json = await response.Content.ReadAsStringAsync();
            var infoPost = JsonConvert.DeserializeObject<InfoPost>(json);

            return infoPost.response.items;
        }
    }
}