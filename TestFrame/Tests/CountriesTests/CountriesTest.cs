using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RestSharp;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using TestFrame.Base;
using TestFrame.Builder;
using TestFrame.Fixtures;
using TestFrame.MessageBroker;
using TestFrame.Models.CountriesModels;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Tests.CountriesTests
{
    public class CountriesTest : OrderedAcceptanceTestsBase<CountriesTestFixtures>
    {
        private readonly IRestBuilder _restBuilder;

        private const string _countriesTestData = "CountriesTestData";
        //general for Queue
        private static string messagebody; //message for RabbitMq - the object is put here
        private string exchange = string.Empty;
        private string routingKey = "RestCountries";
        private string queueName = "RestCountries";
        IBasicProperties basicProperties = null;

        //general RabbitMQ
        private static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "config")).AddJsonFile("appsettings.json").Build();
        RabbitMQManager rabbitMQManager = RabbitMQManager.GetInstance(configuration);


        public CountriesTest(CountriesTestFixtures testFixture, ITestOutputHelper outputHelper, IRestBuilder restBuilder) : base(testFixture, outputHelper)
        {
            var api = config.GetSection(_countriesTestData)["CountriesApiUri"];
            TestFixture.Name = config.GetSection(_countriesTestData)["Name"];
            TestFixture.Api = api;
            TestFixture.Client = RestClientFactory.CreateBasicClient(api);
            _restBuilder = restBuilder;
        }

        internal void DeclareQueue()
        {
            rabbitMQManager.DeclareQueue(queueName, false, false, false, null);
        }

        internal void PublishMessage()
        {
            var body = Encoding.UTF8.GetBytes(messagebody);
            rabbitMQManager.PublishMessage(exchange, routingKey, basicProperties, body);
        }

        internal void ConsumeMessage()
        {
            rabbitMQManager.ConsumeMessage(queueName, false);
        }

        internal void ConsumeQueue()
        {
            rabbitMQManager.ConsumeQueue(queueName, true);
        }

        internal void StopConsumer()
        {
            rabbitMQManager.StopConsumer();
        }

        //=============
        //   Test 1
        //=============
        [Fact, TestPriority(1)]
        public async Task Get_Country_By_Name_Test()
        {
            var response = await _restBuilder.Create()
                 .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                 .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;

            var complexObject = new CountryModel()
            {
                Name = new NameModel()
                {
                    Common = "Romania",
                    Official = "Romania"
                },

                Idd = new IddModel()
                {
                    Root = "+4",
                    Suffixes = new List<string> { "0" }
                },

                Car = new CarModel()
                {
                    Signs = new List<string> { "RO" },
                    Side = "right"
                },

                CapitalInfo = new CapitalInfoModel()
                {
                    Latlang = new List<double> { 44.43, 26.1 }
                },

                PostalCode = new PostalCodeModel()
                {
                    Format = "######",
                    Regex = "^(\\d{6})$"
                },

                Maps = new MapsModel()
                {
                    GoogleMaps = "https://goo.gl/maps/845hAgCf1mDkN3vr7",
                    OpenStreetMaps = "https://www.openstreetmap.org/relation/90689"
                },

                Flags = new FlagsModel()
                {
                    Png = "https://flagcdn.com/w320/ro.png",
                    Svg = "https://flagcdn.com/ro.svg",
                    Alt = "The flag of Romania is composed of three equal vertical bands of navy blue, yellow and red."
                },

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
                AltSpellings = new List<string> { "RO", "Rumania", "Roumania", "România" },
                Tld = new List<string> { ".ro" },
                Borders = new List<string> { "BGR", "HUN", "MDA", "SRB", "UKR" },
                Latlng = new List<double> { 46.0, 25.0 },
                Timezones = new List<string> { "UTC+02:00" },
                Continents = new List<string> { "Europe" },
            };

            var nameCountry = new NameModel()
            {
                Common = "Romania",
                Official = "Romania"
            };

            var iddCountry = new IddModel()
            {
                Root = "+4",
                Suffixes = new List<string> { "0" }
            };

            var carCountry = new CarModel()
            {
                Signs = new List<string> { "RO" },
                Side = "right"
            };

            var capitalCountry = new CapitalInfoModel()
            {
                Latlang = new List<double> { 44.43, 26.1 }
            };

            var currencyCountry = new CurrenciesModel()
            {
                //Currency = { "RON" }   
            };

            var postalcodeCountry = new PostalCodeModel()
            {
                Format = "######",
                Regex = "^(\\d{6})$"
            };

            var mapsCountry = new MapsModel()
            {
                GoogleMaps = "https://goo.gl/maps/845hAgCf1mDkN3vr7",
                OpenStreetMaps = "https://www.openstreetmap.org/relation/90689"
            };

            var flagCountry = new FlagsModel()
            {
                Png = "https://flagcdn.com/w320/ro.png",
                Svg = "https://flagcdn.com/ro.svg",
                Alt = "The flag of Romania is composed of three equal vertical bands of navy blue, yellow and red."
            };

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
                AltSpellings = new List<string> { "RO", "Rumania", "Roumania", "România" },
                Tld = new List<string> { ".ro" },
                Borders = new List<string> { "BGR", "HUN", "MDA", "SRB", "UKR" },
                Latlng = new List<double> { 46.0, 25.0 },
                Timezones = new List<string> { "UTC+02:00" },
                Continents = new List<string> { "Europe" },
            };

            //producer RabbitMQ
            DeclareQueue();
            messagebody = JsonConvert.SerializeObject(complexObject);
            PublishMessage();
            //ConsumeMessage();
            //ConsumeQueue();
            //StopConsumer(); // works just with ConsumeQueue

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.FirstOrDefault();

                getResponse.Should().NotBeNull();

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

                if (getResponse[0].Name != null)
                {
                    getResponse[0].Name.Common.Should().Be(nameCountry.Common);
                    getResponse[0].Name.Official.Should().Be(nameCountry.Official);
                }

                if (getResponse[0].Maps != null)
                {
                    getResponse[0].Maps.GoogleMaps.Should().Be(mapsCountry.GoogleMaps);
                    getResponse[0].Maps.OpenStreetMaps.Should().Be(mapsCountry.OpenStreetMaps);
                }

                if (getResponse[0].Flags != null)
                {
                    getResponse[0].Flags.Png.Should().Be(flagCountry.Png);
                    getResponse[0].Flags.Svg.Should().Be(flagCountry.Svg);
                    getResponse[0].Flags.Alt.Should().Be(flagCountry.Alt);
                }

                if (getResponse[0].Idd != null)
                {
                    getResponse[0].Idd.Root.Should().Be(iddCountry.Root);
                    getResponse[0].Idd.Suffixes.Should().BeEquivalentTo(iddCountry.Suffixes);
                }

                if (getResponse[0].Car != null)
                {
                    getResponse[0].Car.Signs.Should().BeEquivalentTo(carCountry.Signs);
                    getResponse[0].Car.Side.Should().Be(carCountry.Side);
                }

                if (getResponse[0].PostalCode != null)
                {
                    getResponse[0].PostalCode.Format.Should().Be(postalcodeCountry.Format);
                    getResponse[0].PostalCode.Regex.Should().Be(postalcodeCountry.Regex);
                }

                getResponse[0].CapitalInfo.Latlang.Should().BeEquivalentTo(capitalCountry.Latlang);
            }
            #endregion
        }

        //=============
        //   Test 2
        //=============
        [Fact, TestPriority(2)]
        public async Task Check_Latitude_Longitude()
        {
            var response = await _restBuilder.Create()
                                             .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                             .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;


            var createCountryModel = new CountryModel()
            {
                Latlng = new List<double> { 46.0, 25.0 },
            };

            messagebody = JsonConvert.SerializeObject("mesaj");
            PublishMessage();
            //ConsumeQueue();
            //ConsumeMessage();

            #region Asserts
            using (new AssertionScope())
            {

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.Should().NotBeNull();

                getResponse.Should().NotBeNull();
                getResponse[0].Latlng.Should().NotBeNullOrEmpty();
                getResponse[0].Latlng[0].Should().BeInRange(-90, 90);
                getResponse[0].Latlng[1].Should().BeInRange(-180, 180);
            }
            #endregion
        }

        //=============
        //   Test 3
        //=============
        [Fact, TestPriority(3)]
        public async Task Check_Continents()
        {
            var response = await _restBuilder.Create()
                                             .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                             .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;


            #region Asserts
            using (new AssertionScope())
            {

                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.Should().NotBeNull();

                getResponse.Should().NotBeNull();

                var continents = new List<string> { "Africa", "Antarctica", "Asia", "Europe", "North America", "Australia/Oceania", "South America" };
                getResponse[0].Continents.Should().ContainSingle(continent => continents.Contains(continent));

            }
            #endregion
        }


        //===================
        //   Negative tests
        //===================
        [Fact, TestPriority(4)]
        public async Task Get_Country_By_Name_Negative_Test()
        {
            var response = await _restBuilder.Create()
                                            .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                            .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;

            var values = getResponse[0].Currencies.Currency;

            var wrongNameCountry = new NameModel()
            {
                Common = "Rooomaaaaniiiaaaa",
                Official = "Roamaaniaa"
            };

            var wrongIddCountry = new IddModel()
            {
                Root = "+7",
                Suffixes = new List<string> { "8" }
            };

            var wrongCarCountry = new CarModel()
            {
                Signs = new List<string> { "ROOO" },
                Side = "left"
            };

            var wrongCapitalCountry = new CapitalInfoModel()
            {
                Latlang = new List<double> { 43.43, 25.1 }
            };

            var wrongValuesCurrency = new Dictionary<string, string>
            {
                {"name", "Romanian ron" },
                {"symbol", "ron" }
            };

            var wrongPostalcodeCountry = new PostalCodeModel()
            {
                Format = "####",
                Regex = "^(\\d{4})$"
            };

            var wrongMapsCountry = new MapsModel()
            {
                GoogleMaps = "https://goo.gl/maps/845hAgCf1mDkN3vr5",
                OpenStreetMaps = "https://www.openstreetmap.org/relation/90685"
            };

            var wrongFlagCountry = new FlagsModel()
            {
                Png = "https://flagcdn.com/w320/ro.jpg",
                Svg = "https://flagcdn.com/ro.png",
                Alt = "The flag of Romania is composed of three equal vertical bands of navy blue, green and red."
            };

            var wrongCountryModel = new CountryModel()
            {
                Cca2 = "ROTest",
                Cca3 = "ROUTest",
                Ccn3 = "642Test",
                Cioc = "ROUTest",
                Independent = false,
                Status = "officially-assignedTest",
                UnMember = false,
                Region = "Africa",
                SubRegion = "Southeast Africa",
                Landlocked = true,
                Area = 238399,
                Flag = "🇷🇴Test",
                Population = 19286126,
                Fifa = "ROUTest",
                StartOfWeek = "tuesday",
                Capital = new List<string> { "Iasi" },
                AltSpellings = new List<string> { "ROOO", "Rumaniaaaa", "Roumaniaaaa", "Româniaaaaa" },
                Tld = new List<string> { ".ro.net" },
                Borders = new List<string> { "BG", "HU", "MD", "SR", "UK" },
                Latlng = new List<double> { 45.0, 24.0 },
                Timezones = new List<string> { "UTC+03:00" },
                Continents = new List<string> { "Africa" }
            };

            #region Asserts
            using (new AssertionScope())
            {
                response.StatusCode.Should().Be(HttpStatusCode.OK);
                response.Content.Should().NotBeNull();

                getResponse.Should().NotBeNull();

                getResponse[0].Cca2.Should().NotBe(wrongCountryModel.Cca2);
                getResponse[0].Cca3.Should().NotBe(wrongCountryModel.Cca3);
                getResponse[0].Ccn3.Should().NotBe(wrongCountryModel.Ccn3);
                getResponse[0].Cioc.Should().NotBe(wrongCountryModel.Cioc);
                getResponse[0].Independent.Should().NotBe(wrongCountryModel.Independent);
                getResponse[0].Status.Should().NotBe(wrongCountryModel.Status);
                getResponse[0].UnMember.Should().NotBe(wrongCountryModel.UnMember);
                getResponse[0].Region.Should().NotBe(wrongCountryModel.Region);
                getResponse[0].SubRegion.Should().NotBe(wrongCountryModel.SubRegion);
                getResponse[0].Landlocked.Should().NotBe(wrongCountryModel.Landlocked);
                getResponse[0].Area.Should().NotBe(wrongCountryModel.Area);
                getResponse[0].Flag.Should().NotBe(wrongCountryModel.Flag);
                getResponse[0].Population.Should().NotBe(wrongCountryModel.Population);
                getResponse[0].Fifa.Should().NotBe(wrongCountryModel.Fifa);
                getResponse[0].StartOfWeek.Should().NotBe(wrongCountryModel.StartOfWeek);
                getResponse[0].Capital.Should().NotBeEquivalentTo(wrongCountryModel.Capital);
                getResponse[0].AltSpellings.Should().NotBeEquivalentTo(wrongCountryModel.AltSpellings);
                getResponse[0].Tld.Should().NotBeEquivalentTo(wrongCountryModel.Tld);
                getResponse[0].Borders.Should().NotBeEquivalentTo(wrongCountryModel.Borders);
                getResponse[0].Latlng.Should().NotBeEquivalentTo(wrongCountryModel.Latlng);
                getResponse[0].Timezones.Should().NotBeEquivalentTo(wrongCountryModel.Timezones);
                getResponse[0].Continents.Should().NotBeEquivalentTo(wrongCountryModel.Continents);

                getResponse[0].CapitalInfo.Should().NotBeEquivalentTo(wrongCapitalCountry.Latlang);

                if (getResponse[0].Name != null)
                {
                    getResponse[0].Name.Common.Should().NotBe(wrongNameCountry.Common);
                    getResponse[0].Name.Official.Should().NotBe(wrongNameCountry.Official);
                }

                if (getResponse[0].Maps != null)
                {
                    getResponse[0].Maps.GoogleMaps.Should().NotBe(wrongMapsCountry.GoogleMaps);
                    getResponse[0].Maps.OpenStreetMaps.Should().NotBe(wrongMapsCountry.OpenStreetMaps);
                }

                if (getResponse[0].Flags != null)
                {
                    getResponse[0].Flags.Png.Should().NotBe(wrongFlagCountry.Png);
                    getResponse[0].Flags.Svg.Should().NotBe(wrongFlagCountry.Svg);
                    getResponse[0].Flags.Alt.Should().NotBe(wrongFlagCountry.Alt);
                }

                if (getResponse[0].Idd != null)
                {
                    getResponse[0].Idd.Root.Should().NotBe(wrongIddCountry.Root);
                    getResponse[0].Idd.Suffixes.Should().NotBeEquivalentTo(wrongIddCountry.Suffixes);
                }

                if (getResponse[0].Car != null)
                {
                    getResponse[0].Car.Signs.Should().NotBeEquivalentTo(wrongCarCountry.Signs);
                    getResponse[0].Car.Side.Should().NotBe(wrongCarCountry.Side);
                }

                if (getResponse[0].PostalCode != null)
                {
                    getResponse[0].PostalCode.Format.Should().NotBe(wrongPostalcodeCountry.Format);
                    getResponse[0].PostalCode.Regex.Should().NotBe(wrongPostalcodeCountry.Regex);
                }

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


