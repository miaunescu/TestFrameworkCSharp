using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CarModel
    {
        [JsonPropertyName("signs")]
        public List<string> Signs { get; set; }

        [JsonPropertyName("side")]
        public string Side { get; set; }

    }
}
