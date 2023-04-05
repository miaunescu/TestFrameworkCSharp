using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CountryModel
    {
        [JsonPropertyName("name")]
        public NameModel Name { get; set; }
        //Search by country name. If you want to get an exact match, use the next endpoint. It can be the common or official value


        //[JsonPropertyName("code")]
        //public string Code { get; set; }
        ////Search by cca2, ccn3, cca3 or cioc country code (yes, any!)


        //[JsonPropertyName("currencies")]
        //public string Currencies { get; set; }
        ////Search by currency code or name


        //[JsonPropertyName("languages")]
        //public string Languages { get; set; }
        ////Search by language code or name


        [JsonPropertyName("capital")]
        public List<String> Capital { get; set; }
        ////Search by capital city

        [JsonPropertyName("region")]
        public string Region { get; set; }
        //Search by region (replace X with the version you want to use)


        [JsonPropertyName("subregion")]
        public string SubRegion { get; set; }
        //You can search by subregions (replace X with the version you want to use)

        [JsonPropertyName("borders")]
        public List<String> Borders { get; set; }
    

        //[JsonPropertyName("translations")]
        //public TranslationModel Translations { get; set; }
        ////You can search by any translation name
    }
}