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

        [Fact, TestPriority(1)]
        public async Task Get_Country_By_Name_Test()
        {
           var response = await restFactory.Create()
                                            .WithRequest($"/name/{TestFixture.Name}", Method.Get)
                                            .Execute<List<CountryModel>>(TestFixture.Client);
            var getResponse = response.Data;
            var values = getResponse[0].Currencies.Currency;

            var nameCountry = new NameModel()
            {
                Common = "Romania",
                Official = "Romania"
            };

            var wrongNameCountry = new NameModel()
            {
                Common = "Rooomaaaaniiiaaaa",
                Official = "Roamaaniaa"
            };

            var iddCountry = new IddModel()
            {
                Root = "+4",
                Suffixes = new List<string> { "0" }
            };

            var wrongIddCountry = new IddModel()
            {
                Root = "+7",
                Suffixes = new List<string> { "8" }
            };


            #region Asserts
            using (new AssertionScope())
            {
                //****************************************************************
                //Check country name Common and Official are not equal to wrongNameCountry
                //****************************************************************

                getResponse[0].Name.Common.Should().NotBe(wrongNameCountry.Common);
                getResponse[0].Name.Official.Should().NotBe(wrongNameCountry.Official);


                //****************************************************************
                //Check if country name Common and Official are equal to wrongNameCountry - Should be - Negative test
                //****************************************************************

                getResponse[0].Name.Common.Should().Be(wrongNameCountry.Common); 
                getResponse[0].Name.Official.Should().Be(wrongNameCountry.Official); 


                //****************************************************************
                //Check country id is not equal to wrongIddCountry - Should not be - Positive test
                //****************************************************************

                getResponse[0].Idd.Root.Should().NotBe(wrongIddCountry.Root);
                getResponse[0].Idd.Suffixes.Should().NotBeEquivalentTo(wrongIddCountry.Suffixes);
                
                //****************************************************************
                //Check country id if it is equal to wrongIddCountry - Should be - Negative test
                //****************************************************************

                getResponse[0].Idd.Root.Should().Be(wrongIddCountry.Root);
                getResponse[0].Idd.Suffixes.Should().BeEquivalentTo(wrongIddCountry.Suffixes);
            }
            #endregion
        }

    }
}
