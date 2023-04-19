using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class TranslationModel
    {
        [JsonPropertyName("tags")]
        public List<string>[] Translations { get; set; }
    }
}
