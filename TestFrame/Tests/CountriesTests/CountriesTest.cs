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
        public async Task Get_Country_By_Name_Test()
        {
           var response = await restFactory.Create()
                                            .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                            .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;

            var values = getResponse[0].Currencies.Currency;

            Dictionary<string, string> wrongValuesCurrency = new Dictionary<string, string>
            {
            { "name", "Romanian leuuuuu" },
            { "symbol", "leiiiii" }
            };

            var iddCountry = new IddModel()
            {
                Root = "+4",
                Suffixes = new List<string> { "0" }
            };

            var country = new CountryModel()
            { 
                Cca2 = "gkjsdkjfsdkfjs",
                Cca3 = "sdfsdfsdfs"
            };


            #region Asserts
            using (new AssertionScope())
            {
                //getResponse[0].Idd.Suffixes[0].Should().Be(iddCountry.Suffixes[0]);
                //getResponse[0].Cca3.Should().NotBe(country.Cca2);
                //getResponse[0].Cca3.Should().Be(country.Cca2);

                //getResponse[0].Idd.Suffixes.Should().BeEquivalentTo(iddCountry.Suffixes);

                foreach (var currency in values)
                {
                    JsonObject valueCurrency = (JsonObject)currency.Value;

                    foreach (var key in wrongValuesCurrency.Keys)
                    {
                        if (valueCurrency.ContainsKey(key))
                        {
                            wrongValuesCurrency[key].Should().Be((string)valueCurrency[key]);
                        }
                    }
                }

            }
            #endregion
            
        }

    }
}
