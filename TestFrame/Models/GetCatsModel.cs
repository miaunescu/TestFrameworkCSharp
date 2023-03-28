using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestFrame.Models
{
    public class GetCatsModel
    {
        [JsonPropertyName("current_page")]
        public int Current_Page { get; set; }
        public List<CatDetailsModel> Data { get; set; }
        public string first_page_url { get; set; }
        [JsonPropertyName("from")]
        public int From { get; set; }
        public int last_page { get; set; }
        public string last_page_url { get; set; }
        public List<CatsLinksModel> Links { get; set; }
        public string next_page_url { get; set; }
        public string path { get; set; }
        public string per_page { get; set; }
        public string prev_page_url { get; set; }
        public int to { get; set; }
        public int total { get; set; }
    }
}
