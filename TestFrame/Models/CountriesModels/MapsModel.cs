using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class MapsModel
    {
        [JsonPropertyName("googleMaps")]
        public string GoogleMaps { get; set; }

        [JsonPropertyName("openStreetMaps")]
        public string OpenStreetMaps { get; set; }

    }
}
