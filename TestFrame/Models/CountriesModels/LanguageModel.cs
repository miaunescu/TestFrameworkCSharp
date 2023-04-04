using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class LanguageModel
    {
        [JsonPropertyName("official")]
        public string Official { get; set; }

        [JsonPropertyName("common")]
        public string Common { get; set; }
    }
}