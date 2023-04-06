using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CapitalInfoModel
    {
        [JsonPropertyName("latlang")]
        public List<float> Latlang { get; set; }

    }
}
