using Newtonsoft.Json;

namespace TestFrame.Models.PetsModels
{
    public class Order
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("petId")]
        public Int64 PetId { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("shipDate")]
        public DateTime ShipDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }
    }
}
