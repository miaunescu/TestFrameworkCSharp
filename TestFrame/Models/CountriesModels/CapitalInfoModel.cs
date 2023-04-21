using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CapitalInfoModel
    {
        [JsonPropertyName("latlng")]
        public List<double> Latlang { get; set; }

    }
}
