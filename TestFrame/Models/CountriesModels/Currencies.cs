using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class Currencies
    {
        [JsonExtensionData]
        public JsonObject Currency { get; set; }
    }
}