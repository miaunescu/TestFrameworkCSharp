using System.Text.Json.Serialization;

namespace TestFrame.Models.CountriesModels
{
    public class CountryModel
    {
        [JsonPropertyName("name")]
        public NameModel Name { get; set; }
        //Search by country name. If you want to get an exact match, use the next endpoint.
        //It can be the common or official value

        [JsonPropertyName("tld")]
        public List<string> Tld { get; set; }

        [JsonPropertyName("cca2")]
        public string Cca2 { get; set; }

        [JsonPropertyName("ccn3")]
        public string Ccn3 { get; set; }

        [JsonPropertyName("cca3")]
        public string Cca3 { get; set; }

        [JsonPropertyName("cioc")]
        public string Cioc { get; set; }

        [JsonPropertyName("independent")]
        public bool Independent { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("unMember")]
        public bool UnMember { get; set; }

        [JsonPropertyName("currencies")]
        public CurrenciesModel Currencies { get; set; }

        [JsonPropertyName("idd")]
        public IddModel Idd { get; set; }

        [JsonPropertyName("capital")]
        public List<string> Capital { get; set; }
        //Search by capital city

        [JsonPropertyName("altSpellings")]
        public List<string> AltSpellings { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }
        //Search by region (replace X with the version you want to use)

        [JsonPropertyName("subregion")]
        public string SubRegion { get; set; }
        //You can search by subregions (replace X with the version you want to use)

        //languages de facut - dinamic

        //translations de facut

        [JsonPropertyName("latlng")]
        public List<double> Latlng { get; set; }

        [JsonPropertyName("landlocked")]
        public bool Landlocked { get; set; }

        [JsonPropertyName("borders")]
        public List<string> Borders { get; set; }

        [JsonPropertyName("area")]
        public double Area { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        [JsonPropertyName("maps")]
        public MapsModel Maps { get; set; }

        [JsonPropertyName("population")]
        public double Population { get; set; }

        //gini de facut

        [JsonPropertyName("fifa")]
        public string Fifa { get; set; }

        [JsonPropertyName("car")]
        public CarModel Car { get; set; }

        [JsonPropertyName("timezones")]
        public List<string> Timezones { get; set; }

        [JsonPropertyName("continents")]
        public List<string> Continents { get; set; }

        [JsonPropertyName("flags")]
        public FlagsModel Flags { get; set; }

        [JsonPropertyName("startOfWeek")]
        public string StartOfWeek { get; set; }

        [JsonPropertyName("capitalInfo")]
        public CapitalInfoModel CapitalInfo { get; set; }

        [JsonPropertyName("postalCode")]
        public PostalCodeModel PostalCode { get; set; }

        [JsonPropertyName("demonyms")]
        public DemonymsModel Demonyms { get; set; }

        //[JsonPropertyName("translations")]
        //public TranslationModel Translations { get; set; }
        ////You can search by any translation name
    }
}