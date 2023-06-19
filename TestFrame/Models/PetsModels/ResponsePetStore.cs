using Newtonsoft.Json;

namespace TestFrame.Models.PetsModels
{
    public class ResponsePetStore
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
