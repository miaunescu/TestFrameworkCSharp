using System.Text.Json.Serialization;

namespace TestFrame.Models.CatsModels
{
    public class GetCatsModel
    {
        [JsonPropertyName("current_page")]
        public int Current_Page { get; set; }

        [JsonPropertyName("data")]
        public List<CatDetailsModel> Data { get; set; }

        [JsonPropertyName("first_page_url")]
        public string FirstPageUrl { get; set; }

        [JsonPropertyName("from")]
        public int From { get; set; }

        [JsonPropertyName("last_page")]
        public int LastPage { get; set; }

        [JsonPropertyName("last_page_url")]
        public string LastPageUrl { get; set; }

        [JsonPropertyName("links")]
        public List<CatsLinksModel> Links { get; set; }

        [JsonPropertyName("next_page_url")]
        public string NextPageUrl { get; set; }

        [JsonPropertyName("Path")]
        public string Path { get; set; }

        [JsonPropertyName("per_page")]
        public string PerPage { get; set; }

        [JsonPropertyName("prev_page_url")]
        public string PrevPageUrl { get; set; }

        [JsonPropertyName("to")]
        public int To { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}
