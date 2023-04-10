using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CoatOfArmsModel
    {
        [JsonPropertyName("png")]
        public string Png { get; set; }

        [JsonPropertyName("svg")]
        public string Svg { get; set; }

    }
}
