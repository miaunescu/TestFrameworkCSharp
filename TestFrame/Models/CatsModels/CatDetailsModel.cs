using System.Text.Json.Serialization;

namespace TestFrame.Models.CatsModels
{
    public class CatDetailsModel
    {
        [JsonPropertyName("breed")]
        public string Breed { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("coat")]
        public string Coat { get; set; }

        [JsonPropertyName("pattern")]
        public string Pattern { get; set; }
    }
}
