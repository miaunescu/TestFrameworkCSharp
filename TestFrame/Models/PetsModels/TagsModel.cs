using System.Text.Json.Serialization;

namespace TestFrame.Models.PetsModels
{
    public class TagsModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
