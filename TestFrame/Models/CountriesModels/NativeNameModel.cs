using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class NativeNameModel
    {
        [JsonPropertyName("common")]
        public string Common { get; set; }
        //Search by country name. If you want to get an exact match, use the next endpoint. It can be the common or official value

        [JsonPropertyName("official")]
        public string Official { get; set; }
        //Search by country name. If you want to get an exact match, use the next endpoint. It can be the common or official value

    }
}