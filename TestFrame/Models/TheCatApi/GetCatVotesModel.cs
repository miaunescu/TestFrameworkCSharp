using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFrame.Models.TheCatApi
{
    public class GetCatVotesModel
    {
        public int Id { get; set; }
        [JsonPropertyName("image_id")]
        public string ImageId { get; set; }

        [JsonPropertyName("sub_id")]
        public string SubId { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        public int Value { get; set; }
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

       // public string Image { get; set; }
    }
}
