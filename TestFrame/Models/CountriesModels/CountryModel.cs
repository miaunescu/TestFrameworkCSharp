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


        //[JsonPropertyName("currency")]
        //public string Currency { get; set; }
        ////Search by currency code or name


        //[JsonPropertyName("languages")]
        //public LanguageModel Languages { get; set; } 
        ////Search by language code or name


        //[JsonPropertyName("capital")]
        //public string Capital { get; set; }
        ////Search by capital city


        //[JsonPropertyName("callingcode")]
        //public int CallingCode { get; set; }
        ////In version 3, calling codes are in the idd object. There is no implementation to search by calling codes in V3.


        //[JsonPropertyName("region")]
        //public string Region { get; set; }
        ////Search by region (replace X with the version you want to use)


        //[JsonPropertyName("subregion")]
        //public string SubRegion { get; set; }
        ////You can search by subregions (replace X with the version you want to use)


        //[JsonPropertyName("translations")]
        //public TranslationModel Translations { get; set; }
        ////You can search by any translation name
    }
}