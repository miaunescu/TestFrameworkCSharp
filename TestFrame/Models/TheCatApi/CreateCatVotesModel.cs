using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFrame.Models.TheCatApi
{
    public class CreateCatVotesModel
    {
        [JsonPropertyName("image_id")]
        public string ImageId { get; set; }

        [JsonPropertyName("sub_id")]
        public string SubId { get; set; }

        public int Value { get; set; }
    }
}
