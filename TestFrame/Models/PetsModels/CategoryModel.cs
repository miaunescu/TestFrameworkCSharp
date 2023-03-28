using System.Text.Json.Serialization;

namespace TestFrame.Models.PetsModels
{
    public class CategoryModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
