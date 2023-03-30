using System.Text.Json.Serialization;

namespace TestFrame.Models.CatsModels
{
    public class CatsLinksModel
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}
