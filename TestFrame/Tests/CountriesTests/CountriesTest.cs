using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Hosting;
using RestSharp;
using System.Net;
using System.Security.Authentication;
using System.Text.Json.Nodes;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.Models.CatsModels;
using TestFrame.Models.CountriesModels;
using Xunit;
using Xunit.Abstractions;

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

        [Fact, TestPriority(1)]
        public async Task Get_Country_By_Name_Negative_Test()
        {
           var response = await restFactory.Create()
                                            .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                            .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;

            var values = getResponse[0].Currencies.Currency;

            Dictionary<string, string> wrongValuesCurrency = new Dictionary<string, string>
            {
            { "name", "Romanian ron" },
            { "symbol", "ron" }
            };

            var nameCountry = new NameModel()
            {
                Common = "Roumanie",
                Official = "Roumanie"
            };

            var carCountry = new CarModel()
            {
                Signs = new List<string> { "ROU" },
                Side = "left"
            };

            var capitalCountry = new CapitalInfoModel()
            {
                Latlang = new List<double> { 45.43 , 27.1 }
            };

            var postalcodeCountry = new PostalCodeModel()
            {
                Format = "#####",
                Regex = "^(\\d{8})$"
            };

            var mapsCountry = new MapsModel()
            {
                GoogleMaps = "https://goo.gl/maps/845hAgCf1mDkN3vr8",
                OpenStreetMaps = "https://www.openstreetmap.org/relation/90681"
            };

            var flagCountry = new FlagsModel()
            {
                Png = "https://flagcdn.com/w320/ro.jpg",
                Svg = "https://flagcdn.com/ro.png",
                Alt = "The flag of Romania is composed of three equal vertical bands of navy white, yellow and red."
            };

            var createCountryModel = new CountryModel()
            {
                Cca2 = "FailTest",
                Cca3 = "FailTest",
                Ccn3 = "FailTest",
                Cioc = "FailTest",
                Independent = false,
                Status = "FailTest",
                UnMember = false,
                Region = "Asia",
                SubRegion = "Southwest Europe",
                Landlocked = true,
                Area = 238392,
                Flag = "FailTest",
                Population = 19286122,
                Fifa = "FailTest",
                StartOfWeek = "sunday",
                Capital = new List<string> { "Targoviste" },
                AltSpellings = new List<string> { "ROU", "Rumani", "Rumania", "Româania" },
                Tld = new List<string> { ".ro.net" },
                Borders = new List<string> { "BG", "HU", "MD", "SR", "UK" },
                Latlng = new List<double> { 48.0, 29.0 },
                Timezones = new List<string> { "UTC+04:00" },
                Continents = new List<string> { "Asia" }
            };

            var iddCountry = new IddModel()
            {
                Root = "+8",
                Suffixes = new List<string> { "3" }
            };


            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.FirstOrDefault();

                getResponse[0].Cca2.Should().NotBe(createCountryModel.Cca2);
                getResponse[0].Cca3.Should().NotBe(createCountryModel.Cca3);
                getResponse[0].Ccn3.Should().NotBe(createCountryModel.Ccn3);
                getResponse[0].Cioc.Should().NotBe(createCountryModel.Cioc);
                getResponse[0].Independent.Should().NotBe(createCountryModel.Independent);
                getResponse[0].Status.Should().NotBe(createCountryModel.Status);
                getResponse[0].UnMember.Should().NotBe(createCountryModel.UnMember);
                getResponse[0].Region.Should().NotBe(createCountryModel.Region);
                getResponse[0].SubRegion.Should().NotBe(createCountryModel.SubRegion);
                getResponse[0].Landlocked.Should().NotBe(createCountryModel.Landlocked);
                getResponse[0].Area.Should().NotBe(createCountryModel.Area);
                getResponse[0].Flag.Should().NotBe(createCountryModel.Flag);
                getResponse[0].Population.Should().NotBe(createCountryModel.Population);
                getResponse[0].Fifa.Should().NotBe(createCountryModel.Fifa);
                getResponse[0].StartOfWeek.Should().NotBe(createCountryModel.StartOfWeek);
                getResponse[0].Capital.Should().NotBeEquivalentTo(createCountryModel.Capital);
                getResponse[0].AltSpellings.Should().NotBeEquivalentTo(createCountryModel.AltSpellings);
                getResponse[0].Tld.Should().NotBeEquivalentTo(createCountryModel.Tld);
                getResponse[0].Borders.Should().NotBeEquivalentTo(createCountryModel.Borders);
                getResponse[0].Latlng.Should().NotBeEquivalentTo(createCountryModel.Latlng);
                getResponse[0].Timezones.Should().NotBeEquivalentTo(createCountryModel.Timezones);
                getResponse[0].Continents.Should().NotBeEquivalentTo(createCountryModel.Continents);

                getResponse[0].CapitalInfo.Should().NotBeEquivalentTo(capitalCountry.Latlang);

                getResponse[0].Name.Common.Should().NotBe(nameCountry.Common);
                getResponse[0].Name.Official.Should().NotBe(nameCountry.Official);

                getResponse[0].Maps.GoogleMaps.Should().NotBe(mapsCountry.GoogleMaps);
                getResponse[0].Maps.OpenStreetMaps.Should().NotBe(mapsCountry.OpenStreetMaps);

                getResponse[0].Flags.Png.Should().NotBe(flagCountry.Png);
                getResponse[0].Flags.Svg.Should().NotBe(flagCountry.Svg);
                getResponse[0].Flags.Alt.Should().NotBe(flagCountry.Alt);

                getResponse[0].Idd.Root.Should().NotBe(iddCountry.Root);
                getResponse[0].Idd.Suffixes.Should().NotBeEquivalentTo(iddCountry.Suffixes);

                getResponse[0].Car.Signs.Should().NotBeEquivalentTo(carCountry.Signs);
                getResponse[0].Car.Side.Should().NotBe(carCountry.Side);

                getResponse[0].PostalCode.Format.Should().NotBe(postalcodeCountry.Format);
                getResponse[0].PostalCode.Regex.Should().NotBe(postalcodeCountry.Regex);


                //Assert.Fail("error message for this case");

                getResponse[0].Idd.Suffixes.Should().NotBeEquivalentTo(iddCountry.Suffixes);

                foreach (var currency in values)
                {
                    JsonObject valueCurrency = (JsonObject)currency.Value;

                    foreach (var key in wrongValuesCurrency.Keys)
                    {
                        if (valueCurrency.ContainsKey(key))
                        {
                            wrongValuesCurrency[key].Should().NotBe((string)valueCurrency[key]);
                        }
                    }
                }

            }
            #endregion
            
        }

    }
}
