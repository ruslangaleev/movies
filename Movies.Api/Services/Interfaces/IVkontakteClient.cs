using System.Threading.Tasks;
using Movies.Api.ResourceModels;

namespace Movies.Api.Services.Interfaces
{
    public interface IVkontakteClient
    {
        Task<Item[]> GetInfoPost(int count);

        Task<Item[]> GetInfoPost(int count, int offset);

        Task SendMessageWithKeyboard(int userId, int groupId, string message, string keyboard);

        Task SendMessageWithAttachment(int userId, int groupId, string message, string attachment);

        Task SendMessage(int userId, int groupId, string message);
    }
}
