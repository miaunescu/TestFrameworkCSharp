using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class TranslationModel
    {
        [JsonExtensionData]
        public JsonObject Translation { get; set; }
    }
}
