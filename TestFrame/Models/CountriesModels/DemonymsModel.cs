using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class DemonymsModel
    {

        [JsonPropertyName("eng")]
        public Eng Eng { get; set; }

        [JsonPropertyName("fra")]
        public Fra Fra { get; set; }

    }
    public class Eng
    {
        public string f { get; set; }
        public string m { get; set; }
    }

    public class Fra
    {
        public string f { get; set; }
        public string m { get; set; }
    }
}
