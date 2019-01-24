using Newtonsoft.Json;

namespace Movies.Api.ResourceModels
{
    public class Message
    {
        public string Type { get; set; }

        [JsonProperty("group_id")]
        public int GroupId { get; set; }

        [JsonProperty("object")]
        public ObjectMessage ObjectMessage { get; set; }

        public string Secret { get; set; }
    }

    public class ObjectMessage
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        public string Body { get; set; }
    }
}