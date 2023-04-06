using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class IddModel
    {
        [JsonPropertyName("root")]
        public string Root { get; set; }

        [JsonPropertyName("suffixes")]
        public List<string> Suffixes { get; set; }
    }
}
