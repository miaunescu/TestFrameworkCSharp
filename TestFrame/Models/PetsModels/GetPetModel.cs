using System.Text.Json.Serialization;

namespace TestFrame.Models.PetsModels
{
    public class GetPetModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("category")]
        public CategoryModel Category { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("photoUrls")]
        public string[] PhotoUrls { get; set; }

        [JsonPropertyName("tags")]
        public List<TagsModel>[] Tags { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }
}
