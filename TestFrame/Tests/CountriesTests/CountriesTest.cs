using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Security.Authentication;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.Models.CatsModels;
using TestFrame.Models.CountriesModels;
using TestFrame.Models.PetsModels;
using Xunit;
using Xunit.Abstractions;
using TestFrame;
using FluentAssertions.Equivalency;

namespace TestFrame.Tests
{
    public class CountriesTest : OrderedAcceptanceTestsBase<CountriesTestFixtures>
    {
        private RestBuilder restBuilder = new RestBuilder();
        private RestFactory restFactory;

        public CountriesTest(CountriesTestFixtures testFixture, ITestOutputHelper outputHelper) : base(testFixture, outputHelper)
        {

            var api = config.GetSection("CountriesTestData")["CountriesApiUri"];
            TestFixture.Name = config.GetSection("CountriesTestData")["Name"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            restFactory = new RestFactory(restBuilder);
        }

        //=============
        //   Test 1
        //=============
        [Fact, TestPriority(1)]
        public async Task Get_Country_By_Name_Test()
        {
            var response = await restFactory.Create()
                                            .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                            .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;

            //Create new object "nameCountry" based on NameModel
            var nameCountry = new NameModel()
            {
                Common = "Romania",
                Official = "Romania"
            };



            //Create new object "iddCountry" based on IddModel
            var iddCountry = new IddModel()
            {
                Root = "+4",
                Suffixes = new List<string> { "0" }
            };


            //Create new object "carCountry" based on CarModel
            var carCountry = new CarModel()
            {
                Signs = new List<string> { "RO" },
                Side = "right"
            };

            //Create new object "capitalCountru" based on CapitalInfoModel
            var capitalCountru = new CapitalInfoModel()
            {
                //CapitalInfo = ?,  ?????  DE FACUT AICI    
            };

            //Create new object "currencyCountry" based on CurrenciesModel
            var currencyCountry = new CurrenciesModel()
            {
                //Currency = "RON"  ?????  DE FACUT AICI    
            };

            //Create new object "postalcodeCountry" based on PostalCodeModel
            var postalcodeCountry = new PostalCodeModel()
            {
                Format = "######",
                Regex = "^(\\d{6})$"
            };

            //Create new object "maspCountry" based on MapsModel
            var mapsCountry = new MapsModel()
            {
                GoogleMaps = "https://goo.gl/maps/845hAgCf1mDkN3vr7",
                OpenStreetMaps = "https://www.openstreetmap.org/relation/90689"
            };

            //Create new object "flagCountry" based on FlagsModel
            var flagCountry = new FlagsModel()
            {
                Png = "https://flagcdn.com/w320/ro.png",
                Svg = "https://flagcdn.com/ro.svg",
                Alt = "The flag of Romania is composed of three equal vertical bands of navy blue, yellow and red."
            };


            //Create new object "createCountryModel" based on CountryModel
            var createCountryModel = new CountryModel()
            {
                Cca2 = "RO",
                Cca3 = "ROU",
                Ccn3 = "642",
                Cioc = "ROU",
                Independent = true,
                Status = "officially-assigned",
                UnMember = true,
                Region = "Europe",
                SubRegion = "Southeast Europe",
                Landlocked = false,
                Area = 238391,
                Flag = "🇷🇴",
                Population = 19286123,
                Fifa = "ROU",
                StartOfWeek = "monday",
                Capital = new List<string> { "Bucharest" },
                AltSpellings = new List<string> { "RO", "Rumania", "Roumania","România"},
                Tld = new List<string> { ".ro" },
                Borders = new List<string> { "BGR", "HUN", "MDA", "SRB", "UKR"},
                Latlng = new List<double> { 46.0, 25.0},
                Timezones = new List<string> { "UTC+02:00" },
                Continents = new List<string> { "Europe" }
            };



            //[JsonPropertyName("currencies")]
            //public CurrenciesModel Currencies { get; set; }

            //[JsonPropertyName("capitalInfo")]
            //public CapitalInfoModel CapitalInfo { get; set; }


            //[JsonPropertyName("demonyms")]
            //public DemonymsModel Demonyms { get; set; }



            #region Asserts
            using (new AssertionScope())
            {

                //==========================================================
                //Check HTTP status code
                //==========================================================
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.FirstOrDefault();

                //==========================================================
                //Check country list of codes
                //Check country independence, status, unmember
                //Check country region, subregion
                //Check country landlocked, area, flag
                //Check country population, fifa, start of week
                //==========================================================
                getResponse[0].Cca2.Should().Be(createCountryModel.Cca2);
                getResponse[0].Cca3.Should().Be(createCountryModel.Cca3);
                getResponse[0].Ccn3.Should().Be(createCountryModel.Ccn3);
                getResponse[0].Cioc.Should().Be(createCountryModel.Cioc);
                getResponse[0].Independent.Should().Be(createCountryModel.Independent);
                getResponse[0].Status.Should().Be(createCountryModel.Status);
                getResponse[0].UnMember.Should().Be(createCountryModel.UnMember);
                getResponse[0].Region.Should().Be(createCountryModel.Region);
                getResponse[0].SubRegion.Should().Be(createCountryModel.SubRegion);
                getResponse[0].Landlocked.Should().Be(createCountryModel.Landlocked);
                getResponse[0].Area.Should().Be(createCountryModel.Area);
                getResponse[0].Flag.Should().Be(createCountryModel.Flag);
                getResponse[0].Population.Should().Be(createCountryModel.Population);
                getResponse[0].Fifa.Should().Be(createCountryModel.Fifa);
                getResponse[0].StartOfWeek.Should().Be(createCountryModel.StartOfWeek);
                getResponse[0].Capital.Should().BeEquivalentTo(createCountryModel.Capital);
                getResponse[0].AltSpellings.Should().BeEquivalentTo(createCountryModel.AltSpellings);
                getResponse[0].Tld.Should().BeEquivalentTo(createCountryModel.Tld);
                getResponse[0].Borders.Should().BeEquivalentTo(createCountryModel.Borders);
                getResponse[0].Latlng.Should().BeEquivalentTo(createCountryModel.Latlng);
                getResponse[0].Timezones.Should().BeEquivalentTo(createCountryModel.Timezones);
                getResponse[0].Continents.Should().BeEquivalentTo(createCountryModel.Continents);

                //==========================================================
                //Check country name
                //==========================================================
                getResponse[0].Name.Common.Should().Be(nameCountry.Common);
                getResponse[0].Name.Official.Should().Be(nameCountry.Official);

                //==========================================================
                //Check country map
                //==========================================================
                getResponse[0].Maps.GoogleMaps.Should().Be(mapsCountry.GoogleMaps);
                getResponse[0].Maps.OpenStreetMaps.Should().Be(mapsCountry.OpenStreetMaps);

                //==========================================================
                //Check country flags
                //==========================================================
                getResponse[0].Flags.Png.Should().Be(flagCountry.Png);
                getResponse[0].Flags.Svg.Should().Be(flagCountry.Svg);
                getResponse[0].Flags.Alt.Should().Be(flagCountry.Alt);


                //==========================================================
                //Check country Idd
                //==========================================================
                getResponse[0].Idd.Root.Should().Be(iddCountry.Root);
                getResponse[0].Idd.Suffixes.Should().BeEquivalentTo(iddCountry.Suffixes);


                //==========================================================
                //Check country Car
                //==========================================================
                getResponse[0].Car.Signs.Should().BeEquivalentTo(carCountry.Signs);
                getResponse[0].Car.Side.Should().Be(carCountry.Side);


                //==========================================================
                //Check country Postal Code
                //==========================================================
                getResponse[0].PostalCode.Format.Should().Be(postalcodeCountry.Format);
                getResponse[0].PostalCode.Regex.Should().Be(postalcodeCountry.Regex);

                //==========================================================
                //Check object is not null
                //==========================================================
                getResponse.Should().NotBeNull();
            }
            #endregion
        }
    }
}


