using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class TranslationModel
    {
        [JsonPropertyName("tags")]
        public List<LanguageModel>[] Translations { get; set; }
    }
}
