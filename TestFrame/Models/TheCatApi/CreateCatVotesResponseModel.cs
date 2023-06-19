using System.Text.Json.Serialization;

namespace TestFrame.Models.TheCatApi
{
    public class CreateCatVotesResponseModel
    {
        public string Message { get; set; }
        public int Id { get; set; }

        [JsonPropertyName("image_id")]
        public string ImageId { get; set; }

        [JsonPropertyName("sub_id")]
        public string SubId { get; set; }
        public int Value { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}
