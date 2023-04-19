using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class FlagsModel
    {

        [JsonPropertyName("png")]
        public string Png { get; set; }

        [JsonPropertyName("svg")]
        public string Svg { get; set; }

        [JsonPropertyName("alt")]
        public string Alt { get; set; }

    }
}