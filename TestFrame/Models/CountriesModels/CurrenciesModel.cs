using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CurrenciesModel
    {
        [JsonExtensionData]
        public JsonObject Currency { get; set; }
    }
}